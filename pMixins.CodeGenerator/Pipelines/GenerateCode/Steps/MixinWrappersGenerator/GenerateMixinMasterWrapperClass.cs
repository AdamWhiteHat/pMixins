﻿//----------------------------------------------------------------------- 
// <copyright file="GenerateMixinWrapperClass.cs" company="Copacetic Software"> 
// Copyright (c) Copacetic Software.  
// <author>Philip Pittle</author> 
// <date>Sunday, January 26, 2014 5:54:45 PM</date> 
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CopaceticSoftware.CodeGenerator.StarterKit.Extensions;
using CopaceticSoftware.Common.Extensions;
using CopaceticSoftware.Common.Patterns;
using CopaceticSoftware.pMixins.CodeGenerator.Extensions;
using CopaceticSoftware.pMixins.CodeGenerator.Infrastructure;
using CopaceticSoftware.pMixins.CodeGenerator.Pipelines.GenerateCode.Infrastructure;
using CopaceticSoftware.pMixins.CodeGenerator.Pipelines.GenerateCode.Steps.PostClassGeneration;
using CopaceticSoftware.pMixins.CodeGenerator.Pipelines.GenerateCode.Steps.TargetPartialClassGenerator;
using CopaceticSoftware.pMixins.Infrastructure;
using CopaceticSoftware.pMixins.Interceptors;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;

namespace CopaceticSoftware.pMixins.CodeGenerator.Pipelines.GenerateCode.Steps.MixinWrappersGenerator
{
    /// <summary>
    /// Creates a wrapper class of the Mixin in the autogenerated namespace
    /// which elevates all protected members to public so it can be used by the Target
    /// </summary>
    public class GenerateMixinMasterWrapperClass : IPipelineStep<pMixinGeneratorPipelineState>
    {
        /// <summary>
        /// Name of the DataMember that stores the reference to an instance of the interface
        /// created in <see cref="GenerateMixinImplementationRequirementsInterface"/>.
        /// This instance is the Target and is added in the constructor for the
        /// Mixin Wrapper class (if the constructor is needed).
        /// </summary>
        public const string MixinInstanceDataMemberName = "_mixinInstance";

        public const string ActivateMixinDependenciesMethodName = "__ActivateMixinDependencies";

        private string GenerateVirtualMethodFuncName(IMember member)
        {
            return member.Name + "Func";
        }

        public bool PerformTask(pMixinGeneratorPipelineState manager)
        {
            if (manager.CurrentpMixinAttribute.Mixin.GetDefinition().IsStatic)
            {
                manager.CurrentMixinMasterWrapperFullTypeName = 
                    manager.CurrentpMixinAttribute.Mixin.GetOriginalFullNameWithGlobal();
                return true;
            }
            
            //Create Wrapper Class Declaration
            var wrapperClassDeclaration = new TypeDeclaration
                                              {
                                                  ClassType = ClassType.Class,
                                                  Modifiers = Modifiers.Public | Modifiers.Sealed,
                                                  Name = manager.CurrentpMixinAttribute.Mixin.GetNameAsIdentifier()
                                                         + "MasterWrapper"
                                              };

            //Set base type
            wrapperClassDeclaration.BaseTypes.Add(
                new SimpleType("global::" + typeof(MasterWrapperBase).FullName));

            //Add class to Auto Generated nested class
            var wrapperClass = 
                manager.CurrentAutoGeneratedTypeDeclaration.AddNestedType(wrapperClassDeclaration);

            
            //Save Master Wrapper Type Name for later
            manager.CurrentMixinMasterWrapperFullTypeName =
                GenerateMixinSpecificAutoGeneratedClass.GetFullNameForChildType(
                    manager,
                    wrapperClassDeclaration.Name);

            CreateVirtualMemberFunctions(manager, wrapperClass);

            CreateConstructorAndRequirementsInterfaceDataMember(manager, wrapperClass);

            CreateActivateMixinDependenciesMethod(manager, wrapperClass);

            ProcessMembers(manager, wrapperClass);

            return true;
            
        }

        /// <summary>
        /// Create code similar to:
        /// <code>
        /// <![CDATA[
        /// public System.Func<int,string> ProtectedVirtualMethodFunc { get; set; }
        ///   
        /// public System.Func<string> PublicVirtualPropertyGetFunc { get; set; }
        /// public System.Action<string> PublicVirtualPropertySetFunc { get; set; }
        ///   
        /// ]]></code>
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="wrapperClass"></param>
        private void CreateVirtualMemberFunctions(
            pMixinGeneratorPipelineState manager, ICodeGeneratorProxy wrapperClass)
        {
            foreach (
                var member in
                    manager.CurrentMixinMembers.Select(x => x.Member)
                        .Where(member => member.IsVirtual || member.IsOverride))
            {
                if (member is IMethod)
                {

                    var propertyReturnType =
                        (member as IMethod).ReturnType.Kind == TypeKind.Void
                            ? "global::System.Action"
                            : String.Format("global::System.Func<{0}>",
                                string.Join(",",
                                    (member as IMethod).Parameters.Select(x => x.Type.GetOriginalFullNameWithGlobal())
                                        .Union(member.ReturnType.GetFullTypeName())));
                        
                    wrapperClass.CreateProperty(
                        "public",
                        propertyReturnType,
                        this.GenerateVirtualMethodFuncName(member),
                        "get;",
                        "set;");
                }
                else if (member is IProperty)
                {
                    //Get
                    if ((member as IProperty).CanGet && !(member as IProperty).Getter.IsPrivate)
                        wrapperClass.CreateProperty(
                            "public",
                            "global::System.Func<" + member.ReturnType.GetOriginalFullNameWithGlobal() + ">",
                            GenerateVirtualMethodFuncName(member) + "Get",
                            "get;",
                            "set;");
                    //Set
                    if ((member as IProperty).CanSet && !(member as IProperty).Setter.IsPrivate)
                        wrapperClass.CreateProperty(
                            "public",
                            "global::System.Action<" + member.ReturnType.GetOriginalFullNameWithGlobal() + ">",
                            GenerateVirtualMethodFuncName(member) + "Set",
                            "get;",
                            "set;");
                }
            }
        }

        /// <summary>
        /// Generate code similar to:
        /// <code>
        /// <![CDATA[
        ///     AbstractWrapper _host;
        /// 
        ///     public TargetWrapper(IMixinRequirements host){ _host = host; }
        /// ]]>
        /// </code>
        /// </summary>
        private void CreateConstructorAndRequirementsInterfaceDataMember(
            pMixinGeneratorPipelineState manager, ICodeGeneratorProxy wrapperClass)
        {
            var mixinInstanceType = "";

            #region Assign Mixin Instance Type
            if (manager.CurrentpMixinAttribute.Mixin.GetDefinition().IsAbstract)
                //Abstract Mixin Wrapper is in a different namespace, so we need the fullname
                mixinInstanceType =
                    ExternalGeneratedNamespaceHelper.GenerateChildClassFullName(
                        manager,
                        GenerateAbstractMixinMembersWrapperClass.GetWrapperClassName(manager));
            else if (manager.CurrentMixinMembers.Any(x => x.Member.IsProtected))
            {
                //Abstract Mixin Wrapper is in the same namespace
                mixinInstanceType = GenerateAbstractMixinMembersWrapperClass.GetWrapperClassName(manager);
            }
            else
            {
                mixinInstanceType = manager.CurrentpMixinAttribute.Mixin.GetOriginalFullNameWithGlobal();
            }
            #endregion


            //add data member
            wrapperClass.CreateDataMember(
                /* Can't remember why it was necessary to have a modifier other than public
                manager.CurrentpMixinAttribute.Mixin.GetDefinition().IsPublic
                    ? "public"
                    : manager.CurrentpMixinAttribute.Mixin.GetDefinition().IsInternal
                        ? "internal"
                        : "private"
                 */ "public"
                    + " readonly",
                mixinInstanceType,
                MixinInstanceDataMemberName);

            string mixinInstanceInitialization;

            #region Create Mixin Instance (InitializeMixin / TryActivateMixin
            if (manager.CurrentpMixinAttribute.ExplicitlyInitializeMixin)
            {
                mixinInstanceInitialization =
                    string.Format("(({0}){1}).{2}();",
                        AddMixinConstructorRequirementDependency
                            .GetMixinConstructorRequirement(manager),
                        MixinInstanceDataMemberName.Replace("_", ""), 
                        "InitializeMixin");
            }
            else if (manager.CurrentpMixinAttribute.Mixin.GetDefinition().IsSealed)
            {
                mixinInstanceInitialization =
                    string.Format("base.TryActivateMixin<{0}>();",
                       manager.CurrentpMixinAttribute.Mixin.GetOriginalFullNameWithGlobal());
            }
            else if (manager.CurrentpMixinAttribute.Mixin.GetDefinition().IsAbstract)
            {
                mixinInstanceInitialization =
                    string.Format("base.TryActivateMixin<{0}>({1});",
                            mixinInstanceType,
                        MixinInstanceDataMemberName.Replace("_", ""));
            }
            else
            {
                mixinInstanceInitialization =
                    string.Format("base.TryActivateMixin<{0}>({1});",
                        GenerateMixinSpecificAutoGeneratedClass.GetFullNameForChildType(
                            manager,
                            GenerateAbstractMixinMembersWrapperClass.GetWrapperClassName(manager)),
                        MixinInstanceDataMemberName.Replace("_", ""));
            }
            #endregion

            
            var initializeBaseExpression =
            #region base.Initialize
                string.Format("base.Initialize( {0}, {1}, new global::{2}<global::{3}>{{ {4} }});",
                    MixinInstanceDataMemberName.Replace("_",""),
                    MixinInstanceDataMemberName,
                    "System.Collections.Generic.List",
                    typeof(IMixinInterceptor),
                    string.Join(",",
                        manager.CurrentpMixinAttribute.Interceptors
                            .Select(x => x.GenerateActivationExpression())
                    )
                );
            #endregion

            wrapperClass.CreateConstructor(
                "public",
                 new List<KeyValuePair<string, string>>
                 {
                     new KeyValuePair<string, string>(
                         manager.CurrentMixinRequirementsInterface.EnsureStartsWith("global::"),
                         MixinInstanceDataMemberName.Replace("_",""))
                 },
                 constructorInitializer: "",
                 constructorBody:
                    MixinInstanceDataMemberName + " = " +
                        mixinInstanceInitialization +
                        CreateConstructorInitializersForVirtualMemberFunctions(manager, wrapperClass) +
                        initializeBaseExpression
                 );
        }

        private string CreateConstructorInitializersForVirtualMemberFunctions(
            pMixinGeneratorPipelineState manager, ICodeGeneratorProxy wrapperClass)
        {
            var proxyMemberHelper =
                new MasterWrapperCodeGeneratorProxyMemberHelper(wrapperClass,
                    manager.BaseState.Context.TypeResolver.Compilation);

            var sb = new StringBuilder();

            foreach (
                var member in manager.CurrentMixinMembers
                        .Select(x => x.Member)
                        .Where(member => !member.IsPrivate && (member.IsVirtual || member.IsOverride)))
            {
                if (member is IMethod)
                {
                    //ProtectedVirtualMethodFunc = (i) => AbstractWrapper.ProtectedVirtualMethod(i);
                    sb.AppendFormat("{0} = ({1}) => {2};",

                        GenerateVirtualMethodFuncName(member),
                        string.Join(",",
                            (member as IMethod).Parameters.Select(x => x.Name)),
                        proxyMemberHelper.GetMethodBodyCallStatement(
                            member as IMethod, 
                            (m) => MixinInstanceDataMemberName, 
                            (m) => m.Name));
                }
                else if (member is IProperty)
                {
                    //PublicVirtaulPropertyFuncGet = () => AbstractWrapper.PublicVirtualProperty;
                    //PublicVirtualPropertySetFunc = (s) => AbstractWrapper.PublicVirtualProperty = s;
                    sb.AppendFormat(
                        @"{0}Get = () => {1};
                          {0}Set = (value) => {2};",
                        GenerateVirtualMethodFuncName(member),
                        proxyMemberHelper.GetPropertyGetterReturnBodyStatement(
                            member as IProperty,
                            (m) => MixinInstanceDataMemberName,
                            (m) => m.Name),
                        proxyMemberHelper.GetPropertySetterReturnBodyStatement(
                            member as IProperty,
                            (m) => MixinInstanceDataMemberName,
                            (m) => m.Name));
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Generate code similar to:
        /// <code>
        /// <![CDATA[
        ///     public void __ActivateMixinDependencies(Target target)
        ///     {
        ///        ((IMixinDependency<T>)_mixinInstance).Dependency = mixinInstance;
        ///        ((IMixinDependency<T>)_mixinInstance).OnDependencySet();
        ///     }
        /// ]]>
        /// </code>
        /// </summary>
        private void CreateActivateMixinDependenciesMethod(
             pMixinGeneratorPipelineState manager, ICodeGeneratorProxy wrapperClass)
        {
            const string targetMetherParameterVariableName = "target";

            wrapperClass.CreateMethod(
                modifier: "public",
                returnTypeFullName: "void",
                methodName: ActivateMixinDependenciesMethodName,
                parameters: new[]
                {
                    new KeyValuePair<string, string>(
                        manager.GeneratedClass.ClassName,
                        targetMetherParameterVariableName),
                },
                methodBody: 
                    
                    string.Join(Environment.NewLine,
                        manager.CurrentpMixinAttribute.Mixin
                            .GetAllBaseTypes()
                            .Where(t =>
                                EnsureMixinDependenciesAreSatisfiedOnGeneratedClass.TypeIsIMixinDependency(t))
                            .Select(t =>
                                string.Format("(({0}){1}).Dependency = {2};(({0}){1}).OnDependencySet();",
                                    t.GetOriginalFullNameWithGlobal(),
                                    MixinInstanceDataMemberName,
                                    targetMetherParameterVariableName)))

                    //Ensure the method is not created as MethodName(); but MethodName(){}
                    + Environment.NewLine
                
                );
        }

        /// <summary>
        /// Wrap every non-private member in current pMixin with special
        /// handling for static and virtual members
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="wrapperClass"></param>
        private void ProcessMembers(pMixinGeneratorPipelineState manager, ICodeGeneratorProxy wrapperClass)
        {
            var proxyMemberHelper = 
                new MasterWrapperCodeGeneratorProxyMemberHelper(wrapperClass,
                    manager.BaseState.Context.TypeResolver.Compilation);


            //Static Members
            proxyMemberHelper.CreateMembers(
                    manager.CurrentMixinMembers
                        .Select(x => x.Member)
                        .Where(member => member.IsStatic),
                    generateMemberModifier: member => "internal static",
                    baseObjectIdentifierFunc: 
                        member => 
                            manager.CurrentpMixinAttribute.Mixin.IsStaticOrSealed()
                            ? manager.CurrentpMixinAttribute.Mixin.GetOriginalFullNameWithGlobal()
                            : manager.CurrentMixinAbstractMembersWrapperClass.Name
                );

            //Regular Members
            proxyMemberHelper.CreateMembers(
                    manager.CurrentMixinMembers
                        .Select(x => x.Member)
                        .Where( 
                            member => !member.IsStatic &&
                            !(member.IsAbstract && member.IsProtected) &&
                            !member.IsOverride && 
                            !member.IsVirtual),
                    generateMemberModifier: member => "internal",
                    baseObjectIdentifierFunc: member => MixinInstanceDataMemberName);

            //Protected Abstract Members 
            proxyMemberHelper.CreateMembers(
                    manager.CurrentMixinMembers
                        .Select(x => x.Member)
                        .Where( member => member.IsAbstract && member.IsProtected),
                   generateMemberModifier: member => "internal",
                   baseObjectIdentifierFunc: member => MixinInstanceDataMemberName,
                   baseObjectMemberNameFunc: member => 
                       GenerateAbstractMixinMembersWrapperClass.GetProtectedMemberWrapperMemberName(member));


            //Virtual Members
            foreach (
                var member in
                     manager.CurrentMixinMembers
                        .Select(x => x.Member)
                        .Where( member => member.IsVirtual || member.IsOverride))
            {
                #region Virtual Method
                if (member is IMethod)
                {
                    //Virtual methods call to the Virtual Method Func
                    wrapperClass.CreateMethod(
                        "internal",
                        member.ReturnType.GetOriginalFullNameWithGlobal(),
                        member.Name,
                        (member as IMethod).Parameters.ToKeyValuePair(),
                        string.Format("{0} {1}({2});",
                            (member as IMethod).GetReturnString(),
                            GenerateVirtualMethodFuncName(member),
                            string.Join(",",
                                (member as IMethod).Parameters.Select(x => x.Name))),
                        (member as IMethod).GetGenericMethodConstraints(
                            manager.BaseState.Context.TypeResolver.Compilation));
                }
                #endregion
                #region Virtual Property
                else if (member is IProperty)
                {
                    wrapperClass.CreateProperty(
                        "internal",
                        member.ReturnType.GetOriginalFullNameWithGlobal(),
                        member.Name,
                        (member as IProperty).CanGet && !(member as IProperty).Getter.IsPrivate
                            ? string.Format("get{{ return {0}(); }}",
                                GenerateVirtualMethodFuncName(member) + "Get")
                            : "",
                        (member as IProperty).CanSet && !(member as IProperty).Setter.IsPrivate
                            ? string.Format("set {{{0}(value); }}",
                                GenerateVirtualMethodFuncName(member) + "Set")
                            : ""
                        );
                }
                #endregion
            }
        }
        

    }
}
