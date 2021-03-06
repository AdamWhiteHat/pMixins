﻿//----------------------------------------------------------------------- 
// <copyright file="OnProjectReferenceAddedToExternalLibrary.cs" company="Copacetic Software"> 
// Copyright (c) Copacetic Software.  
// <author>Philip Pittle</author> 
// <date>Wednesday, May 7, 2014 6:35:12 PM</date> 
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

using System.Threading;
using CopaceticSoftware.CodeGenerator.StarterKit.Infrastructure;
using CopaceticSoftware.CodeGenerator.StarterKit.Infrastructure.IO;
using NUnit.Framework;
using Rhino.Mocks;

namespace CopaceticSoftware.CodeGenerator.StarterKit.Tests.IntegrationTests.VisualStudioEvents
{
    public class SimpleObject { }

    public class OnProjectReferenceAddedToExternalLibraryTest : MockSolutionTestBase
    {
        private readonly MockSourceFile _sourceFile = MockSourceFile.CreateDefaultFile();
        private const string _sourceFileClass = "SimpleObjectChild";

        public override void MainSetup()
        {
            base.MainSetup();

            _sourceFile.Source =
                string.Format(
                    @"
                        namespace Testing
                        {{
                            [ CopaceticSoftware.pMixins.Attributes.pMixin(Mixin = typeof({1}))]
                            public partial class {0}  {{ 
                                
                            }}
                        }}",
                    _sourceFileClass,
                    typeof (SimpleObject).FullName);

            // Set Initial Solution State
            _MockSolution.Projects.Add(new MockProject
            {
                MockSourceFiles = new []{_sourceFile}
            });
            
            //Code Generator should not be able to generate Mixin code yet
            Assert.False(
                CanGenerateMixinCodeForSourceFile(_sourceFile),
                "Should not be able to generate Mixin code for _sourceFile yet!  Was the file built correctly?");
           
            //Simulate Project Reference Added
            var referencePath = 
                new FilePath(
                    ReferenceHelper.GetReferenceToDllInTestProject(
                        typeof (SimpleObject).Assembly.Location));
           
            _MockSolution.Projects[0].AssemblyReferences.Add(referencePath.FullPath);

            //Fire Project Reference Added Event
            EventProxy.FireOnProjectReferenceAdded(this, new ProjectReferenceAddedEventArgs
            {
                ProjectFullPath = _MockSolution.Projects[0].FileName,
                ReferencePath = referencePath
            });

            //Wait a Second for the Async reader to catch up.
            Thread.Sleep(1000);

            //Make sure the Project was evicted from cache and reloaded
            _MockMicrosoftBuildProjectLoader.AssertWasCalled(
                x => x.LoadMicrosoftBuildProject(Arg.Is(_MockSolution.Projects[0].FileName)));
        }

        [Test]
        public void ClassWithExternalReferenceResolvesAfterReferencesHasBeenAdded()
        {
            Assert.True(
                CanGenerateMixinCodeForSourceFile(_sourceFile),
                "Failed to build Mixin code _sourceFile");
        }
    }
}
