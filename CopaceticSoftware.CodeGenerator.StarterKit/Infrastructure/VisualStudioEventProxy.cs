﻿//----------------------------------------------------------------------- 
// <copyright file="VisualStudioEventProxy.cs" company="Copacetic Software"> 
// Copyright (c) Copacetic Software.  
// <author>Philip Pittle</author> 
// <date>Wednesday, April 30, 2014 5:48:10 PM</date> 
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
using System.IO;
using CopaceticSoftware.CodeGenerator.StarterKit.Extensions;
using EnvDTE;
using EnvDTE80;
using VSLangProj;

namespace CopaceticSoftware.CodeGenerator.StarterKit.Infrastructure
{
    public interface IVisualStudioEventProxy : IDisposable
    {
        event EventHandler<ProjectAddedEventArgs> OnProjectAdded;
        event EventHandler<ProjectRemovedEventArgs> OnProjectRemoved;
        event EventHandler<ProjectReferenceAddedEventArgs> OnProjectReferenceAdded;
        event EventHandler<ProjectReferenceRemovedEventArgs> OnProjectReferenceRemoved;

        event EventHandler<ProjectItemAddedEventArgs> OnProjectItemAdded;
        event EventHandler<ProjectItemRemovedEventArgs> OnProjectItemRemoved;
        event EventHandler<ProjectItemRenamedEventArgs> OnProjectItemRenamed;
        event EventHandler<ProjectItemOpenedEventArgs> OnProjectItemOpened;
        event EventHandler<ProjectItemSavedEventArgs> OnProjectItemSaved;

        event EventHandler<VisualStudioBuildEventArgs> OnBuildBegin;
        event EventHandler<VisualStudioBuildEventArgs> OnBuildDone;

        event EventHandler<EventArgs> OnSolutionClosing;
        event EventHandler<EventArgs> OnSolutionOpening;
    }

    #region Event Arg Definitions

    #region Abstract Base Classes
    [Serializable]
    public abstract class VisualStudioEventArgs : EventArgs
    {
        public string ProjectFullPath { get; set; }
        public abstract string GetDebugString();
    }

    [Serializable]
    public abstract class VisualStudioProjectEventArgs : VisualStudioEventArgs { }

    [Serializable]
    public abstract class VisualStudioProjectReferenceEventArgs : VisualStudioEventArgs
    {
        public string ReferencePath { get; set; }
    }

    [Serializable]
    public abstract class VisualStudioClassEventArgs : VisualStudioEventArgs
    {
        public string ClassFullPath { get; set; }

        public bool IsCSharpFile()
        {
            if (string.IsNullOrEmpty(ClassFullPath))
                return false;

            try
            {
                var ext = Path.GetExtension(ClassFullPath);

                return null == ext
                    ? false
                    : ext.Equals("cs", StringComparison.InvariantCultureIgnoreCase);
            }
            catch
            {
                return false;
            }
        }
    }
    #endregion

    #region Project EventArgs
    [Serializable]
    public class ProjectAddedEventArgs : VisualStudioProjectEventArgs
    {
        public override string GetDebugString()
        {
            return string.Format(
                Strings.VisualStudioEventProjectAddedDebugString, ProjectFullPath);
        }
    }

    [Serializable]
    public class ProjectRemovedEventArgs : VisualStudioProjectEventArgs
    {
        public override string GetDebugString()
        {
            return string.Format(
                Strings.VisualStudioEventProjectRemovedDebugString, ProjectFullPath);
        }
    }

    [Serializable]
    public class ProjectReferenceAddedEventArgs : VisualStudioProjectReferenceEventArgs
    {
        public override string GetDebugString()
        {
            return string.Format(
                Strings.VisualStudioEventProjectReferenceAddedDebugString,
                ProjectFullPath,
                ReferencePath);
        }
    }

    [Serializable]
    public class ProjectReferenceRemovedEventArgs : VisualStudioProjectReferenceEventArgs
    {
        public override string GetDebugString()
        {
            return string.Format(
                Strings.VisualStudioEventProjectReferenceRemovedDebugString,
                ProjectFullPath,
                ReferencePath);
        }
    }


    #endregion

    #region Project Item Event Args
    [Serializable]
    public class ProjectItemAddedEventArgs : VisualStudioClassEventArgs
    {
        public override string GetDebugString()
        {
            return string.Format(
                Strings.VisualStudioEventClassAddedDebugString,
                ProjectFullPath,
                ClassFullPath);
        }
    }

    [Serializable]
    public class ProjectItemRemovedEventArgs : VisualStudioClassEventArgs
    {
        public override string GetDebugString()
        {
            return string.Format(
                Strings.VisualStudioEventClassRemovedDebugString,
                ProjectFullPath,
                ClassFullPath);
        }
    }

    [Serializable]
    public class ProjectItemRenamedEventArgs : VisualStudioClassEventArgs
    {
        public string OldClassFileName { get; set; }

        public override string GetDebugString()
        {
            return string.Format(
                Strings.VisualStudioEventClassRenamedDebugString,
                ProjectFullPath,
                OldClassFileName,
                ClassFullPath);
        }

    }

    [Serializable]
    public class ProjectItemOpenedEventArgs : VisualStudioClassEventArgs
    {
        public override string GetDebugString()
        {
            return string.Format(
                Strings.VisualStudioEventClassOpenedDebugString,
                ProjectFullPath,
                ClassFullPath);
        }
    }

    [Serializable]
    public class ProjectItemSavedEventArgs : VisualStudioClassEventArgs
    {
        public override string GetDebugString()
        {
            return string.Format(
                Strings.VisualStudioEventClassSavedDebugString,
                ProjectFullPath,
                ClassFullPath);
        }
    }
    #endregion

    #region Build Events

    public class VisualStudioBuildEventArgs : VisualStudioEventArgs
    {
        public vsBuildScope Scope { get; set; }
        public vsBuildAction BuildAction { get; set; }

        public override string GetDebugString()
        {
            return Strings.VisualStudioBuildEvent;
        }
    }
    #endregion

    #endregion

    public class VisualStudioEventProxy : IVisualStudioEventProxy
    {
        #region IVisualStudioEventProxy Event Declarations
        public event EventHandler<ProjectAddedEventArgs> OnProjectAdded;
        public event EventHandler<ProjectRemovedEventArgs> OnProjectRemoved;
        public event EventHandler<ProjectReferenceAddedEventArgs> OnProjectReferenceAdded;
        public event EventHandler<ProjectReferenceRemovedEventArgs> OnProjectReferenceRemoved;

        public event EventHandler<ProjectItemAddedEventArgs> OnProjectItemAdded;
        public event EventHandler<ProjectItemRemovedEventArgs> OnProjectItemRemoved;
        public event EventHandler<ProjectItemRenamedEventArgs> OnProjectItemRenamed;
        public event EventHandler<ProjectItemOpenedEventArgs> OnProjectItemOpened;
        public event EventHandler<ProjectItemSavedEventArgs> OnProjectItemSaved;

        public event EventHandler<VisualStudioBuildEventArgs> OnBuildBegin;
        public event EventHandler<VisualStudioBuildEventArgs> OnBuildDone;

        public event EventHandler<EventArgs> OnSolutionClosing;
        public event EventHandler<EventArgs> OnSolutionOpening;
        #endregion

        /// <summary>
        /// Keep an instance of the dte objects to make sure it doesn't get GC'd
        /// http://stackoverflow.com/questions/5405167/dte2-events-dont-fire
        /// </summary>
        private DTE2 _dte;
        private SolutionEvents _solutionEvents;
        private DocumentEvents _documentEvents;
        private ProjectItemsEvents _projectItemsEvents;
        private BuildEvents _buildEvents;
        

        private readonly Dictionary<string, ReferencesEvents> _projectSpecificReferenceEvents = 
            new Dictionary<string, ReferencesEvents>();
        
        public VisualStudioEventProxy(DTE2 dte)
        {
            _dte = dte;

            _buildEvents = _dte.Events.BuildEvents;
            _solutionEvents = _dte.Events.SolutionEvents;
            _documentEvents = _dte.Events.DocumentEvents;
            _projectItemsEvents = ((Events2)dte.Events).ProjectItemsEvents;

            AddDefaultEventHandlers();

            RegisterForSolutionLevelEvents();

            foreach (Project project in _dte.Solution.Projects)
            {
                RegisterForProjectLevelEvents(project.Object as VSProject);
            }
        }

        private void RegisterForSolutionLevelEvents()
        {
            _solutionEvents.ProjectAdded += project =>
                    {
                        RegisterForProjectLevelEvents(project.Object as VSProject);
                        OnProjectAdded(this, new ProjectAddedEventArgs {ProjectFullPath = project.FullName});
                    };

            _solutionEvents.ProjectRemoved += project =>
                {
                    if (string.IsNullOrEmpty(project.FullName))
                        return;

                    UnregisterForProjectLevelEvents(project as VSProject);
                    OnProjectRemoved(this, new ProjectRemovedEventArgs { ProjectFullPath = project.FullName });
                };

            _solutionEvents.BeforeClosing += () => OnSolutionClosing(this, new EventArgs());

            _solutionEvents.Opened += () => OnSolutionOpening(this, new EventArgs());

            _projectItemsEvents.ItemAdded += item =>
                OnProjectItemAdded(this, new ProjectItemAddedEventArgs { ProjectFullPath = item.ContainingProject.FullName, ClassFullPath = item.Name });

            _projectItemsEvents.ItemRemoved += item =>
                OnProjectItemRemoved(this, new ProjectItemRemovedEventArgs { ProjectFullPath = item.ContainingProject.FullName, ClassFullPath = item.Name });

            _projectItemsEvents.ItemRenamed += (item, name) =>
                OnProjectItemRenamed(this, new ProjectItemRenamedEventArgs
                    {
                        ProjectFullPath = item.ContainingProject.FullName, 
                        ClassFullPath = item.Name,
                        OldClassFileName = name
                    });

            _documentEvents.DocumentOpened += item =>
                OnProjectItemOpened(this, new ProjectItemOpenedEventArgs{ ProjectFullPath = item.ProjectItem.ContainingProject.FullName, ClassFullPath = item.Name });

            _documentEvents.DocumentSaved += item =>
                OnProjectItemSaved(this, new ProjectItemSavedEventArgs{ ProjectFullPath = item.ProjectItem.ContainingProject.FullName, ClassFullPath = item.Name });

            _buildEvents.OnBuildBegin += (scope, action) =>
                OnBuildBegin(this, new VisualStudioBuildEventArgs {Scope = scope, BuildAction = action});

            _buildEvents.OnBuildDone += (scope, action) =>
                 OnBuildDone(this, new VisualStudioBuildEventArgs { Scope = scope, BuildAction = action });
        }

        private void RegisterForProjectLevelEvents(VSProject project)
        {

            if (null == project || _projectSpecificReferenceEvents.ContainsKey(project.Project.FullName))
                return;

            var projectFullName = project.Project.FullName;
            
            #region Reference Events
            var referenceEvents = project.Events.ReferencesEvents;

            referenceEvents.ReferenceAdded += reference =>
                OnProjectReferenceAdded(this, new ProjectReferenceAddedEventArgs { ReferencePath = reference.Path, ProjectFullPath = projectFullName });
            
            referenceEvents.ReferenceRemoved += reference =>
                OnProjectReferenceRemoved(this, new ProjectReferenceRemovedEventArgs { ReferencePath = reference.Path, ProjectFullPath = projectFullName });

            _projectSpecificReferenceEvents.Add(projectFullName, referenceEvents);
            #endregion
        }

        private void UnregisterForProjectLevelEvents(VSProject project)
        {
            if (null == project || !_projectSpecificReferenceEvents.ContainsKey(project.Project.FullName))
                return;

            _projectSpecificReferenceEvents.Remove(project.Project.FullName);
        }


        #region Misc Methods
        private void AddDefaultEventHandlers()
        {
            OnProjectAdded += (s, a) => { };
            OnProjectRemoved += (s, a) => { };
            OnProjectReferenceAdded += (s, a) => { };
            OnProjectReferenceRemoved += (s, a) => { };
            OnProjectItemAdded += (s, a) => { };
            OnProjectItemRemoved += (s, a) => { };
            OnProjectItemRenamed += (s, a) => { };
            OnProjectItemOpened += (s, a) => { };
            OnProjectItemSaved += (s, a) => { };
            OnBuildBegin += (s, a) => { };
            OnBuildDone += (s, a) => { };
            OnSolutionClosing += (s, a) => { };
            OnSolutionOpening += (s, a) => { };
        }
        
        public void Dispose()
        {
            _dte = null;
            _solutionEvents = null;
            _documentEvents = null;
            _projectItemsEvents = null;

            OnProjectAdded.GetInvocationList().Map(del => OnProjectAdded -= (EventHandler<ProjectAddedEventArgs>)del);
            OnProjectRemoved.GetInvocationList().Map(del => OnProjectRemoved -= (EventHandler<ProjectRemovedEventArgs>)del);
            OnProjectReferenceAdded.GetInvocationList().Map(del => OnProjectReferenceAdded -= (EventHandler<ProjectReferenceAddedEventArgs>)del);
            OnProjectReferenceRemoved.GetInvocationList().Map(del => OnProjectReferenceRemoved -= (EventHandler<ProjectReferenceRemovedEventArgs>)del);
            OnProjectItemAdded.GetInvocationList().Map(del => OnProjectItemAdded -= (EventHandler<ProjectItemAddedEventArgs>)del);
            OnProjectItemRemoved.GetInvocationList().Map(del => OnProjectItemRemoved -= (EventHandler<ProjectItemRemovedEventArgs>)del);
            OnProjectItemRenamed.GetInvocationList().Map(del => OnProjectItemRenamed -= (EventHandler<ProjectItemRenamedEventArgs>)del);
            OnProjectItemOpened.GetInvocationList().Map(del => OnProjectItemOpened -= (EventHandler<ProjectItemOpenedEventArgs>)del);
            OnProjectItemSaved.GetInvocationList().Map(del => OnProjectItemSaved -= (EventHandler<ProjectItemSavedEventArgs>)del);
            OnBuildBegin.GetInvocationList().Map(del => OnBuildBegin -= (EventHandler<VisualStudioBuildEventArgs>)del);
            OnBuildDone.GetInvocationList().Map(del => OnBuildDone -= (EventHandler<VisualStudioBuildEventArgs>)del);

        }
        #endregion
    }
}
