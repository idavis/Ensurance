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
    /// The Text class is a helper class with properties and methods that supply
    /// a number of constraints used with strings.
    /// </summary>
    public class Text
    {
        /// <summary>
        /// Text.All returns a ConstraintBuilder, which will apply the following
        /// constraint to all members of a collection, succeeding if all of them
        /// succeed.
        /// </summary>
        public static ConstraintBuilder All
        {
            get { return new ConstraintBuilder().All; }
        }

        /// <summary>
        /// Contains returns a constraint that succeeds if the actual value
        /// contains the substring supplied as an argument.
        /// </summary>
        public static Constraint Contains( string substring )
        {
            return new SubstringConstraint( substring );
        }

        /// <summary>
        /// DoesNotContain returns a constraint that fails if the actual value
        /// contains the substring supplied as an argument.
        /// </summary>
        public static Constraint DoesNotContain( string substring )
        {
            return new NotConstraint( Contains( substring ) );
        }

        /// <summary>
        /// StartsWith returns a constraint that succeeds if the actual value
        /// starts with the substring supplied as an argument.
        /// </summary>
        public static Constraint StartsWith( string substring )
        {
            return new StartsWithConstraint( substring );
        }

        /// <summary>
        /// DoesNotStartWith returns a constraint that fails if the actual value
        /// starts with the substring supplied as an argument.
        /// </summary>
        public static Constraint DoesNotStartWith( string substring )
        {
            return new NotConstraint( StartsWith( substring ) );
        }

        /// <summary>
        /// EndsWith returns a constraint that succeeds if the actual value ends
        /// with the substring supplied as an argument.
        /// </summary>
        public static Constraint EndsWith( string substring )
        {
            return new EndsWithConstraint( substring );
        }

        /// <summary>
        /// DoesNotEndWith returns a constraint that fails if the actual value
        /// ends with the substring supplied as an argument.
        /// </summary>
        public static Constraint DoesNotEndWith( string substring )
        {
            return new NotConstraint( EndsWith( substring ) );
        }

        /// <summary>
        /// Matches returns a constraint that succeeds if the actual value
        /// matches the pattern supplied as an argument.
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static Constraint Matches( string pattern )
        {
            return new RegexConstraint( pattern );
        }

        /// <summary>
        /// DoesNotMatch returns a constraint that failss if the actual value
        /// matches the pattern supplied as an argument.
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static Constraint DoesNotMatch( string pattern )
        {
            return new NotConstraint( Matches( pattern ) );
        }
    }
}