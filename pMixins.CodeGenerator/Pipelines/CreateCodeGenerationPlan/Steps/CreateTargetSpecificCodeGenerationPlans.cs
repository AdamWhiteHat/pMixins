﻿//----------------------------------------------------------------------- 
// <copyright file="CreateTagetSpecificCodeGenerationPlans.cs" company="Copacetic Software"> 
// Copyright (c) Copacetic Software.  
// <author>Philip Pittle</author> 
// <date>Thursday, July 24, 2014 3:54:20 PM</date> 
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
using CopaceticSoftware.Common.Patterns;
using CopaceticSoftware.pMixins.Attributes;
using CopaceticSoftware.pMixins.CodeGenerator.Extensions;
using CopaceticSoftware.pMixins.CodeGenerator.Infrastructure.CodeGenerationPlan;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;

namespace CopaceticSoftware.pMixins.CodeGenerator.Pipelines.CreateCodeGenerationPlan.Steps
{
    /// <summary>
    /// Populates a <see cref="CodeGenerationPlan"/> for every 
    /// <see cref="IPipelineCommonState.SourcePartialClassDefinitions"/>
    /// </summary>
    public class CreateTargetSpecificCodeGenerationPlans : IPipelineStep<ICreateCodeGenerationPlanPipelineState>
    {
        private const string AutogeneratedNamespacePrefix = "pMixins.AutoGenerated";

        public bool PerformTask(ICreateCodeGenerationPlanPipelineState manager)
        {
            foreach (var target in manager.CommonState.SourcePartialClassDefinitions)
            {
                manager.CodeGenerationPlans.Add(
                    target, 
                    new CodeGenerationPlan
                    {
                        SourceClass = 
                            target,

                        SourceClassMembers = 
                            ResolveSourceClassMembers(target, manager.CommonState),

                        TargetCodeBehindPlan = 
                            new TargetCodeBehindPlan
                            {
                                MixinsPropertyName = "__mixins",

                                MixinsClassName = "__Mixins",

                                MixinsLockVariableName = "____Lock",

                                MixinsActivateMixinDependenciesMethodName = "__ActivateMixinDependencies",
                            },

                        MixinGenerationPlans = 
                            manager.GetAllPMixinAttributes(target)
                                .ToDictionary(
                                    mixin => mixin,
                                    mixin => new MixinGenerationPlan
                                    {
                                        SourceClass = target,
                                        MixinAttribute = mixin
                                    }),

                        ExternalTargetSpecificAutoGeneratedNamespaceName = 
                            string.Format("{0}.{1}.{2}.{3}",
                                AutogeneratedNamespacePrefix,
                                (target.GetParent<NamespaceDeclaration>()
                                 ?? new NamespaceDeclaration("Unknown")).FullName,
                                target.Name,
                                "__Shared")
                    });

                //Wire TargetCodeBehindPlan up to CodeGenerationPlan
                manager.CodeGenerationPlans.Values.Map(
                    cgp => cgp.TargetCodeBehindPlan.CodeGenerationPlan = cgp);

                //Wire MixinGenerationPlan up to CodeGenerationPlan
                manager.CodeGenerationPlans.Values.Map(cgp => 
                    cgp.MixinGenerationPlans.Values.Map(
                        mgp =>  mgp.CodeGenerationPlan = cgp ));
                
            }

            return true;
        }

        private IEnumerable<IMember> ResolveSourceClassMembers(TypeDeclaration sourceClass, IPipelineCommonState manager)
        {
            var mixedInMemberAttribute =
                typeof (MixedInMemberAttribute).ToIType(manager.Context.TypeResolver.Compilation);

            var resolvedSourceResult = manager.Context.TypeResolver.Resolve(sourceClass);


            if (resolvedSourceResult.IsError)
                return Enumerable.Empty<IMember>();
                
            return
                resolvedSourceResult.Type.GetMembers()
                    .Where(m =>
                        !m.DeclaringType.Equals(resolvedSourceResult.Type) ||
                        !m.IsDecoratedWithAttribute(mixedInMemberAttribute));
        }

    }
}
