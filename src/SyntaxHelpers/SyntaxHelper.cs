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

using Ensurance.Constraints;

namespace Ensurance.SyntaxHelpers
{
    /// <summary>
    /// SyntaxHelper is the abstract base class for all syntax helpers.
    /// </summary>
    public abstract class SyntaxHelper
    {
        #region Prefix Operators

        /// <summary>
        /// Not returns a ConstraintBuilder that negates the constraint that
        /// follows it.
        /// </summary>
        public static ConstraintBuilder Not
        {
            get { return new ConstraintBuilder().Not; }
        }

        /// <summary>
        /// All returns a ConstraintBuilder, which will apply the following
        /// constraint to all members of a collection, succeeding if all of them
        /// succeed.
        /// </summary>
        public static ConstraintBuilder AllItems
        {
            get { return new ConstraintBuilder().All; }
        }

        /// <summary>
        /// SomeItem returns a ConstraintBuilder, which will apply the following
        /// constraint to all members of a collection, succeeding if any of them
        /// succeed.
        /// </summary>
        public static ConstraintBuilder SomeItem
        {
            get { return new ConstraintBuilder().Some; }
        }

        /// <summary>
        /// NoItem returns a ConstraintBuilder, which will apply the following
        /// constraint to all members of a collection, succeeding only if none
        /// of them succeed.
        /// </summary>
        public static ConstraintBuilder NoItem
        {
            get { return new ConstraintBuilder().None; }
        }

        #endregion
    }
}