﻿//----------------------------------------------------------------------- 
// <copyright file="VisualStudioOpenDocumentManager.cs" company="Copacetic Software"> 
// Copyright (c) Copacetic Software.  
// <author>Philip Pittle</author> 
// <date>Thursday, May 8, 2014 10:19:06 PM</date> 
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
using System.Reflection;
using JetBrains.Annotations;
using log4net;

namespace CopaceticSoftware.CodeGenerator.StarterKit.Infrastructure.IO
{
    public interface IVisualStudioOpenDocumentManager
    {
        bool IsDocumentOpen(FilePath filename);

        [CanBeNull]
        IVisualStudioOpenDocumentReader GetOpenDocument(FilePath filename);
    }

    public class VisualStudioOpenDocumentManager : IVisualStudioOpenDocumentManager
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        //string / document reader
        private static ConcurrentDictionary<FilePath, IVisualStudioOpenDocumentReader> _openDocuments
            = new ConcurrentDictionary<FilePath, IVisualStudioOpenDocumentReader>();

        public VisualStudioOpenDocumentManager(IVisualStudioEventProxy eventProxy)
        {
            eventProxy.OnProjectItemOpened += (sender, args) =>
            {
                _log.InfoFormat("Document Opened [{0}]", args.ClassFullPath);

                _openDocuments.AddOrUpdate(
                    args.ClassFullPath,
                    (x) => args.DocumentReader,
                    (x, y) => args.DocumentReader);
            };

            eventProxy.OnProjectItemClosed += (sender, args) =>
            {
                _log.InfoFormat("Document Closed [{0}]", args.ClassFullPath);

                IVisualStudioOpenDocumentReader dummy;

                if (!_openDocuments.TryRemove(args.ClassFullPath, out dummy))
                    _log.WarnFormat("Recevied a Close event but Window was not in Cache [{0}]", 
                        args.ClassFullPath);
            };

            eventProxy.OnSolutionClosing += (sender, args) =>
            {
                _log.Info("Solution Closing.  Clearing Cache");

                _openDocuments = new ConcurrentDictionary<FilePath, IVisualStudioOpenDocumentReader>();
            };
        }

        public bool IsDocumentOpen(FilePath filename)
        {
            return _openDocuments.ContainsKey(filename);
        }

        public IVisualStudioOpenDocumentReader GetOpenDocument(FilePath filename)
        {
            IVisualStudioOpenDocumentReader dummy;

            _openDocuments.TryGetValue(filename, out dummy);

            return dummy;
        }
    }
}
