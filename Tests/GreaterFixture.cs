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
    public class GreaterFixture : MessageChecker
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
        public void Greater()
        {
            Ensure.Greater( i1, i2 );
            Ensure.Greater( u1, u2 );
            Ensure.Greater( l1, l2 );
            Ensure.Greater( ul1, ul2 );
            Ensure.Greater( d1, d2, "double" );
            Ensure.Greater( de1, de2, "{0}", "decimal" );
            Ensure.Greater( f1, f2, "float" );
        }

        [TestMethod]
        public void MixedTypes()
        {
            Ensure.Greater( 5, 3L, "int to long" );
            Ensure.Greater( 5, 3.5f, "int to float" );
            Ensure.Greater( 5, 3.5d, "int to double" );
            Ensure.Greater( 5, 3U, "int to uint" );
            Ensure.Greater( 5, 3UL, "int to ulong" );
            Ensure.Greater( 5, 3M, "int to decimal" );

            Ensure.Greater( 5L, 3, "long to int" );
            Ensure.Greater( 5L, 3.5f, "long to float" );
            Ensure.Greater( 5L, 3.5d, "long to double" );
            Ensure.Greater( 5L, 3U, "long to uint" );
            Ensure.Greater( 5L, 3UL, "long to ulong" );
            Ensure.Greater( 5L, 3M, "long to decimal" );

            Ensure.Greater( 8.2f, 5, "float to int" );
            Ensure.Greater( 8.2f, 8L, "float to long" );
            Ensure.Greater( 8.2f, 3.5d, "float to double" );
            Ensure.Greater( 8.2f, 8U, "float to uint" );
            Ensure.Greater( 8.2f, 8UL, "float to ulong" );
            Ensure.Greater( 8.2f, 3.5M, "float to decimal" );

            Ensure.Greater( 8.2d, 5, "double to int" );
            Ensure.Greater( 8.2d, 5L, "double to long" );
            Ensure.Greater( 8.2d, 3.5f, "double to float" );
            Ensure.Greater( 8.2d, 8U, "double to uint" );
            Ensure.Greater( 8.2d, 8UL, "double to ulong" );
            Ensure.Greater( 8.2d, 3.5M, "double to decimal" );


            Ensure.Greater( 5U, 3, "uint to int" );
            Ensure.Greater( 5U, 3L, "uint to long" );
            Ensure.Greater( 5U, 3.5f, "uint to float" );
            Ensure.Greater( 5U, 3.5d, "uint to double" );
            Ensure.Greater( 5U, 3UL, "uint to ulong" );
            Ensure.Greater( 5U, 3M, "uint to decimal" );

            Ensure.Greater( 5ul, 3, "ulong to int" );
            Ensure.Greater( 5UL, 3L, "ulong to long" );
            Ensure.Greater( 5UL, 3.5f, "ulong to float" );
            Ensure.Greater( 5UL, 3.5d, "ulong to double" );
            Ensure.Greater( 5UL, 3U, "ulong to uint" );
            Ensure.Greater( 5UL, 3M, "ulong to decimal" );

            Ensure.Greater( 5M, 3, "decimal to int" );
            Ensure.Greater( 5M, 3L, "decimal to long" );
            Ensure.Greater( 5M, 3.5f, "decimal to float" );
            Ensure.Greater( 5M, 3.5d, "decimal to double" );
            Ensure.Greater( 5M, 3U, "decimal to uint" );
            Ensure.Greater( 5M, 3UL, "decimal to ulong" );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void NotGreaterWhenEqual()
        {
            expectedMessage =
                "  Expected: greater than 5" + Environment.NewLine +
                "  But was:  5" + Environment.NewLine;
            Ensure.Greater( i1, i1 );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void NotGreater()
        {
            expectedMessage =
                "  Expected: greater than 5" + Environment.NewLine +
                "  But was:  4" + Environment.NewLine;
            Ensure.Greater( i2, i1 );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void NotGreaterIComparable()
        {
            expectedMessage =
                "  Expected: greater than TableDirect" + Environment.NewLine +
                "  But was:  StoredProcedure" + Environment.NewLine;
            Ensure.Greater( e2, e1 );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void FailureMessage()
        {
            expectedMessage =
                "  Expected: greater than 99" + Environment.NewLine +
                "  But was:  7" + Environment.NewLine;
            Ensure.Greater( 7, 99 );
        }
    }
}