﻿//----------------------------------------------------------------------- 
// <copyright file="CreateMasterWrapperPlan.cs" company="Copacetic Software"> 
// Copyright (c) Copacetic Software.  
// <author>Philip Pittle</author> 
// <date>Thursday, July 24, 2014 6:03:42 PM</date> 
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

using System.Linq;
using CopaceticSoftware.CodeGenerator.StarterKit.Extensions;
using CopaceticSoftware.Common.Extensions;
using CopaceticSoftware.Common.Patterns;
using CopaceticSoftware.pMixins.CodeGenerator.Infrastructure.CodeGenerationPlan;
using CopaceticSoftware.pMixins.Infrastructure;
using ICSharpCode.NRefactory.TypeSystem;

namespace CopaceticSoftware.pMixins.CodeGenerator.Pipelines.CreateCodeGenerationPlan.Steps
{
    /// <summary>
    /// Creates a <see cref="MasterWrapperPlan"/> for every
    /// <see cref="MixinGenerationPlan"/>.
    /// </summary>
    public class CreateMasterWrapperPlan : IPipelineStep<ICreateCodeGenerationPlanPipelineState>
    {
        public bool PerformTask(ICreateCodeGenerationPlanPipelineState manager)
        {
            foreach (var mixinPlan in
                manager.CodeGenerationPlans.SelectMany(
                    x => x.Value.MixinGenerationPlans))
            {
                mixinPlan.Value.MasterWrapperPlan = BuildPlan(mixinPlan.Value);
            }

            return true;
        }

        private MasterWrapperPlan BuildPlan(MixinGenerationPlan mixinPlan)
        {
            var mixinInstanceTypeFullName = ResolveMixinInstanceTypeFullName(mixinPlan);

            return new MasterWrapperPlan
            {
                MixinGenerationPlan = mixinPlan,

                MasterWrapperClassName = 
                    mixinPlan.MixinAttribute.Mixin.GetNameAsIdentifier() + 
                    "MasterWrapper",

                MasterWrapperInstanceNameInMixinsContainer =
                    mixinPlan.MixinAttribute.Mixin.GetFullNameAsIdentifier(),

                MixinInstanceTypeFullName = 
                    mixinInstanceTypeFullName,

                MixinInstanceInitializationStatement =
                    CreateMixinInitializationStatement(mixinPlan, mixinInstanceTypeFullName),

                MixinDependencies = 
                    mixinPlan.MixinAttribute.Mixin
                        .GetAllBaseTypes()
                        .Where(t => 
                            t.Kind == TypeKind.Interface &&
                            t.GetOriginalFullName().StartsWith(
                                typeof (IMixinDependency<>).GetOriginalFullName()
                                //This is hacky, but can't find a way to compare typeof(IMixinDependency<>) 
                                //to new IType(IMixinDependency<int>)
                                .Replace("<>", ""))),

                ImplementExplicitlyMembers = 
                    mixinPlan.Members
                        .Where(x => x.ImplementationDetails.ImplementExplicitly),

                ProtectedAbstractMembers =
                    mixinPlan.Members
                        .Where(m =>
                            (m.Member.IsAbstract || m.Member.IsOverride)
                            && m.Member.IsProtected
                            && !m.ImplementationDetails.ImplementExplicitly),

                RegularMembers =
                    mixinPlan.Members
                        .Where(m =>
                            !m.Member.IsStaticOrConst() &&
                            !(m.Member.IsAbstract && m.Member.IsProtected) &&
                            !m.Member.IsOverride &&
                            !m.Member.IsOverridable &&
                            !m.Member.IsVirtual &&
                            !m.ImplementationDetails.ImplementExplicitly),

                StaticMembers =
                    mixinPlan.Members
                        .Where(m => m.Member.IsStaticOrConst()),

                VirtualMembers =
                    mixinPlan.Members
                        .Where(m =>
                            !m.ImplementationDetails.ImplementExplicitly &&
                            (m.Member.IsVirtual ||
                            (
                                m.Member.IsOverride ||
                                m.Member.IsOverridable &&
                                ! m.Member.IsProtected ))),

            };
        }

        private string ResolveMixinInstanceTypeFullName(
            MixinGenerationPlan mixinPlan)
        {
            if (!mixinPlan.ProtectedWrapperPlan.Members.Any() &&
               (
                    !mixinPlan.AbstractWrapperPlan.Members.Any() ||
                    mixinPlan.MixinAttribute.ExplicitlyInitializeMixin
                ))
            {
                //Can just use mixin type
                return
                    mixinPlan.MixinAttribute.Mixin.GetOriginalFullNameWithGlobal();
            }

            else if (mixinPlan.AbstractWrapperPlan.GenerateAbstractWrapperInExternalNamespace)
            {
                //External_Namespace.AbstractWrapper
                return
                    mixinPlan.ExternalMixinSpecificAutoGeneratedNamespaceName.EnsureEndsWith(".") +
                    mixinPlan.AbstractWrapperPlan.AbstractWrapperClassName;
            }
            else
            {
                //Abstract warpper is in the same namespace
                return mixinPlan.AbstractWrapperPlan.AbstractWrapperClassName;
            }
        }

        //TODO: Document this logic better.  Forget the need for edge cases
        private string CreateMixinInitializationStatement(
            MixinGenerationPlan mixinPlan,
            string mixinInstanceTypeFullName)
        {
            if (mixinPlan.MixinAttribute.ExplicitlyInitializeMixin)
            {
                //initialize by casting the 'target' constructor parameter
                //to IMixinConstructorRequirement and calling InitializeMixin

                return
                    string.Format(
                        @"( 
                                (global::CopaceticSoftware.pMixins.Infrastructure.
                                    IMixinConstructorRequirement<{0}>)
                                {1}
                            )
                            .InitializeMixin();",
                            mixinInstanceTypeFullName,
                            MasterWrapperPlan.TargetInstanceConstructorParameterName);
            }

            else if (mixinPlan.MixinAttribute.Mixin.GetDefinition().IsSealed)
            {
                return
                    string.Format("base.TryActivateMixin<{0}>();",
                      mixinPlan.MixinAttribute.Mixin.GetOriginalFullNameWithGlobal());
            }
            else if (mixinPlan.MixinAttribute.Mixin.GetDefinition().IsAbstract)
            {
                return
                    string.Format("base.TryActivateMixin<{0}>({1});",
                        mixinInstanceTypeFullName,
                        MasterWrapperPlan.TargetInstanceConstructorParameterName);
            }
            else if (mixinPlan.AbstractWrapperPlan.GenerateAbstractWrapperInExternalNamespace)
            {
                return
                    string.Format("base.TryActivateMixin<{0}>({1});",
                        mixinPlan.ExternalMixinSpecificAutoGeneratedNamespaceName
                            .EnsureEndsWith(".") +
                        mixinPlan.AbstractWrapperPlan.AbstractWrapperClassName,
                        MasterWrapperPlan.TargetInstanceConstructorParameterName);
            }
            else
            {
                return
                    string.Format("base.TryActivateMixin<{0}>({1});",
                        mixinPlan.AbstractWrapperPlan.AbstractWrapperClassName,
                        MasterWrapperPlan.TargetInstanceConstructorParameterName);
            }
        }
    }
}
