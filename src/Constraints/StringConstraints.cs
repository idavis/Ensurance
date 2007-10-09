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

using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Ensurance.MessageWriters;

namespace Ensurance.Constraints
{
    /// <summary>
    /// SubstringConstraint can test whether a string contains the expected
    /// substring.
    /// </summary>
    public class SubstringConstraint : Constraint
    {
        private string _expected;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubstringConstraint"/>
        /// class.
        /// </summary>
        /// <param name="expected">The expected.</param>
        public SubstringConstraint( string expected )
        {
            _expected = expected;
        }

        /// <summary>
        /// Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True for success, false for failure</returns>
        public override bool Matches( object actual )
        {
            Actual = actual;

            if ( !( actual is string ) )
            {
                return false;
            }

            if ( CaseInsensitive )
            {
                return ( (string) actual ).ToLower( CultureInfo.CurrentCulture ).IndexOf( _expected.ToLower( CultureInfo.CurrentCulture ) ) >= 0;
            }
            else
            {
                return ( (string) actual ).IndexOf( _expected ) >= 0;
            }
        }

        /// <summary>
        /// Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo( MessageWriter writer )
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }

            writer.WritePredicate( "String containing" );
            writer.WriteExpectedValue( _expected );
            if ( CaseInsensitive )
            {
                writer.WriteModifier( "ignoring case" );
            }
        }
    }

    /// <summary>
    /// StartsWithConstraint can test whether a string starts with an expected
    /// substring.
    /// </summary>
    public class StartsWithConstraint : Constraint
    {
        private string _expected;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartsWithConstraint"/>
        /// class.
        /// </summary>
        /// <param name="expected">The expected string</param>
        public StartsWithConstraint( string expected )
        {
            _expected = expected;
        }

        /// <summary>
        /// Test whether the constraint is matched by the actual value. This is
        /// a template method, which calls the IsMatch method of the derived
        /// class.
        /// </summary>
        /// <param name="actual"></param>
        /// <returns></returns>
        public override bool Matches( object actual )
        {
            Actual = actual;

            if ( !( actual is string ) )
            {
                return false;
            }

            if ( CaseInsensitive )
            {
                return ( (string) actual ).ToLower( CultureInfo.CurrentCulture ).StartsWith( _expected.ToLower( CultureInfo.CurrentCulture ) );
            }
            else
            {
                return ( (string) actual ).StartsWith( _expected );
            }
        }

        /// <summary>
        /// Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo( MessageWriter writer )
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }

            writer.WritePredicate( "String starting with" );
            writer.WriteExpectedValue( MsgUtils.ClipString( _expected, writer.MaxLineLength - 40, 0 ) );
            if ( CaseInsensitive )
            {
                writer.WriteModifier( "ignoring case" );
            }
        }
    }

    /// <summary>
    /// EndsWithConstraint can test whether a string ends with an expected
    /// substring.
    /// </summary>
    public class EndsWithConstraint : Constraint
    {
        private string _expected;

        /// <summary>
        /// Initializes a new instance of the <see cref="EndsWithConstraint"/>
        /// class.
        /// </summary>
        /// <param name="expected">The expected string</param>
        public EndsWithConstraint( string expected )
        {
            _expected = expected;
        }

        /// <summary>
        /// Test whether the constraint is matched by the actual value. This is
        /// a template method, which calls the IsMatch method of the derived
        /// class.
        /// </summary>
        /// <param name="actual"></param>
        /// <returns></returns>
        public override bool Matches( object actual )
        {
            Actual = actual;
            string actualString = actual as string;

            if ( actualString == null )
            {
                return false;
            }

            if ( CaseInsensitive )
            {
                return actualString.ToLower( CultureInfo.CurrentCulture ).EndsWith( _expected.ToLower( CultureInfo.CurrentCulture ) );
            }
            else
            {
                return actualString.EndsWith( _expected );
            }
        }

        /// <summary>
        /// Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo( MessageWriter writer )
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            writer.WritePredicate( "String ending with" );
            writer.WriteExpectedValue( _expected );
            if ( CaseInsensitive )
            {
                writer.WriteModifier( "ignoring case" );
            }
        }
    }

    /// <summary>
    /// RegexConstraint can test whether a string matches the pattern provided.
    /// </summary>
    public class RegexConstraint : Constraint
    {
        private string _pattern;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegexConstraint"/>
        /// class.
        /// </summary>
        /// <param name="pattern">The pattern.</param>
        public RegexConstraint( string pattern )
        {
            _pattern = pattern;
        }

        /// <summary>
        /// Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True for success, false for failure</returns>
        public override bool Matches( object actual )
        {
            Actual = actual;

            return actual is string &&
                   Regex.IsMatch(
                       (string) actual,
                       _pattern,
                       CaseInsensitive ? RegexOptions.IgnoreCase : RegexOptions.None );
        }

        /// <summary>
        /// Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo( MessageWriter writer )
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }

            writer.WritePredicate( "String matching" );
            writer.WriteExpectedValue( _pattern );
            if ( CaseInsensitive )
            {
                writer.WriteModifier( "ignoring case" );
            }
        }
    }
}