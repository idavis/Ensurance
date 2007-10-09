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

using System;
using System.Drawing;
using System.IO;
using Ensurance.Constraints;
using Ensurance.MessageWriters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ensurance.Tests.Constraints
{
    [TestClass]
    public class EqualTest : ConstraintTestBase
    {
        private static string testString = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        [TestInitialize]
        public void SetUp()
        {
            Matcher = new EqualConstraint( 4 );
            GoodValues = new object[] {4, 4.0f, 4.0d, 4.0000m};
            BadValues = new object[] {5, null, "Hello"};
            Description = "4";
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void FailedStringMatchShowsFailurePosition()
        {
            Ensure.That( "abcdgfe", new EqualConstraint( "abcdefg" ) );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void LongStringsAreTruncated()
        {
            string expected = testString;
            string actual = testString.Replace( 'k', 'X' );

            Ensure.That( actual, new EqualConstraint( expected ) );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void LongStringsAreTruncatedAtBothEndsIfNecessary()
        {
            string expected = testString;
            string actual = testString.Replace( 'Z', '?' );

            Ensure.That( actual, new EqualConstraint( expected ) );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void LongStringsAreTruncatedAtFrontEndIfNecessary()
        {
            string expected = testString;
            string actual = testString + "+++++";

            Ensure.That( actual, new EqualConstraint( expected ) );
        }

        [TestMethod]
        public void NANsCompareAsEqual()
        {
            Ensure.That( double.NaN, new EqualConstraint( double.NaN ) );
        }

//        [TestMethod]
//        public void NamedAndUnnamedColorsCompareAsEqual()
//        {
//            EqualConstraint.SetConstraintForType(typeof(Color), typeof(SameColorAs));
//            Ensure.That(System.Drawing.Color.Red,
//                Is.Is.EqualTo(System.Drawing.Color.FromArgb(255, 0, 0)));
//        }

        public void HandleException( Exception ex )
        {
            StringReader rdr = new StringReader( ex.Message );
            string report = rdr.ReadLine();
            string expected = rdr.ReadLine();
            if ( expected != null && expected.Length > 11 )
            {
                expected = expected.Substring( 11 );
            }
            string actual = rdr.ReadLine();
            if ( actual != null && actual.Length > 11 )
            {
                actual = actual.Substring( 11 );
            }
            string line = rdr.ReadLine();
            Ensure.That( line, new NotConstraint( new EqualConstraint( null ) ), "No caret line displayed" );
            int caret = line.Substring( 11 ).IndexOf( '^' );

            int minLength = Math.Min( expected.Length, actual.Length );
            int minMatch = Math.Min( caret, minLength );

            if ( caret != minLength )
            {
                if ( caret > minLength ||
                     expected.Substring( 0, minMatch ) != actual.Substring( 0, minMatch ) ||
                     expected[caret] == actual[caret] )
                {
                    Ensure.Fail( "Message Error: Caret does not point at first mismatch..." + Environment.NewLine + ex.Message );
                }
            }

            if ( expected.Length > 68 || actual.Length > 68 || caret > 68 )
            {
                Ensure.Fail( "Message Error: Strings are not truncated..." + Environment.NewLine + ex.Message );
            }
        }

        #region Nested type: SameColorAs

        public class SameColorAs : Constraint
        {
            private Color expectedColor;

            public SameColorAs( Color expectedColor )
            {
                this.expectedColor = expectedColor;
            }

            public override bool Matches( object actual )
            {
                Actual = actual;
                return actual is Color && ( (Color) actual ).ToArgb() == expectedColor.ToArgb();
            }

            public override void WriteDescriptionTo( MessageWriter writer )
            {
                writer.WriteExpectedValue( "same color as " + expectedColor.ToString() );
            }
        }

        #endregion
    }
}