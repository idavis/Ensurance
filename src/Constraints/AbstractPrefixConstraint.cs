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

namespace Ensurance.Constraints
{
    /// <summary>
    /// Abstract base class used for prefixes
    /// </summary>
    public abstract class AbstractPrefixConstraint : Constraint
    {
        /// <summary>
        /// The base constraint
        /// </summary>
        private Constraint _baseConstraint;

        /// <summary>
        /// Construct given a base constraint
        /// </summary>
        /// <param name="baseConstraint"></param>
        protected AbstractPrefixConstraint( Constraint baseConstraint )
        {
            _baseConstraint = baseConstraint;
        }

        /// <summary>
        /// The base constraint
        /// </summary>
        public Constraint BaseConstraint
        {
            get { return _baseConstraint; }
            set { _baseConstraint = value; }
        }

        /// <summary>
        /// Set all modifiers applied to the prefix into the base constraint
        /// before matching
        /// </summary>
        protected void PassModifiersToBase()
        {
            if ( CaseInsensitive )
            {
                _baseConstraint = _baseConstraint.IgnoreCase;
            }
            if ( Tolerance != null )
            {
                _baseConstraint = _baseConstraint.Within( Tolerance );
            }
            if ( CompareAsCollection )
            {
                _baseConstraint = _baseConstraint.AsCollection;
            }
            if ( CompareWith != null )
            {
                _baseConstraint = _baseConstraint.Comparer( CompareWith );
            }
        }
    }
}