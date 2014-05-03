﻿//----------------------------------------------------------------------- 
// <copyright file="RawSourceFile.cs" company="Copacetic Software"> 
// Copyright (c) Copacetic Software.  
// <author>Philip Pittle</author> 
// <date>Saturday, May 3, 2014 4:30:55 PM</date> 
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

namespace CopaceticSoftware.CodeGenerator.StarterKit.Infrastructure.VisualStudioSolution
{
    public class RawSourceFile
    {
        public string FileName { get; set; }
        public string ProjectFileName { get; set; }
        public string FileContents { get; set; }
    }
}
