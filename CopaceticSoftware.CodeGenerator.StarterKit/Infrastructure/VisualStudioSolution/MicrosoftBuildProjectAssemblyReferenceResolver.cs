﻿//----------------------------------------------------------------------- 
// <copyright file="MicrosoftBuildProjectAssemblyReferenceResolver.cs" company="Copacetic Software"> 
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

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CopaceticSoftware.CodeGenerator.StarterKit.Infrastructure.IO;
using CopaceticSoftware.Common.Infrastructure;
using ICSharpCode.NRefactory.TypeSystem;
using ICSharpCode.NRefactory.Utils;
using log4net;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Framework;
using Microsoft.Build.Logging;
using ITaskFactory = CopaceticSoftware.CodeGenerator.StarterKit.Threading.ITaskFactory;

namespace CopaceticSoftware.CodeGenerator.StarterKit.Infrastructure.VisualStudioSolution
{
    public interface IMicrosoftBuildProjectAssemblyReferenceResolver
    {
        IAssemblyReference[] ResolveReferences(Project project);
    }

    public class CachedMicrosoftBuildProjectAssemblyReferenceResolver : MicrosoftBuildProjectAssemblyReferenceResolver
    {
        private static ConcurrentDictionary<FilePath, IAssemblyReference[]> _cache =
            new ConcurrentDictionary<FilePath, IAssemblyReference[]>();

        private readonly IMicrosoftBuildProjectLoader _buildProjectLoader;
        private readonly ITaskFactory _taskFactory;

        public CachedMicrosoftBuildProjectAssemblyReferenceResolver(IVisualStudioEventProxy visualStudioEventProxy, IMicrosoftBuildProjectLoader buildProjectLoader, ITaskFactory taskFactory)
        {
            _buildProjectLoader = buildProjectLoader;
            _taskFactory = taskFactory;
            WireUpVisualStudioEvents(visualStudioEventProxy);
        }

        private void WireUpVisualStudioEvents(IVisualStudioEventProxy visualStudioEventProxy)
        {
            IAssemblyReference[] dummy;

            visualStudioEventProxy.OnSolutionClosing +=
                (sender, args) =>
                {
                    _log.Info("Solution closing.  Clearing cache");
                    _cache = new ConcurrentDictionary<FilePath, IAssemblyReference[]>();
                };

            visualStudioEventProxy.OnProjectAdded +=
                (sender, args) =>
                {
                    if (_cache.TryRemove(args.ProjectFullPath, out dummy))
                        _log.InfoFormat("Evicted [{0}]", args.ProjectFullPath);

                    //This can cause problems if the project added event is in response to 
                    //a project reload.
                    //EagerlyResolveReferences(args.ProjectFullPath);
                };

            visualStudioEventProxy.OnProjectReferenceAdded +=
                (sender, args) =>
                {
                    if (_cache.TryRemove(args.ProjectFullPath, out dummy))
                        _log.InfoFormat("Evicted [{0}]", args.ProjectFullPath);

                    EagerlyResolveReferences(args.ProjectFullPath);
                };

            visualStudioEventProxy.OnProjectReferenceRemoved +=
                (sender, args) =>
                {
                    if (_cache.TryRemove(args.ProjectFullPath, out dummy))
                        _log.InfoFormat("Evicted [{0}]", args.ProjectFullPath);

                    EagerlyResolveReferences(args.ProjectFullPath);
                };

            visualStudioEventProxy.OnProjectRemoved +=
                (sender, args) =>
                {
                    if (_cache.TryRemove(args.ProjectFullPath, out dummy))
                        _log.InfoFormat("Evicted [{0}]", args.ProjectFullPath);
                };
        }

        private void EagerlyResolveReferences(FilePath filename)
        {
            if (filename.IsNullOrEmpty())
                return;

            //TODO: Fix race condition on ResoleReferences method before re-enabling
            //_taskFactory.StartNew(
            //    () =>
            //    {
            //        _log.InfoFormat("Eagerly Resolve References for [{0}]", filename);
            //
            //        ResolveReferences(_buildProjectLoader.LoadMicrosoftBuildProject(filename));
            //    });
        }

        public override IAssemblyReference[] ResolveReferences(Project project)
        {
            return _cache.GetOrAdd(new FilePath(project.FullPath), f =>
                        base.ResolveReferences(project));
        }
    }
    

    //Should be singleton
    public class MicrosoftBuildProjectAssemblyReferenceResolver : IMicrosoftBuildProjectAssemblyReferenceResolver
    {
        protected static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly ConcurrentDictionary<string, IUnresolvedAssembly> _assemblyDict =
                new ConcurrentDictionary<string, IUnresolvedAssembly>(Platform.FileNameComparer);

        private static object _ResolveAssemblyReferencesLock = new object();

        public virtual IAssemblyReference[] ResolveReferences(Project project)
        {
            Ensure.ArgumentNotNull(project, "project");
            Ensure.ArgumentNotNullOrEmpty(project.FullPath, "project.FullPath");

            var sw = Stopwatch.StartNew();
            try
            {
                return ResolveAssemblyReferences(project)
                    .Union<IAssemblyReference>(ResolveProjectReferences(project))
                    .ToArray();
            }
            finally
            {
                _log.DebugFormat("References Resolved for [{0}] in [{1}] ms",
                    Path.GetFileName(project.FullPath),
                    sw.ElapsedMilliseconds);
            }
        }

        protected virtual IEnumerable<IUnresolvedAssembly> ResolveAssemblyReferences(Project project)
        {
            lock (_ResolveAssemblyReferencesLock)
            {
                var baseDirectory = Path.GetDirectoryName(project.FullPath);

                if (null == baseDirectory)
                {
                    _log.WarnFormat("Failed to get Directory Name from [{0}]", project.FullPath);
                    return Enumerable.Empty<IUnresolvedAssembly>();
                }

                // Use MSBuild to figure out the full path of the referenced assemblies
                var projectInstance = project.CreateProjectInstance();
                projectInstance.SetProperty("BuildingProject", "false");
              
                projectInstance.Build("ResolveAssemblyReferences", new[] {new ConsoleLogger(LoggerVerbosity.Minimal)});

                var items = projectInstance.GetItems("_ResolveAssemblyReferenceResolvedFiles");

                return 
                    items.Select(
                        i => LoadAssembly(Path.Combine(baseDirectory, i.GetMetadataValue("Identity"))))
                    .ToArray();
            }
        }

        protected virtual IEnumerable<ProjectReference> ResolveProjectReferences(Project project)
        {
            foreach (var item in project.GetItems("ProjectReference"))
            {
                string referencedFileName = Path.Combine(project.DirectoryPath, item.EvaluatedInclude);

                // Normalize the path; this is required to match the name with the referenced project's file name
                referencedFileName = Path.GetFullPath(referencedFileName);

                yield return new ProjectReference(referencedFileName);
            }
        }


        protected IUnresolvedAssembly LoadAssembly(string assemblyFileName)
        {
            return _assemblyDict.GetOrAdd(
                assemblyFileName, file => new CecilLoader().LoadAssemblyFile(file));
        }
    }
}
