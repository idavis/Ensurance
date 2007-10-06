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

namespace CreatingCustomEnsuranceClasses
{
    // 1. Declare your class and implement IEnsuranceResponsibilityChainLink.

    // 2. Declare a preprocessor to make your code non user if you do not want
    // debugging to enter your code
#if !DEBUG
    [System.Diagnostics.DebuggerNonUserCode]
#endif

    internal class LoggingEnsuranceHandler : IEnsuranceResponsibilityChainLink
    {
        // 3. Create a logger for use in this class
        private static readonly ILog log = LogManager.GetLogger( MethodBase.GetCurrentMethod().DeclaringType );

        // 4. Create a pointer to the next link in the responsibility chain.
        private static IEnsuranceResponsibilityChainLink _successor;

        #region IEnsuranceResponsibilityChainLink Members

        public IEnsuranceResponsibilityChainLink Successor
        {
            get { return _successor; }
            set { _successor = value; }
        }

        // 5. Implement the interface explicitly.

        void IEnsuranceHandler.Handle( Constraint constraint, string message, params object[] args )
        {
            // 5. Do your logging.
            try
            {
                if ( log.IsErrorEnabled )
                {
                    log.Error( constraint.ToString() );
                    log.WarnFormat( message, args );
                }
            }
            finally
            {
                // 6. ALWAYS pass the call onto your successor.
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