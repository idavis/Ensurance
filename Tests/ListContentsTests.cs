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
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ensurance.Tests
{
    /// <summary>
    /// Summary description for ListContentsTests.
    /// </summary>
    [TestClass]
    public class ListContentsTests : MessageChecker
    {
        private static readonly object[] testArray = {"abc", 123, "xyz"};

        [TestMethod]
        public void ArraySucceeds()
        {
            Ensure.Contains( "abc", testArray );
            Ensure.Contains( 123, testArray );
            Ensure.Contains( "xyz", testArray );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void ArrayFails()
        {
            expectedMessage =
                "  Expected: collection containing \"def\"" + Environment.NewLine +
                "  But was:  < \"abc\", 123, \"xyz\" >" + Environment.NewLine;
            Ensure.Contains( "def", testArray );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void EmptyArrayFails()
        {
            expectedMessage =
                "  Expected: collection containing \"def\"" + Environment.NewLine +
                "  But was:  <empty>" + Environment.NewLine;
            Ensure.Contains( "def", new object[0] );
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void NullArrayIsError()
        {
            Ensure.Contains( "def", null );
        }

        [TestMethod]
        public void ArrayListSucceeds()
        {
            ArrayList list = new ArrayList( testArray );

            Ensure.Contains( "abc", list );
            Ensure.Contains( 123, list );
            Ensure.Contains( "xyz", list );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void ArrayListFails()
        {
            expectedMessage =
                "  Expected: collection containing \"def\"" + Environment.NewLine +
                "  But was:  < \"abc\", 123, \"xyz\" >" + Environment.NewLine;
            Ensure.Contains( "def", new ArrayList( testArray ) );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void DifferentTypesFail()
        {
            // TODO: Better message for this case
            expectedMessage =
                "  Expected: collection containing 123.0d" + Environment.NewLine +
                "  But was:  < \"abc\", 123, \"xyz\" >" + Environment.NewLine;
            Ensure.Contains( 123.0, new ArrayList( testArray ) );
        }
    }
}