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
using System.Data;
using Ensurance.MessageWriters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ensurance.Tests
{
    [TestClass]
    public class GreaterEqualFixture : MessageChecker
    {
        private readonly double d1 = 4.85948654;
        private readonly double d2 = 1.0;
        private readonly decimal de1 = 53.4M;
        private readonly decimal de2 = 33.4M;
        private readonly Enum e1 = CommandType.TableDirect;
        private readonly Enum e2 = CommandType.StoredProcedure;
        private readonly float f1 = 3.543F;
        private readonly float f2 = 2.543F;
        private readonly int i1 = 5;
        private readonly int i2 = 4;
        private readonly long l1 = 12345879;
        private readonly long l2 = 12345678;
        private readonly uint u1 = 12345879;
        private readonly uint u2 = 12345678;
        private readonly ulong ul1 = 12345879;
        private readonly ulong ul2 = 12345678;

        [TestMethod]
        public void GreaterOrEqual_Int32()
        {
            Ensure.GreaterOrEqual( i1, i1 );
            Ensure.GreaterOrEqual( i1, i2 );
        }

        [TestMethod]
        public void GreaterOrEqual_UInt32()
        {
            Ensure.GreaterOrEqual( u1, u1 );
            Ensure.GreaterOrEqual( u1, u2 );
        }

        [TestMethod]
        public void GreaterOrEqual_Long()
        {
            Ensure.GreaterOrEqual( l1, l1 );
            Ensure.GreaterOrEqual( l1, l2 );
        }

        [TestMethod]
        public void GreaterOrEqual_ULong()
        {
            Ensure.GreaterOrEqual( ul1, ul1 );
            Ensure.GreaterOrEqual( ul1, ul2 );
        }

        [TestMethod]
        public void GreaterOrEqual_Double()
        {
            Ensure.GreaterOrEqual( d1, d1, "double" );
            Ensure.GreaterOrEqual( d1, d2, "double" );
        }

        [TestMethod]
        public void GreaterOrEqual_Decimal()
        {
            Ensure.GreaterOrEqual( de1, de1, "{0}", "decimal" );
            Ensure.GreaterOrEqual( de1, de2, "{0}", "decimal" );
        }

        [TestMethod]
        public void GreaterOrEqual_Float()
        {
            Ensure.GreaterOrEqual( f1, f1, "float" );
            Ensure.GreaterOrEqual( f1, f2, "float" );
        }

        [TestMethod]
        public void MixedTypes()
        {
            Ensure.GreaterOrEqual( 5, 3L, "int to long" );
            Ensure.GreaterOrEqual( 5, 3.5f, "int to float" );
            Ensure.GreaterOrEqual( 5, 3.5d, "int to double" );
            Ensure.GreaterOrEqual( 5, 3U, "int to uint" );
            Ensure.GreaterOrEqual( 5, 3UL, "int to ulong" );
            Ensure.GreaterOrEqual( 5, 3M, "int to decimal" );

            Ensure.GreaterOrEqual( 5L, 3, "long to int" );
            Ensure.GreaterOrEqual( 5L, 3.5f, "long to float" );
            Ensure.GreaterOrEqual( 5L, 3.5d, "long to double" );
            Ensure.GreaterOrEqual( 5L, 3U, "long to uint" );
            Ensure.GreaterOrEqual( 5L, 3UL, "long to ulong" );
            Ensure.GreaterOrEqual( 5L, 3M, "long to decimal" );

            Ensure.GreaterOrEqual( 8.2f, 5, "float to int" );
            Ensure.GreaterOrEqual( 8.2f, 8L, "float to long" );
            Ensure.GreaterOrEqual( 8.2f, 3.5d, "float to double" );
            Ensure.GreaterOrEqual( 8.2f, 8U, "float to uint" );
            Ensure.GreaterOrEqual( 8.2f, 8UL, "float to ulong" );
            Ensure.GreaterOrEqual( 8.2f, 3.5M, "float to decimal" );

            Ensure.GreaterOrEqual( 8.2d, 5, "double to int" );
            Ensure.GreaterOrEqual( 8.2d, 5L, "double to long" );
            Ensure.GreaterOrEqual( 8.2d, 3.5f, "double to float" );
            Ensure.GreaterOrEqual( 8.2d, 8U, "double to uint" );
            Ensure.GreaterOrEqual( 8.2d, 8UL, "double to ulong" );
            Ensure.GreaterOrEqual( 8.2d, 3.5M, "double to decimal" );


            Ensure.GreaterOrEqual( 5U, 3, "uint to int" );
            Ensure.GreaterOrEqual( 5U, 3L, "uint to long" );
            Ensure.GreaterOrEqual( 5U, 3.5f, "uint to float" );
            Ensure.GreaterOrEqual( 5U, 3.5d, "uint to double" );
            Ensure.GreaterOrEqual( 5U, 3UL, "uint to ulong" );
            Ensure.GreaterOrEqual( 5U, 3M, "uint to decimal" );

            Ensure.GreaterOrEqual( 5ul, 3, "ulong to int" );
            Ensure.GreaterOrEqual( 5UL, 3L, "ulong to long" );
            Ensure.GreaterOrEqual( 5UL, 3.5f, "ulong to float" );
            Ensure.GreaterOrEqual( 5UL, 3.5d, "ulong to double" );
            Ensure.GreaterOrEqual( 5UL, 3U, "ulong to uint" );
            Ensure.GreaterOrEqual( 5UL, 3M, "ulong to decimal" );

            Ensure.GreaterOrEqual( 5M, 3, "decimal to int" );
            Ensure.GreaterOrEqual( 5M, 3L, "decimal to long" );
            Ensure.GreaterOrEqual( 5M, 3.5f, "decimal to float" );
            Ensure.GreaterOrEqual( 5M, 3.5d, "decimal to double" );
            Ensure.GreaterOrEqual( 5M, 3U, "decimal to uint" );
            Ensure.GreaterOrEqual( 5M, 3UL, "decimal to ulong" );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void NotGreaterOrEqual()
        {
            expectedMessage =
                "  Expected: greater than or equal to 5" + Environment.NewLine +
                "  But was:  4" + Environment.NewLine;
            Ensure.GreaterOrEqual( i2, i1 );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void NotGreaterEqualIComparable()
        {
            expectedMessage =
                "  Expected: greater than or equal to TableDirect" + Environment.NewLine +
                "  But was:  StoredProcedure" + Environment.NewLine;
            Ensure.GreaterOrEqual( e2, e1 );
        }

        [TestMethod]
        public void FailureMessage()
        {
            string msg = null;

            try
            {
                Ensure.GreaterOrEqual( 7, 99 );
            }
            catch (EnsuranceException ex)
            {
                msg = ex.Message;
            }

            EnsureBase<Ensure>.Strings.Contains( TextMessageWriter.Pfx_Expected + "greater than or equal to 99", msg );
            EnsureBase<Ensure>.Strings.Contains( TextMessageWriter.Pfx_Actual + "7", msg );
        }
    }
}