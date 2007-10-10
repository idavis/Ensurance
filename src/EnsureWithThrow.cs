#region Copyright & License

//
// Author: Ian Davis <ian.f.davis@gmail.com> Copyright (c) 2007, Ian Davs
//
// Portions of this software were developed for NUnit. See NOTICE.txt for more
// information. 
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may not
// use this file except in compliance with the License. You may obtain a copy of
// the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
// WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
// License for the specific language governing permissions and limitations under
// the License.
//

#endregion

using Ensurance.ResponsibilityChainLinks;

namespace Ensurance
{
    public partial class Ensure
    {
        #region Nested type: With

        public partial class With
        {
            #region Nested type: Throw

            /// <summary>
            /// 
            /// </summary>
#if !DEBUG
            [System.Diagnostics.DebuggerNonUserCode]
#endif

            public partial class Throw : EnsuranceHandlerContainer<ExceptionEnsuranceHandler>
            {
            }

            #endregion
        }

        #endregion
    }
}