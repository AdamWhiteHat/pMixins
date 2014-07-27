﻿//----------------------------------------------------------------------- 
// <copyright file="CodeGenerationPlan.cs" company="Copacetic Software"> 
// Copyright (c) Copacetic Software.  
// <author>Philip Pittle</author> 
// <date>Thursday, July 24, 2014 3:11:56 PM</date> 
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
using CopaceticSoftware.pMixins.CodeGenerator.Pipelines.ResolveAttributes.Infrastructure;
using ICSharpCode.NRefactory.CSharp;

namespace CopaceticSoftware.pMixins.CodeGenerator.Infrastructure.CodeGenerationPlan
{
    /// <summary>
    /// Contains plan for Target level members
    /// and links to wrapper specific plans
    /// </summary>
    public class CodeGenerationPlan
    {
        public CodeGenerationPlan()
        {
            MixinGenerationPlans = new Dictionary<pMixinAttributeResolvedResult, MixinGenerationPlan>();
        }

        public IDictionary<
            pMixinAttributeResolvedResult,
            MixinGenerationPlan> MixinGenerationPlans { get; set; }

        public TypeDeclaration SourceClass { get; set; }

        /// <summary>
        /// Collects all <see cref="MemberWrapper"/>s
        /// in <see cref="MixinGenerationPlans"/>.
        /// </summary>
        public IEnumerable<MemberWrapper> Members
        {
            get { return MixinGenerationPlans.SelectMany(x => x.Value.Members); }
        }

        public RequirementsInterfacePlan SharedRequirementsInterfacePlan { get; set; }

        public string ExternalTargetSpecificAutoGeneratedNamespaceName { get; set; }

        /// <summary>
        /// Name for the container class for Auto Generated wrapper classes:
        /// <code>
        /// <![CDATA[
        /// public partial class Target
        /// {
        ///     private sealed class __pMixinAutoGenerated { }    
        /// }
        /// ]]>
        /// </code>
        /// </summary>
        public readonly string GlobalAutoGeneratedContainerClassName = "__pMixinAutoGenerated";

        /// <summary>
        /// The private property name for the accessor to the __Mixins class:
        /// <code>
        /// <![CDATA[
        /// private __Mixins __mixins {get;set;}
        /// ]]>
        /// </code>
        /// </summary>
        public string MixinsPropertyName { get; set; }

        /// <summary>
        /// Class name for the __Mixins class:
        /// <code>
        /// <![CDATA[
        /// public partial class Target
        /// {
        ///     private sealed class __Mixins{}
        /// }
        /// ]]>
        /// </code>
        /// </summary>
        public string MixinsClassName { get; set; }

        /// <summary>
        /// Static lock data member inside <see cref="MixinsClassName"/>:
        /// <code>
        /// <![CDATA[
        /// private sealed class __Mixins{
        ///     private static object ____Lock = new object();
        /// }
        /// ]]></code>
        /// </summary>
        public string MixinsLockVariableName { get; set; }

        /// <summary>
        /// Activate Mixin Dependencies method inside <see cref="MixinsClassName"/>:
        /// <code>
        /// <![CDATA[
        /// private sealed class __Mixins{
        ///     public void __ActivateMixinDependencies (Target target)
        ///     {
        ///     }
        /// }
        /// ]]></code>
        /// </summary>
        public string MixinsActivateMixinDependenciesMethodName { get; set; }
    }
}
