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

using System.ComponentModel;
using System.Diagnostics;
using Ensurance.Constraints;
using Ensurance.Properties;

namespace Ensurance
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
#if !DEBUG
    [DebuggerNonUserCode]
#endif
    public class EnsuranceHandlerContainer<T> : EnsureBase<EnsuranceHandlerContainer<T>>, IEnsuranceHandler where T : IEnsuranceHandler, new()
    {
        private static readonly T _handler = new T();

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EnsuranceHandlerContainer&lt;T&gt;"/> class.
        /// </summary>
        protected EnsuranceHandlerContainer()
        {
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
            throw new EnsuranceException( Resources.IEnsuranceHandlerDoNotUse );
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
            _handler.Handle( constraint, message, args );
        }
    }
}