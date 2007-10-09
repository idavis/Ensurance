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

using System.Globalization;
using System.IO;
using Ensurance.Constraints;
using Ensurance.MessageWriters;

namespace Ensurance.ResponsibilityChainLinks
{
    /// <summary>
    /// 
    /// </summary>
    public class ExceptionEnsuranceHandler : TextMessageWriter
    {
        /// <summary>
        /// Handles an Ensurance failure for the given constraint. Implementors
        /// should always call 
        /// <code>
        /// IEnsuranceResponsibilityChainLink handler = successor;
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
        public override void Handle( Constraint constraint, string message, params object[] args )
        {
            try
            {
                MessageWriter messageWriter = new TextMessageWriter( new StringWriter( CultureInfo.CurrentCulture ) );
                constraint.WriteMessageTo( messageWriter );
                throw new EnsuranceException( messageWriter.ToString() );
            }
            finally
            {
                IEnsuranceResponsibilityChainLink handler = Successor;
                if ( handler != null )
                {
                    handler.Handle( constraint, message, args );
                }
            }
        }
    }
}