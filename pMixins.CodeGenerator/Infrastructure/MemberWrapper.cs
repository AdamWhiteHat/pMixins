﻿//----------------------------------------------------------------------- 
// <copyright file="MemberWrapper.cs" company="Copacetic Software"> 
// Copyright (c) Copacetic Software.  
// <author>Philip Pittle</author> 
// <date>Wednesday, July 23, 2014 10:15:58 AM</date> 
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

using CopaceticSoftware.pMixins.CodeGenerator.Pipelines.GenerateCodeBehind.Pipelines.MixinLevelCodeGenerator.Steps.GenerateMembers;
using CopaceticSoftware.pMixins.CodeGenerator.Pipelines.ResolveAttributes.Infrastructure;
using ICSharpCode.NRefactory.TypeSystem;

namespace CopaceticSoftware.pMixins.CodeGenerator.Infrastructure
{
    /// <summary>
    /// Provides a structure for wrapping 
    /// <see cref="IMember"/>s with Mixin 
    /// specific context.
    /// </summary>
    public class MemberWrapper
    {
        public MemberWrapper(MemberImplementationDetails implementationDetails = null)
        {
            if (null == implementationDetails)
                implementationDetails = new MemberImplementationDetails();

            ImplementationDetails = implementationDetails;
        }

        /// <summary>
        /// Reference to the original member
        /// </summary>
        public IMember Member { get; set; }

        /// <summary>
        /// The Mixin associated with this Member
        /// </summary>
        public pMixinAttributeResolvedResult MixinAttribute { get; set; }

        /// <summary>
        /// The Type that originally declared this 
        /// member.  Can be the <see cref="MixinAttribute"/> or
        /// one of its parent classes.
        /// </summary>
        public IType DeclaringType { get; set; }

        /// <summary>
        /// Strategy for creating this member in the appropriate
        /// wrapper.
        /// </summary>
        //public IImplementationStrategy ImplementationStrategy { get; set; }

        public MemberImplementationDetails ImplementationDetails { get; private set; }
    }

    public class MemberImplementationDetails
    {
        /// <summary>
        /// The Parent <see cref="MemberWrapper"/> this 
        /// <see cref="MemberImplementationDetails"/> is 
        /// associated with.
        /// </summary>
        public MemberWrapper ParentMemberWrapper { get; set; }

        /// <summary>
        /// The name of the <see cref="ParentMemberWrapper"/> when added
        /// to the Requirements Implementation name.
        /// </summary>
        /// <remarks>
        /// Referenced by 
        /// <see cref="GenerateAbstractWrapperMembers.ProcessMembers"/>
        /// </remarks>
        public string RequirementsInterfaceImplementationName { get; set; }

        /// <summary>
        /// If the <see cref="ParentMemberWrapper"/> is protected and abstract,
        /// a special wrapper member needs to be created in order
        /// to promote the member to public in the 
        /// Abstract Wrapper.
        /// </summary>
        public string ProtectedAbstractMemberPromotedToPublicMemberName { get; set; }

    }
}
