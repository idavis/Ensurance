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

using Ensurance;

namespace ExtendingFluentInterface
{
    // 1. Create a partial implementation of the Ensure class.
    internal partial class Ensure
    {
        // 2. Create a partial implementation of the With class.

        #region Nested type: With

        public partial class With
        {
            // 3. Declare a preprocessor to make your code non user if you do not want
            // debugging to enter your code
#if !DEBUG
            [System.Diagnostics.DebuggerNonUserCode]
#endif
            // 4. Declare your class. You can make it partial if you like, but it is not
            // required. You must extend EnsuranceHandlerContainer specifying a class
            // where <T> implements IEnsuranceHandler and has a default constructor.
            // In this example we are going to use our own class.

            #region Nested type: Logging

            public partial class Logging : EnsuranceHandlerContainer<LoggingEnsuranceHandler>
            {
            }

            #endregion
        }

        #endregion
    }
}