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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ensurance.Tests
{
    [TestClass]
    public class LessEqualFixture : MessageChecker
    {
        private readonly double d1 = 4.85948654;
        private readonly double d2 = 8.0;
        private readonly decimal de1 = 53.4M;
        private readonly decimal de2 = 83.4M;
        private readonly Enum e1 = CommandType.StoredProcedure;
        private readonly Enum e2 = CommandType.TableDirect;
        private readonly float f1 = 3.543F;
        private readonly float f2 = 8.543F;
        private readonly int i1 = 5;
        private readonly int i2 = 8;
        private readonly long l1 = 12345678;
        private readonly long l2 = 12345879;
        private readonly uint u1 = 12345678;
        private readonly uint u2 = 12345879;
        private readonly ulong ul1 = 12345678;
        private readonly ulong ul2 = 12345879;

        [TestMethod]
        public void LessOrEqual()
        {
            // Test equality check for all forms
            Ensure.LessOrEqual( i1, i1 );
            Ensure.LessOrEqual( i1, i1, "int" );
            Ensure.LessOrEqual( i1, i1, "{0}", "int" );
            Ensure.LessOrEqual( u1, u1 );
            Ensure.LessOrEqual( u1, u1, "uint" );
            Ensure.LessOrEqual( u1, u1, "{0}", "uint" );
            Ensure.LessOrEqual( l1, l1 );
            Ensure.LessOrEqual( l1, l1, "long" );
            Ensure.LessOrEqual( l1, l1, "{0}", "long" );
            Ensure.LessOrEqual( ul1, ul1 );
            Ensure.LessOrEqual( ul1, ul1, "ulong" );
            Ensure.LessOrEqual( ul1, ul1, "{0}", "ulong" );
            Ensure.LessOrEqual( d1, d1 );
            Ensure.LessOrEqual( d1, d1, "double" );
            Ensure.LessOrEqual( d1, d1, "{0}", "double" );
            Ensure.LessOrEqual( de1, de1 );
            Ensure.LessOrEqual( de1, de1, "decimal" );
            Ensure.LessOrEqual( de1, de1, "{0}", "decimal" );
            Ensure.LessOrEqual( f1, f1 );
            Ensure.LessOrEqual( f1, f1, "float" );
            Ensure.LessOrEqual( f1, f1, "{0}", "float" );

            // Testing all forms after seeing some bugs. CFP
            Ensure.LessOrEqual( i1, i2 );
            Ensure.LessOrEqual( i1, i2, "int" );
            Ensure.LessOrEqual( i1, i2, "{0}", "int" );
            Ensure.LessOrEqual( u1, u2 );
            Ensure.LessOrEqual( u1, u2, "uint" );
            Ensure.LessOrEqual( u1, u2, "{0}", "uint" );
            Ensure.LessOrEqual( l1, l2 );
            Ensure.LessOrEqual( l1, l2, "long" );
            Ensure.LessOrEqual( l1, l2, "{0}", "long" );
            Ensure.LessOrEqual( ul1, ul2 );
            Ensure.LessOrEqual( ul1, ul2, "ulong" );
            Ensure.LessOrEqual( ul1, ul2, "{0}", "ulong" );
            Ensure.LessOrEqual( d1, d2 );
            Ensure.LessOrEqual( d1, d2, "double" );
            Ensure.LessOrEqual( d1, d2, "{0}", "double" );
            Ensure.LessOrEqual( de1, de2 );
            Ensure.LessOrEqual( de1, de2, "decimal" );
            Ensure.LessOrEqual( de1, de2, "{0}", "decimal" );
            Ensure.LessOrEqual( f1, f2 );
            Ensure.LessOrEqual( f1, f2, "float" );
            Ensure.LessOrEqual( f1, f2, "{0}", "float" );
        }

        [TestMethod]
        public void MixedTypes()
        {
            Ensure.LessOrEqual( 5, 8L, "int to long" );
            Ensure.LessOrEqual( 5, 8.2f, "int to float" );
            Ensure.LessOrEqual( 5, 8.2d, "int to double" );
            Ensure.LessOrEqual( 5, 8U, "int to uint" );
            Ensure.LessOrEqual( 5, 8UL, "int to ulong" );
            Ensure.LessOrEqual( 5, 8M, "int to decimal" );

            Ensure.LessOrEqual( 5L, 8, "long to int" );
            Ensure.LessOrEqual( 5L, 8.2f, "long to float" );
            Ensure.LessOrEqual( 5L, 8.2d, "long to double" );
            Ensure.LessOrEqual( 5L, 8U, "long to uint" );
            Ensure.LessOrEqual( 5L, 8UL, "long to ulong" );
            Ensure.LessOrEqual( 5L, 8M, "long to decimal" );

            Ensure.LessOrEqual( 3.5f, 5, "float to int" );
            Ensure.LessOrEqual( 3.5f, 8L, "float to long" );
            Ensure.LessOrEqual( 3.5f, 8.2d, "float to double" );
            Ensure.LessOrEqual( 3.5f, 8U, "float to uint" );
            Ensure.LessOrEqual( 3.5f, 8UL, "float to ulong" );
            Ensure.LessOrEqual( 3.5f, 8.2M, "float to decimal" );

            Ensure.LessOrEqual( 3.5d, 5, "double to int" );
            Ensure.LessOrEqual( 3.5d, 5L, "double to long" );
            Ensure.LessOrEqual( 3.5d, 8.2f, "double to float" );
            Ensure.LessOrEqual( 3.5d, 8U, "double to uint" );
            Ensure.LessOrEqual( 3.5d, 8UL, "double to ulong" );
            Ensure.LessOrEqual( 3.5d, 8.2M, "double to decimal" );


            Ensure.LessOrEqual( 5U, 8, "uint to int" );
            Ensure.LessOrEqual( 5U, 8L, "uint to long" );
            Ensure.LessOrEqual( 5U, 8.2f, "uint to float" );
            Ensure.LessOrEqual( 5U, 8.2d, "uint to double" );
            Ensure.LessOrEqual( 5U, 8UL, "uint to ulong" );
            Ensure.LessOrEqual( 5U, 8M, "uint to decimal" );

            Ensure.LessOrEqual( 5ul, 8, "ulong to int" );
            Ensure.LessOrEqual( 5UL, 8L, "ulong to long" );
            Ensure.LessOrEqual( 5UL, 8.2f, "ulong to float" );
            Ensure.LessOrEqual( 5UL, 8.2d, "ulong to double" );
            Ensure.LessOrEqual( 5UL, 8U, "ulong to uint" );
            Ensure.LessOrEqual( 5UL, 8M, "ulong to decimal" );

            Ensure.LessOrEqual( 5M, 8, "decimal to int" );
            Ensure.LessOrEqual( 5M, 8L, "decimal to long" );
            Ensure.LessOrEqual( 5M, 8.2f, "decimal to float" );
            Ensure.LessOrEqual( 5M, 8.2d, "decimal to double" );
            Ensure.LessOrEqual( 5M, 8U, "decimal to uint" );
            Ensure.LessOrEqual( 5M, 8UL, "decimal to ulong" );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void NotLessOrEqual()
        {
            expectedMessage =
                "  Expected: less than or equal to 5" + Environment.NewLine +
                "  But was:  8" + Environment.NewLine;
            Ensure.LessOrEqual( i2, i1 );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void NotLessEqualIComparable()
        {
            expectedMessage =
                "  Expected: less than or equal to StoredProcedure" + Environment.NewLine +
                "  But was:  TableDirect" + Environment.NewLine;
            Ensure.LessOrEqual( e2, e1 );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void FailureMessage()
        {
            expectedMessage =
                "  Expected: less than or equal to 4" + Environment.NewLine +
                "  But was:  9" + Environment.NewLine;
            Ensure.LessOrEqual( 9, 4 );
        }
    }
}