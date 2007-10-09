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

using System.Reflection;
using Ensurance;
using Ensurance.Constraints;
using log4net;

namespace ExtendingFluentInterface
{
    // 1. Declare your class and implement IEnsuranceHandler.

    // 2. Declare a preprocessor to make your code non user if you do not want
    // debugging to enter your code
#if !DEBUG
    [DebuggerNonUserCode]
#endif

    internal class LoggingEnsuranceHandler : IEnsuranceHandler
    {
        // 3. Create a logger for use in this class
        private static readonly ILog _log = LogManager.GetLogger( MethodBase.GetCurrentMethod().DeclaringType );

        // 4. Implement the interface explicitly.

        #region IEnsuranceHandler Members

        void IEnsuranceHandler.Handle( Constraint constraint, string message, params object[] args )
        {
            // 5. Do your logging.
            if ( _log.IsErrorEnabled )
            {
                _log.Error( constraint.ToString() );
                _log.WarnFormat( message, args );
            }
        }

        #endregion
    }
}