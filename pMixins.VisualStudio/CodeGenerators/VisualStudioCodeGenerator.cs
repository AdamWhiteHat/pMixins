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
using CopaceticSoftware.CodeGenerator.StarterKit;
using CopaceticSoftware.CodeGenerator.StarterKit.Extensions;
using CopaceticSoftware.CodeGenerator.StarterKit.Infrastructure;
using CopaceticSoftware.CodeGenerator.StarterKit.Infrastructure.VisualStudioSolution;
using CopaceticSoftware.CodeGenerator.StarterKit.Logging;
using log4net;
using ObjectDumper;

namespace CopaceticSoftware.pMixins.VisualStudio.CodeGenerators
{
    public interface IVisualStudioCodeGenerator
    {
        IEnumerable<CodeGeneratorResponse> GenerateCode(IEnumerable<RawSourceFile> rawSourceFiles);
        IEnumerable<CodeGeneratorResponse> GenerateCode(IEnumerable<ICodeGeneratorContext> codeGeneratorContexts);
    }

    public class VisualStudioCodeGenerator : IVisualStudioCodeGenerator
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IVisualStudioWriter _visualStudioWriter;
        private readonly ICodeGeneratorContextFactory _codeGeneratorContextFactory;
        private readonly IPartialCodeGenerator _codeGenerator;
        private readonly IVisualStudioEventProxy _visualStudioEventProxy;
        
        public VisualStudioCodeGenerator(IVisualStudioWriter visualStudioWriter, IPartialCodeGenerator codeGenerator, ICodeGeneratorContextFactory codeGeneratorContextFactory, IVisualStudioEventProxy visualStudioEventProxy)
        {
            _visualStudioWriter = visualStudioWriter;
            _codeGenerator = codeGenerator;
            _codeGeneratorContextFactory = codeGeneratorContextFactory;
            _visualStudioEventProxy = visualStudioEventProxy;
        }

        public IEnumerable<CodeGeneratorResponse> GenerateCode(IEnumerable<RawSourceFile> rawSourceFiles)
        {
            return GenerateCode(_codeGeneratorContextFactory.GenerateContext(rawSourceFiles.ToList()));
        }

        public IEnumerable<CodeGeneratorResponse> GenerateCode(IEnumerable<ICodeGeneratorContext> codeGeneratorContexts)
        {
            _visualStudioWriter.ClearErrorList();

            foreach (var context in codeGeneratorContexts)
            {
                Log.InfoFormat("Generating Code for file [{0}] in [{1}]",
                        context.Source.FileName, context.Source.Project.FileName);

                if (Log.IsDebugEnabled)
                {
                    Log.DebugFormat("Solution Dump: {1}{1}{0}{1}{1}",
                        context.Solution.DumpForLogging(),
                        Environment.NewLine);
                }
                
                yield return GenerateCode(context);
            }
        }

        private CodeGeneratorResponse GenerateCode(ICodeGeneratorContext context)
        {
            using (new LoggingActivity("GenerateCode " + context.Source.FileName))
            try
            {
                _visualStudioWriter.WriteToStatusBar("pMixin - Generating Code Behind for " + context.Source.FileName);

                var response = _codeGenerator.GeneratePartialCode(context);

                _visualStudioEventProxy.FireOnCodeGenerated(this, response);

                #region Write Errors / Warnings

                foreach (var error in response.Errors)
                    switch (error.Severity)
                    {
                        case CodeGenerationError.SeverityOptions.Error:
                            _visualStudioWriter.GeneratorError(error.Message, error.Line, error.Column);
                            Log.Error("Code Generator Registered Error: " + error.Message);
                            break;

                        case CodeGenerationError.SeverityOptions.Warning:
                            _visualStudioWriter.GeneratorWarning(error.Message, error.Line, error.Column);
                            Log.Warn("Code Generator Registered Warning: " + error.Message);
                            break;

                        case CodeGenerationError.SeverityOptions.Message:
                            _visualStudioWriter.GeneratorMessage(error.Message, error.Line, error.Column);
                            Log.InfoFormat("Code Generator Registered Message: " + error.Message);
                            break;
                    }

                #endregion

                Log.DebugFormat("Generated Code for File [{0}]: {1}{1}{2}{1}{1}",
                    context.Source.FileName,
                    Environment.NewLine,
                    response.GeneratedCodeSyntaxTree.GetText());

                return response;
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

                return new CodeGeneratorResponse(); 
            }
            finally
            {
                _visualStudioWriter.WriteToStatusBar("pMixin - Generating Code Behind for " + context.Source.FileName + " ... complete");
            }
        }
    }
}
