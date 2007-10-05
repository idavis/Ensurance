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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ensurance.Tests
{
    [TestClass]
    public class NotEqualFixture : MessageChecker
    {
        [TestMethod]
        public void NotEqual()
        {
            Ensure.AreNotEqual( 5, 3 );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void NotEqualFails()
        {
            expectedMessage =
                "  Expected: not 5" + Environment.NewLine +
                "  But was:  5" + Environment.NewLine;
            Ensure.AreNotEqual( 5, 5 );
        }

        [TestMethod]
        public void NullNotEqualToNonNull()
        {
            Ensure.AreNotEqual( null, 3 );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void NullEqualsNull()
        {
            expectedMessage =
                "  Expected: not null" + Environment.NewLine +
                "  But was:  null" + Environment.NewLine;
            object obj1 = null;
            object obj2 = null;
            Ensure.AreEqual( obj1, obj2 );
            Ensure.AreNotEqual( obj1, obj2 );
        }

        [TestMethod]
        public void ArraysNotEqual()
        {
            Ensure.AreNotEqual( new object[] {1, 2, 3}, new object[] {1, 3, 2} );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void ArraysNotEqualFails()
        {
            expectedMessage =
                "  Expected: not < 1, 2, 3 >" + Environment.NewLine +
                "  But was:  < 1, 2, 3 >" + Environment.NewLine;
            Ensure.AreNotEqual( new object[] {1, 2, 3}, new object[] {1, 2, 3} );
        }

        [TestMethod]
        public void UInt()
        {
            uint u1 = 5;
            uint u2 = 8;
            Ensure.AreNotEqual( u1, u2 );
        }

        [TestMethod]
        public void NotEqualSameTypes()
        {
            byte b1 = 35;
            sbyte sb2 = 35;
            decimal d4 = 35;
            double d5 = 35;
            float f6 = 35;
            int i7 = 35;
            uint u8 = 35;
            long l9 = 35;
            short s10 = 35;
            ushort us11 = 35;

            Byte b12 = 35;
            SByte sb13 = 35;
            Decimal d14 = 35;
            Double d15 = 35;
            Single s16 = 35;
            Int32 i17 = 35;
            UInt32 ui18 = 35;
            Int64 i19 = 35;
            UInt64 ui20 = 35;
            Int16 i21 = 35;
            UInt16 i22 = 35;

            Ensure.AreNotEqual( 23, b1 );
            Ensure.AreNotEqual( 23, sb2 );
            Ensure.AreNotEqual( 23, d4 );
            Ensure.AreNotEqual( 23, d5 );
            Ensure.AreNotEqual( 23, f6 );
            Ensure.AreNotEqual( 23, i7 );
            Ensure.AreNotEqual( 23, u8 );
            Ensure.AreNotEqual( 23, l9 );
            Ensure.AreNotEqual( 23, s10 );
            Ensure.AreNotEqual( 23, us11 );

            Ensure.AreNotEqual( 23, b12 );
            Ensure.AreNotEqual( 23, sb13 );
            Ensure.AreNotEqual( 23, d14 );
            Ensure.AreNotEqual( 23, d15 );
            Ensure.AreNotEqual( 23, s16 );
            Ensure.AreNotEqual( 23, i17 );
            Ensure.AreNotEqual( 23, ui18 );
            Ensure.AreNotEqual( 23, i19 );
            Ensure.AreNotEqual( 23, ui20 );
            Ensure.AreNotEqual( 23, i21 );
            Ensure.AreNotEqual( 23, i22 );
        }
    }
}