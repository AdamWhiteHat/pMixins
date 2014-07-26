﻿//----------------------------------------------------------------------- 
// <copyright file="GenerateMembersInTargetClass.cs" company="Copacetic Software"> 
// Copyright (c) Copacetic Software.  
// <author>Philip Pittle</author> 
// <date>Saturday, July 26, 2014 1:26:43 PM</date> 
// Licensed under the Apache License, Version 2.0,
// you may not use this file except in compliance with this License.
//  
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an 'AS IS' BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright> 
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using CopaceticSoftware.CodeGenerator.StarterKit.Extensions;
using CopaceticSoftware.Common.Extensions;
using CopaceticSoftware.Common.Patterns;
using CopaceticSoftware.pMixins.CodeGenerator.Infrastructure;
using CopaceticSoftware.pMixins.CodeGenerator.Infrastructure.CodeGenerationPlan;
using CopaceticSoftware.pMixins.CodeGenerator.Infrastructure.CodeGeneratorProxy;
using CopaceticSoftware.pMixins.CodeGenerator.Pipelines.GenerateCodeBehind.Pipelines.TargetLevelCodeGenerator;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;

namespace CopaceticSoftware.pMixins.CodeGenerator.Pipelines.GenerateCodeBehind.Pipelines.MixinLevelCodeGenerator.Steps.GenerateMembers
{
    /// <summary>
    /// Adds the <see cref="MixinGenerationPlan.MembersPromotedToTarget"/> to the 
    /// <see cref="TargetLevelCodeGeneratorPipelineState.TargetCodeBehindTypeDeclaration"/>.
    /// These methods will effectively be available to consumers of Target.
    /// 
    /// Member invocation is 
    /// proxied to the Master Wrapper (<see cref="MixinLevelCodeGeneratorPipelineState.MasterWrapper"/>
    /// via the __mixins private Property (<see cref="CodeGenerationPlan.MixinsPropertyName"/>)
    /// <code>
    /// <![CDATA[
    /// public partial class Target
    /// {
    ///     public void MixinMethod()
    ///     {
    ///         ___mixins.Other_Namespace_OtherMixin.OtherMixinMethod (); 
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// </summary>
    public class GenerateMembersInTargetClass : IPipelineStep<MixinLevelCodeGeneratorPipelineState>
    {
        public bool PerformTask(MixinLevelCodeGeneratorPipelineState manager)
        {
            var targetCodeBehindCodeGeneratorProxy = 
                new CodeGeneratorProxy(
                    manager.TargetLevelCodeGeneratorPipelineState.TargetCodeBehindTypeDeclaration);

            GenerateMembers(
                manager,
                targetCodeBehindCodeGeneratorProxy,
                manager.MixinGenerationPlan.MembersPromotedToTarget);

            return true;
        }

        private void GenerateMembers(
            MixinLevelCodeGeneratorPipelineState manager,
            ICodeGeneratorProxy targetCodeBehind, 
            IEnumerable<MemberWrapper> membersPromotedToTarget)
        {

            var compilation =
                manager.CommonState.Context.TypeResolver.Compilation;

            //__mixins.Test_ExampleMixin
            var masterWrapperVariableName =
                manager.MixinGenerationPlan.CodeGenerationPlan.MixinsPropertyName.EnsureEndsWith(".") +
                manager.MixinGenerationPlan.MasterWrapperPlan.MasterWrapperInstanceNameInMixinsContainer;

            foreach (var mw in membersPromotedToTarget)
            {
                EntityDeclaration newMemberDeclaration = null;

                if (mw.Member is IMethod)
                    newMemberDeclaration = GenerateMethod(
                        mw.Member as IMethod,
                        targetCodeBehind,
                        compilation,
                        masterWrapperVariableName);

                else if (mw.Member is IProperty)
                    newMemberDeclaration = GenerateProperty(
                        mw.Member as IProperty,
                        targetCodeBehind,
                        masterWrapperVariableName);

                else if (mw.Member is IField)
                    newMemberDeclaration = GenerateField(
                        mw.Member as IField,
                        targetCodeBehind,
                        masterWrapperVariableName);

                else
                    continue;

                //Copy attributes from member to newMemberDeclaration
                newMemberDeclaration.Attributes.AddRange(
                    mw.Member.MemberDefinition.Attributes
                        .FilterOutNonInheritedAttributes()
                        .ConvertToAttributeAstTypes()
                        .Select(attributeAstType => new AttributeSection(attributeAstType)));
            }
            
        }
        
        #region Methods

        private EntityDeclaration GenerateMethod(
            IMethod method,
            ICodeGeneratorProxy targetCodeBehind,
            ICompilation compilation,
            string masterWrapperVariableName)
        {
            return
                targetCodeBehind.CreateMethod(
                    modifier:
                        method.GetModifiersString(),
                    returnTypeFullName:
                        method.ReturnType.GetOriginalFullNameWithGlobal(),
                    methodName:
                        method.GetOriginalName(),
                    parameters:
                        method.Parameters.ToKeyValuePair(),
                    methodBody:
                        string.Format(
                            "{0} {1}.{2}({3});",
                            (method.ReturnType.Kind == TypeKind.Void)
                            ? string.Empty : "return",
                            masterWrapperVariableName,
                            method.GetOriginalName(),
                            string.Join(",", method.Parameters.Select(x => x.Name))),
                    constraingClause:
                        method.GetGenericMethodConstraints(compilation));
        }
        #endregion

        #region Properties

        private EntityDeclaration GenerateProperty(
            IProperty property,
            ICodeGeneratorProxy targetCodeBehind,
            string masterWrapperVariableName)
        {
            return
                targetCodeBehind.CreateProperty(
                    modifier:
                        property.GetModifiersString(),
                    returnTypeFullName:
                        property.ReturnType.GetOriginalFullNameWithGlobal(),
                    propertyName:
                        property.Name,
                    getterMethodBody:
                        GetPropertyGetterStatement(property, masterWrapperVariableName),
                    setterMethodBody:
                        GetPropertySetterStatement(property, masterWrapperVariableName));
        }

        private string GetPropertyGetterStatement(
           IProperty prop,
           string masterWrapperVariableName)
        {
            if (!prop.CanGet || prop.Getter.IsPrivate)
                return string.Empty;

            var formatString =
                (prop.IsVirtual)
                // virtual properties are wrapped as functions and need to be called as a method
                    ? "get{{ return {0}.{1}FuncGet(); }}"
                    : "get{{ return {0}.{1}; }}";


            return string.Format(formatString,
                                 masterWrapperVariableName,
                                 prop.Name);

        }

        private string GetPropertySetterStatement(
            IProperty prop,
            string masterWrapperVariableName)
        {
            if (!prop.CanSet || prop.Setter.IsPrivate)
                return string.Empty;

            var formatString =
                (prop.IsVirtual)
                // virtual properties are wrapped as functions and need to be called as a method
                    ? "set{{ {0}.{1}FuncSet(value); }}"
                    : "set{{ {0}.{1} = value; }}";

            return string.Format(formatString,
                                 masterWrapperVariableName,
                                 prop.Name);
        }

        #endregion

        #region Fields

        private EntityDeclaration GenerateField(
            IField field,
            ICodeGeneratorProxy targetCodeBehind,
            string masterWrapperVariableName)
        {
            return

                targetCodeBehind.CreateProperty(
                    modifier:
                        field.GetModifiersString(),
                    returnTypeFullName:
                        field.ReturnType.GetOriginalFullNameWithGlobal(),
                    propertyName:
                        field.Name,
                    getterMethodBody:
                        string.Format(
                            "get{{ return {0}.{1}; }}",
                            masterWrapperVariableName,
                            field.Name),
                    setterMethodBody:
                        (field.IsConst || field.IsReadOnly)
                            ? string.Empty
                            : string.Format(
                                "set{{ {0}.{1} = value; }}",
                                masterWrapperVariableName,
                                field.Name));

        }
        #endregion
    }
}
