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

using System.Collections;

namespace Ensurance.SyntaxHelpers
{
    /// <summary>
    /// The List class is a helper class with properties and methods that supply
    /// a number of constraints used with lists and collections.
    /// </summary>
    public class List
    {
        /// <summary>
        /// List.Map returns a ListMapper, which can be used to map the original
        /// collection to another collection.
        /// </summary>
        /// <param name="actual"></param>
        /// <returns></returns>
        public static ListMapper Map( ICollection actual )
        {
            return new ListMapper( actual );
        }
    }
}