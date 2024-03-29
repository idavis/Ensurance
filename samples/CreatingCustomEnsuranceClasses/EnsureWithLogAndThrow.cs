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
using Ensurance;
using Ensurance.Constraints;
using Ensurance.ResponsibilityChainLinks;
using CreatingCustomEnsuranceClasses.Properties;

namespace CreatingCustomEnsuranceClasses
{
    // 1. Declare your class 'X' and have it extend EnsureBase<X>; implement IEnsuranceHandler.
    // This is not actually necessary, except that it makes sure that you have at least implemented one
    // 'Handle' method guiding you to make the static one. The issue is that in a 
    // static API, there is not ability to make virtual calls. The EnsureBase<T> class
    // has to make assumptions concerning your code and this is just a way to guide you.

    // 2. Declare a preprocessor to make your code non user if you do not want
    // debugging to enter your code
#if !DEBUG
    [DebuggerNonUserCode]
#endif
    public class EnsureWithLogAndThrow : EnsureBase<EnsureWithLogAndThrow>, IEnsuranceHandler
    {
        // 3. Create the initial handler chain link
        private static IEnsuranceResponsibilityChainLink _handler;

        // 4. We want to do some static initialization that cannot be done outside
        // of the static constructor.
        static EnsureWithLogAndThrow()
        {
            _handler = new LoggingEnsuranceHandler();
            _handler.Successor = new ExceptionEnsuranceHandler();
        }

        // 5. Create a protected constructor (private if you want to prevent inhereted instances).
        // We do not want a public facing constructor as the entire API should be static.
        protected EnsureWithLogAndThrow()
        {
        }

        // 6. Implement the interface explicitly. If you like you can add the
        // EditorBrowsable attribute to hide the call from Intellisense. You will,
        // never call this method as it is non-static.

        #region IEnsuranceHandler Members

        [EditorBrowsable( EditorBrowsableState.Never )]
        void IEnsuranceHandler.Handle( Constraint constraint, string message, params object[] args )
        {
            // 7. We never want this call to be made. It is non static, but it forces us
            // to see that we need a Handle method.
            throw new EnsuranceException( Resources.IEnsuranceHandlerDoNotUse );
        }

        #endregion

        // 8. 'Implement' the interface with a private static version with which you
        // will do all of your work.
        private static void Handle( Constraint constraint, string message, params object[] args )
        {
            // 9. Process your handler chain
            _handler.Handle( constraint, message, args );
        }
    }
}