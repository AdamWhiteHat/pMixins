﻿//----------------------------------------------------------------------- 
// <copyright file="IntegrationTestBase.cs" company="Copacetic Software"> 
// Copyright (c) Copacetic Software.  
// <author>Philip Pittle</author> 
// <date>Sunday, May 4, 2014 2:49:53 PM</date> 
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
using CopaceticSoftware.CodeGenerator.StarterKit.Ninject;
using CopaceticSoftware.pMixins.Tests.Common;
using Ninject;

namespace CopaceticSoftware.CodeGenerator.StarterKit.Tests.IntegrationTests
{
    public abstract class IntegrationTestBase : TestBase
    {
        protected static IKernel Kernel { get; private set; }

        static IntegrationTestBase()
        {
            Kernel = new StandardKernel(new StandardModule());

            Kernel.Bind<IVisualStudioEventProxy>().To<DummyVisualStudioEventProxy>();

            Kernel.Bind<IVisualStudioWriter>().To<TestVisualStudioWriter>();
        }

    }
}
