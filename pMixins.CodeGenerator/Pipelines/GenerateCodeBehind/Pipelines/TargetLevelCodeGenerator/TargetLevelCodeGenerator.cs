﻿//----------------------------------------------------------------------- 
// <copyright file="TargetLevelCodeGenerator.cs" company="Copacetic Software"> 
// Copyright (c) Copacetic Software.  
// <author>Philip Pittle</author> 
// <date>Wednesday, July 23, 2014 11:34:24 AM</date> 
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

using CopaceticSoftware.Common.Patterns;
using CopaceticSoftware.pMixins.CodeGenerator.Pipelines.GenerateCodeBehind.Pipelines.TargetLevelCodeGenerator.Steps;

namespace CopaceticSoftware.pMixins.CodeGenerator.Pipelines.GenerateCodeBehind.Pipelines.TargetLevelCodeGenerator
{
    /// <summary>
    /// Controller class for executing the Target Level Code Generator Pipeline.
    /// </summary>
    public class TargetLevelCodeGenerator : IPipelineStep<TargetLevelCodeGeneratorPipelineState>
    {
        private readonly IPipelineStep<TargetLevelCodeGeneratorPipelineState>[] _targetLevelCodeGeneratorPipeline =
            {
                new CreateTargetCodeBehindTypeDeclaration(), 
                new RunMixinLevelCodeGeneratorForEachMixin()
            };

        public bool PerformTask(TargetLevelCodeGeneratorPipelineState manager)
        {
            return
                _targetLevelCodeGeneratorPipeline.RunPipeline(manager,
                    haltOnStepFailing: step => true);
        }
    }
}
