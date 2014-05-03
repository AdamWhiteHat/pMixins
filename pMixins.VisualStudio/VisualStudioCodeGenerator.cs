﻿//----------------------------------------------------------------------- 
// <copyright file="VisualStudioCodeGenerator.cs" company="Copacetic Software"> 
// Copyright (c) Copacetic Software.  
// <author>Philip Pittle</author> 
// <date>Saturday, May 3, 2014 3:15:31 PM</date> 
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
using System.Reflection;
using System.Text;
using CopaceticSoftware.CodeGenerator.StarterKit;
using CopaceticSoftware.CodeGenerator.StarterKit.Infrastructure;
using CopaceticSoftware.CodeGenerator.StarterKit.Infrastructure.VisualStudioSolution;
using log4net;

namespace CopaceticSoftware.pMixins.VisualStudio
{
    public interface IVisualStudioCodeGenerator
    {
        IEnumerable<byte[]> GenerateCode(IEnumerable<RawSourceFile> rawSourceFiles);
    }

    public class VisualStudioCodeGenerator : IVisualStudioCodeGenerator
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IVisualStudioWriter _visualStudioWriter;
        private readonly ICodeGeneratorContextFactory _codeGeneratorContextFactory;
        private readonly IPartialCodeGenerator _codeGenerator;
        private readonly IpMixinsSolutionManager _solutionManager;

        public VisualStudioCodeGenerator(IVisualStudioWriter visualStudioWriter, IpMixinsSolutionManager solutionManager, IPartialCodeGenerator codeGenerator, ICodeGeneratorContextFactory codeGeneratorContextFactory)
        {
            _visualStudioWriter = visualStudioWriter;
            _solutionManager = solutionManager;
            _codeGenerator = codeGenerator;
            _codeGeneratorContextFactory = codeGeneratorContextFactory;
        }

        public IEnumerable<byte[]> GenerateCode(IEnumerable<RawSourceFile> rawSourceFiles)
        {
            var sourceFileList = rawSourceFiles.ToList();

            IEnumerable<ICodeGeneratorContext> contexts = 
                _codeGeneratorContextFactory.GenerateContext(sourceFileList);

            foreach (var context in contexts)
            {
                Log.InfoFormat("Generating Code for file [{0}] in [{1}]",
                        context.Source.FileName, context.Source.Project.FileName);

                Log.DebugFormat("Input File Contents: {0}{1}",
                    Environment.NewLine,
                    context.Source.OriginalText);

                yield return GenerateCode(context);
            }
        }

        private byte[] GenerateCode(ICodeGeneratorContext context)
        {
            try
            {
                var response = _codeGenerator.GeneratePartialCode(context);

                _solutionManager.RegisterCodeGeneratorResponse(response);

                #region Write Errors / Warnings

                foreach (var error in response.Errors)
                    switch (error.Severity)
                    {
                        case CodeGenerationError.SeverityOptions.Error:
                            _visualStudioWriter.GeneratorError(error.Message, error.Line, error.Column);
                            break;

                        case CodeGenerationError.SeverityOptions.Warning:
                            _visualStudioWriter.GeneratorWarning(error.Message, error.Line, error.Column);
                            break;

                        case CodeGenerationError.SeverityOptions.Message:
                            _visualStudioWriter.GeneratorMessage(error.Message, error.Line, error.Column);
                            break;
                    }

                #endregion

                var generatedCode = response.GeneratedCodeSyntaxTree.GetText();

                Log.InfoFormat("Generated Code for File [{0}]: {1}{2}{1}",
                    Environment.NewLine,
                    generatedCode);

                return Encoding.UTF8.GetBytes(generatedCode);
            }
            catch (Exception e)
            {
                var errorMessage =
                    string.Format("Unhandled Exception Generating Code for File [{0}] in Product [{1}]: {2}",
                        context.Source.FileName,
                        context.Source.Project.FileName,
                        e.Message);

                if (Log.IsInfoEnabled)
                    errorMessage += Environment.NewLine + "File Contents:" + Environment.NewLine + context.Source.OriginalText;

                Log.Error(errorMessage, e);

                return Encoding.UTF8.GetBytes(""); 
            }
        }
    }
}