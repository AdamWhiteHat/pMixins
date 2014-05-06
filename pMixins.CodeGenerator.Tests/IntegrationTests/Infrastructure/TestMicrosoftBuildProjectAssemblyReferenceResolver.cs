﻿//----------------------------------------------------------------------- 
// <copyright file="TestMicrosoftBuildProjectAssemblyReferenceResolver.cs" company="Copacetic Software"> 
// Copyright (c) Copacetic Software.  
// <author>Philip Pittle</author> 
// <date>Monday, May 5, 2014 10:47:11 PM</date> 
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
using CopaceticSoftware.CodeGenerator.StarterKit.Infrastructure;
using CopaceticSoftware.CodeGenerator.StarterKit.Infrastructure.VisualStudioSolution;
using CopaceticSoftware.pMixins.VisualStudio;
using ICSharpCode.NRefactory.TypeSystem;
using Microsoft.Build.Evaluation;

namespace CopaceticSoftware.pMixins.CodeGenerator.Tests.IntegrationTests.Infrastructure
{
    public class TestMicrosoftBuildProjectAssemblyReferenceResolver :
        pMixinsMicrosoftBuildProjectAssemblyReferenceResolver
    {
        private const string projectMatchingString =
            "\\pMixins\\pMixins\\pMixins.csproj]";

        public TestMicrosoftBuildProjectAssemblyReferenceResolver(IVisualStudioEventProxy visualStudioEventProxy, IMicrosoftBuildProjectLoader buildProjectLoader) : base(visualStudioEventProxy, buildProjectLoader)
        {
        }

        protected override IEnumerable<ProjectReference> ResolveProjectReferences(Project project)
        {
            return
                base.ResolveProjectReferences(project)
                    //Don't include project reference to pMixins
                    .Where(p => !p.ToString().EndsWith(projectMatchingString));

        }
    }
}
