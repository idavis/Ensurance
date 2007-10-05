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
using Ensurance.Constraints;
using Ensurance.MessageWriters;
using Ensurance.SyntaxHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ensurance.Tests
{
    /*[TestClass]
    public class MessageWriterTests : ConstraintBuilder
    {
        protected TextMessageWriter writer;

		[TestInitialize]
		public void SetUp()
        {
            writer = new TextMessageWriter();
        }
    }*/

    [TestClass]
    public class TestMessageWriterTests : ConstraintBuilder
    {
        protected TextMessageWriter writer;

        [TestInitialize]
        public void SetUp()
        {
            writer = new TextMessageWriter();
        }

        [TestMethod]
        public void ConnectorIsWrittenWithSurroundingSpaces()
        {
            writer.WriteConnector( "and" );
            EnsuranceHelper.Expect( writer.ToString(), Is.EqualTo( " and " ) );
        }

        [TestMethod]
        public void PredicateIsWrittenWithTrailingSpace()
        {
            writer.WritePredicate( "contains" );
            EnsuranceHelper.Expect( writer.ToString(), Is.EqualTo( "contains " ) );
        }

        #region Nested type: ActualValueTests

        [TestClass]
        public class ActualValueTests : ValueTests
        {
            protected override void WriteValue( object obj )
            {
                writer.WriteActualValue( obj );
            }
        }

        #endregion

        #region Nested type: ExpectedValueTests

        [TestClass]
        public class ExpectedValueTests : ValueTests
        {
            protected override void WriteValue( object obj )
            {
                writer.WriteExpectedValue( obj );
            }
        }

        #endregion

        #region Nested type: ValueTests

        [TestClass]
        public class ValueTests : ConstraintBuilder
        {
            protected TextMessageWriter writer;

            [TestInitialize]
            public void SetUp()
            {
                writer = new TextMessageWriter();
            }

            protected virtual void WriteValue( object obj )
            {
                writer.WriteActualValue( obj );
            }

            [TestMethod]
            public void IntegerIsWrittenAsIs()
            {
                WriteValue( 42 );
                EnsuranceHelper.Expect( writer.ToString(), Is.EqualTo( "42" ) );
            }

            [TestMethod]
            public void StringIsWrittenWithQuotes()
            {
                WriteValue( "Hello" );
                EnsuranceHelper.Expect( writer.ToString(), Is.EqualTo( "\"Hello\"" ) );
            }

            // This test currently fails because control character replacement is
            // done at a higher level...
            // TODO: See if we should do it at a lower level
//            [TestMethod]
//            public void ControlCharactersInStringsAreEscaped()
//            {
//                WriteValue("Best Wishes,\r\n\tCharlie\r\n");
//                Ensure.That(writer.ToString(), Is.Is.EqualTo("\"Best Wishes,\\r\\n\\tCharlie\\r\\n\""));
//            }

            [TestMethod]
            public void FloatIsWrittenWithTrailingF()
            {
                WriteValue( 0.5f );
                EnsuranceHelper.Expect( writer.ToString(), Is.EqualTo( "0.5f" ) );
            }

            [TestMethod]
            public void FloatIsWrittenToNineDigits()
            {
                WriteValue( 0.33333333333333f );
                int digits = writer.ToString().Length - 3; // 0.dddddddddf
                EnsuranceHelper.Expect( digits, Is.EqualTo( 9 ) );
                EnsuranceHelper.Expect( writer.ToString().Length, Is.EqualTo( 12 ) );
            }

            [TestMethod]
            public void DoubleIsWrittenWithTrailingD()
            {
                WriteValue( 0.5d );
                EnsuranceHelper.Expect( writer.ToString(), Is.EqualTo( "0.5d" ) );
            }

            [TestMethod]
            public void DoubleIsWrittenToSeventeenDigits()
            {
                WriteValue( 0.33333333333333333333333333333333333333333333d );
                EnsuranceHelper.Expect( writer.ToString().Length, Is.EqualTo( 20 ) ); // add 3 for leading 0, decimal and trailing d
            }

            [TestMethod]
            public void DecimalIsWrittenWithTrailingM()
            {
                WriteValue( 0.5m );
                EnsuranceHelper.Expect( writer.ToString(), Is.EqualTo( "0.5m" ) );
            }

            [TestMethod]
            public void DecimalIsWrittenToTwentyNineDigits()
            {
                WriteValue( 12345678901234567890123456789m );
                EnsuranceHelper.Expect( writer.ToString(), Is.EqualTo( "12345678901234567890123456789m" ) );
            }

            [TestMethod]
            public void DateTimeTest()
            {
                WriteValue( new DateTime( 2007, 7, 4, 9, 15, 30, 123 ) );
                EnsuranceHelper.Expect( writer.ToString(), Is.EqualTo( "2007-07-04 09:15:30.123" ) );
            }
        }

        #endregion
    }
}