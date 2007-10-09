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

using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Ensurance.Constraints;
using Ensurance.ResponsibilityChainLinks;
using Ensurance.SyntaxHelpers;

namespace Ensurance
{
    /// <summary>
    /// 
    /// </summary>
#if !DEBUG
    [DebuggerNonUserCode]
#endif
    public partial class Ensure : EnsureBase<Ensure>, IEnsuranceHandler
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Ensure"/> class.
        /// </summary>
        protected Ensure()
        {
        }

        #endregion

        #region EnsurenceHandler

        private static IEnsuranceResponsibilityChainLink _handler;

        /// <summary>
        /// Gets or sets the handler.
        /// </summary>
        /// <value>The handler.</value>
        public static IEnsuranceResponsibilityChainLink Handler
        {
            get
            {
                if ( _handler == null )
                {
                    _handler = new ExceptionEnsuranceHandler();
                }
                return _handler;
            }
        }

        /// <summary>
        /// Sets the ensurance handlers.
        /// </summary>
        /// <param name="handlersList">The handlers.</param>
        public static void SetEnsuranceHandlers( IList<IEnsuranceResponsibilityChainLink> handlersList )
        {
            That( handlersList, Is.Not.Null );
            That( handlersList.Count, Is.GreaterThan( 0 ) );
            ProcessEnsuranceHandlers( handlersList );
        }

        /// <summary>
        /// Sets the ensurance handlers.
        /// </summary>
        /// <param name="handlersList">The handlers list.</param>
        public static void SetEnsuranceHandlers( params IEnsuranceResponsibilityChainLink[] handlersList )
        {
            That( handlersList.Length, Is.GreaterThan( 0 ) );
            ProcessEnsuranceHandlers( handlersList );
        }

        /// <summary>
        /// Processes the ensurance handlers.
        /// </summary>
        /// <param name="handlersList">The handlers list.</param>
        public static void ProcessEnsuranceHandlers( IList<IEnsuranceResponsibilityChainLink> handlersList )
        {
            _handler = handlersList[0];
            IEnsuranceResponsibilityChainLink current = _handler;
            for (int i = 1; i < handlersList.Count; i++)
            {
                current.Successor = handlersList[i];
                current = current.Successor;
            }
        }

        #endregion

        #region IEnsuranceHandler Members

        /// <summary>
        /// Handles an Ensurance failure for the given constraint. Implementors
        /// should always call 
        /// <code>
        /// if( successor != null)
        /// {
        ///     successor.Handle( constraint, message, args );
        /// }
        /// </code>
        /// So that the downstream handler can have a chance to process the failure.
        /// </summary>
        /// <param name="constraint">The constraint.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The args.</param>
        [EditorBrowsable( EditorBrowsableState.Never )]
        void IEnsuranceHandler.Handle( Constraint constraint, string message, params object[] args )
        {
            throw new EnsuranceException( "IEnsuranceHandler.Handle should not be used." );
        }

        #endregion

        /// <summary>
        /// Handles an Ensurance failure for the given constraint. Implementors
        /// should always call
        /// <code>
        /// if( successor != null)
        /// {
        ///     successor.Handle( constraint, message, args );
        /// }
        /// </code>
        /// So that the downstream handler can have a chance to process the failure.
        /// </summary>
        /// <param name="constraint">The constraint.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The args.</param>
        private static void Handle( Constraint constraint, string message, params object[] args )
        {
            Handler.Handle( constraint, message, args );
        }

        #region Extensions

#if !DEBUG
        [DebuggerNonUserCode]
#endif

            #region Nested type: With

        public partial class With
        {
        }

        #endregion

        #endregion
    }
}