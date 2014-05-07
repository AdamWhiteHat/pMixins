﻿//----------------------------------------------------------------------- 
// <copyright file="SolutionFileReader.cs" company="Copacetic Software"> 
// Copyright (c) Copacetic Software.  
// <author>Philip Pittle</author> 
// <date>Wednesday, April 30, 2014 11:07:08 PM</date> 
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

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using CopaceticSoftware.CodeGenerator.StarterKit.Infrastructure.IO;
using CopaceticSoftware.Common.Infrastructure;
using log4net;

namespace CopaceticSoftware.CodeGenerator.StarterKit.Infrastructure.VisualStudioSolution
{
    public class SolutionFileProjectReference
    {
        public string Title;
        public string ProjectFileName;
    }
    public interface ISolutionFileReader
    {
        IEnumerable<SolutionFileProjectReference> ReadProjectReferences(string solutionFileName);
    }

    public class SolutionFileReader : ISolutionFileReader
    {
        private readonly IFileReader _fileReader;

        private static readonly Regex ProjectLinePattern = new Regex(
            "Project\\(\"(?<TypeGuid>.*)\"\\)\\s+=\\s+\"(?<Title>.*)\",\\s*\"(?<Location>.*)\",\\s*\"(?<Guid>.*)\"");

        private static ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public SolutionFileReader(IFileReader fileReader)
        {
            _fileReader = fileReader;
        }

        public IEnumerable<SolutionFileProjectReference> ReadProjectReferences(string solutionFileName)
        {
            if (!File.Exists(solutionFileName))
                throw new FileNotFoundException("Solution FileName", solutionFileName);

            Ensure.ArgumentNotNullOrEmpty(solutionFileName, "solutionFileName");

            var directory = Path.GetDirectoryName(solutionFileName);

            foreach (string line in _fileReader.ReadLines(solutionFileName))
            {
                Match match = ProjectLinePattern.Match(line);
                if (match.Success)
                {
                    string typeGuid = match.Groups["TypeGuid"].Value;
                    string title = match.Groups["Title"].Value;
                    string location = match.Groups["Location"].Value;
                    
                    switch (typeGuid.ToUpperInvariant())
                    {
                        case "{2150E333-8FDC-42A3-9474-1A3956D46DE8}": // Solution Folder
                            // ignore folders
                            break;
                        case "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}": // C# project
                           yield return new SolutionFileProjectReference
                                        {
                                            ProjectFileName = Path.GetFullPath(Path.Combine(directory, location)),
                                            Title = title
                                        };

                            break;
                        default:
                            _log.InfoFormat("Project {0} has unsupported type {1}", location, typeGuid);
                            break;
                    }
                }
            }
        }
    }
}
