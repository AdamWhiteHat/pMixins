﻿//----------------------------------------------------------------------- 
// <copyright file="StandardModule.cs" company="Copacetic Software"> 
// Copyright (c) Copacetic Software.  
// <author>Philip Pittle</author> 
// <date>Saturday, May 3, 2014 2:37:05 PM</date> 
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

using CopaceticSoftware.CodeGenerator.StarterKit.Infrastructure;
using CopaceticSoftware.CodeGenerator.StarterKit.Infrastructure.Caching;
using CopaceticSoftware.CodeGenerator.StarterKit.Infrastructure.IO;
using CopaceticSoftware.CodeGenerator.StarterKit.Infrastructure.VisualStudioSolution;
using CopaceticSoftware.CodeGenerator.StarterKit.Threading;
using Ninject.Modules;

namespace CopaceticSoftware.CodeGenerator.StarterKit.Ninject
{
    public class StandardModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICodeGeneratorContextFactory>().To<CodeGeneratorContextFactory>();
            Bind<ICSharpProjectFactory>().To<CSharpProjectFactory>();
            Bind<IMicrosoftBuildProjectLoader>().To<MicrosoftBuildProjectLoader>();
            Bind<IMicrosoftBuildProjectAssemblyReferenceResolver>().To<CachedMicrosoftBuildProjectAssemblyReferenceResolver>().InSingletonScope();
            Bind<ISolutionFactory>().To<SolutionFactory>();
            Bind<ISolutionFileReader>().To<SolutionFileReader>();
            
            Bind<IFileWrapper>().To<FileWrapper>();
            Bind<IFileReader>().To<VisualStudioFileCache>().InSingletonScope();
            Bind<ICSharpFileFactory>().To<CSharpFileFactory>().InSingletonScope();
            Bind<ISolutionContext>().To<SolutionContext>().InSingletonScope();
            Bind<IVisualStudioOpenDocumentManager>().To<VisualStudioOpenDocumentManager>().InSingletonScope();
            Bind<ICacheEventHelper>().To<CacheEventHelper>().InSingletonScope();
            Bind<ICodeGeneratorDependencyManager>().To<CodeGeneratorDependencyManager>().InSingletonScope();

            Bind<ITaskFactory>().To<TaskFactoryWrapper>();
        }
    }
}
