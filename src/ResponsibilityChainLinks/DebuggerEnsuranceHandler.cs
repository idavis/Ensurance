#region Copyright & License
//
// Author: Ian Davis <ian.f.davis@gmail.com>
// Copyright (c) 2007, Ian Davs
//
// Portions of this software were developed for NUnit.
// See NOTICE.txt for more information. 
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

using System.Diagnostics;
using Ensurance.Constraints;

namespace Ensurance.MessageWriters
{
#if !DEBUG
    [DebuggerNonUserCode]
#endif
    public class DebuggerEnsuranceHandler : IEnsuranceResponsibilityChainLink
    {
        private IEnsuranceResponsibilityChainLink _successor;

        #region IEnsuranceResponsibilityChainLink Members

        /// <summary>
        /// Gets or sets the successor.
        /// </summary>
        /// <value>The successor.</value>
        public IEnsuranceResponsibilityChainLink Successor
        {
            get { return _successor; }
            set { _successor = value; }
        }

        /// <summary>
        /// Handles an Ensurance failure for the given constraint. Implementors
        /// should always call
        /// <code>
        /// if( successor != null)
        /// {
        /// successor.Handle( constraint, message, args );
        /// }
        /// </code>
        /// So that the downstream handler can have a chance to process the failure.
        /// </summary>
        /// <param name="constraint">The constraint.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The args.</param>
        public void Handle( Constraint constraint, string message, params object[] args )
        {
            try
            {
                if ( Debugger.IsAttached )
                {
                    Debugger.Break();
                }
            }
            finally
            {
                IEnsuranceResponsibilityChainLink handler = _successor;
                if ( handler != null )
                {
                    handler.Handle( constraint, message, args );
                }
            }
        }

        #endregion
    }
}