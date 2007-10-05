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
using System.Reflection;
using Ensurance;
using Ensurance.Constraints;
using log4net;

namespace ExtendingFluentInterface
{
    // 1. Declare your class and implement IEnsuranceHandler. This is not actually
    // necessary, except that it makes sure that you have at least implemented one
    // 'Handle' method guiding you to make the static one. The issue is that in a 
    // static API, there is not ability to make virtual calls. The EnsureBase<T> class
    // has to make assumptions concerning your code and this is just a way to guide you.

    // 2. Declare a preprocessor to make your code non user if you do not want
    // debugging to enter your code
#if !DEBUG
    [System.Diagnostics.DebuggerNonUserCode]
#endif

    internal class LoggingEnsuranceHandler : IEnsuranceHandler
    {
        // 3. Create a logger for use in this class
        private static readonly ILog log = LogManager.GetLogger( MethodBase.GetCurrentMethod().DeclaringType );

        // 4. Implement the interface explicitly. If you like you can add the
        // EditorBrowsable attribute to hide the call from Intellisense. This will
        // be called automatically.

        #region IEnsuranceHandler Members

        [EditorBrowsable( EditorBrowsableState.Never )]
        void IEnsuranceHandler.Handle( Constraint constraint, string message, params object[] args )
        {
            // 5. Do your logging.
            if ( log.IsErrorEnabled )
            {
                log.Error( constraint.ToString() );
                log.WarnFormat( message, args );
            }
        }

        #endregion
    }
}