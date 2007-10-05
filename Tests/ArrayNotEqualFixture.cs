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

using Ensurance.SyntaxHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ensurance.Tests
{
    /// <summary>
    /// Summary description for ArrayNotEqualFixture.
    /// </summary>
    [TestClass]
    public class ArrayNotEqualFixture : Ensure
    {
        [TestMethod]
        public void DifferentLengthArrays()
        {
            string[] array1 = {"one", "two", "three"};
            string[] array2 = {"one", "two", "three", "four", "five"};

            AreNotEqual( array1, array2 );
            AreNotEqual( array2, array1 );
            EnsuranceHelper.Expect( array1, Is.Not.EqualTo( array2 ) );
            EnsuranceHelper.Expect( array2, Is.Not.EqualTo( array1 ) );
        }

        [TestMethod]
        public void SameLengthDifferentContent()
        {
            string[] array1 = {"one", "two", "three"};
            string[] array2 = {"one", "two", "ten"};
            AreNotEqual( array1, array2 );
            AreNotEqual( array2, array1 );
            EnsuranceHelper.Expect( array1, Is.Not.EqualTo( array2 ) );
            EnsuranceHelper.Expect( array2, Is.Not.EqualTo( array1 ) );
        }

        [TestMethod]
        public void ArraysDeclaredAsDifferentTypes()
        {
            string[] array1 = {"one", "two", "three"};
            object[] array2 = {"one", "three", "two"};
            AreNotEqual( array1, array2 );
            EnsuranceHelper.Expect( array1, Is.Not.EqualTo( array2 ) );
            EnsuranceHelper.Expect( array2, Is.Not.EqualTo( array1 ) );
        }
    }
}