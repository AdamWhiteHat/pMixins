﻿//----------------------------------------------------------------------- 
// <copyright file="pMixinGeneratorPipelineState.cs" company="Copacetic Software"> 
// Copyright (c) Copacetic Software.  
// <author>Philip Pittle</author> 
// <date>Friday, January 31, 2014 3:26:27 PM</date> 
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
using CopaceticSoftware.pMixins.CodeGenerator.Infrastructure;
using CopaceticSoftware.pMixins.CodeGenerator.Pipelines.GenerateCode.Infrastructure;
using CopaceticSoftware.pMixins.CodeGenerator.Pipelines.GenerateCode.Steps.MixinWrappersGenerator;
using CopaceticSoftware.pMixins.CodeGenerator.Pipelines.ResolveAttributes.Infrastructure;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;

namespace CopaceticSoftware.pMixins.CodeGenerator.Pipelines.GenerateCode.Steps
{
    public class pMixinGeneratorPipelineState
    {
        public pMixinGeneratorPipelineState()
        {
            MixinContainerClassConstructorStatements = new List<string>();
            GeneratedClassInterfaceList = new List<string>();

            MixinMembers = new Dictionary<IType, List<MixinMemberResolvedResult>>();
        }

        /// <summary>
        /// Gets a reference to the <see cref="ICodeGeneratorProxy"/>
        /// that builds up interfaces and classes in the Auto Generated
        /// Namespace (ie pMixin.AutoGenerated).  This is where
        /// things like Wrapper classes for abstract Mixins are built
        /// </summary>
        /// <example>
        /// Usage from <see cref="GenerateMixinMasterWrapperClass"/>: 
        /// <code>
        /// <![CDATA[
        /// manager.BaseState.GeneratedCodeSyntaxTree.AddChildTypeDeclaration
        ///        (new ClassDeclaration(), manager.CurrentAutoGeneratedNamespace);
        /// ]]>
        /// </code>
        /// </example>
        [Obsolete("Use CurrentAutoGeneratedTypeDeclaration", true)]
        public NamespaceDeclaration CurrentAutoGeneratedNamespace { get; set; }

        public ICodeGeneratorProxy CurrentAutoGeneratedTypeDeclaration { get; set; }

        /// <summary>
        /// Fully qualified 
        /// </summary>
        [Obsolete("Use CurrentMixinInstanceVariableAccessor", true)]
        public string MixinVariableAccessor { get; set; }

        /// <summary>
        /// Class that is getting generated
        /// </summary>
        public ICodeGeneratorProxy GeneratedClass { get; set; }

        public TypeDeclaration SourceClass { get; set; }

        public ICodeGenerationPipelineState BaseState { get; set; }

        /// <summary>
        /// The pMixin Attribute currently being worked on
        /// </summary>
        public pMixinAttributeResolvedResult CurrentpMixinAttribute { get; set; }

        /// <summary>
        /// Type of the Mixin variable that will be created.
        /// </summary>
        public string CurrentMixinMasterWrapperFullTypeName { get; set; }

        public ICodeGeneratorProxy MixinContainerClassGeneratorProxy { get; set; }

        public IList<string> MixinContainerClassConstructorStatements { get; private set; }

        /// <summary>
        /// Gets the access for accessing the <see cref="CurrentpMixinAttribute"/>'s
        /// instance variable.
        /// 
        /// Like: "__mixins.MixinTypeName.Value
        /// </summary>
        public string CurrentMixinInstanceVariableAccessor { get; set; }

        public string CurrentMixinRequirementsInterface { get; set; }
        public TypeDeclaration CurrentMixinProtectedMembersWrapperClass { get; set; }
        public TypeDeclaration CurrentMixinAbstractMembersWrapperClass { get; set; }

        public IList<MixinMemberResolvedResult> CurrentMixinMembers { get; set; } 

        public List<string> GeneratedClassInterfaceList { get; private set; }

        public Dictionary<IType, List<MixinMemberResolvedResult>> MixinMembers { get; set; }

        /// <summary>
        /// __pMixinAutoGenerated class, used by all mixins
        /// </summary>
        public ICodeGeneratorProxy AutoGeneratedContainerClass { get; set; }
    }
}
