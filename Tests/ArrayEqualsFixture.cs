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
using Ensurance.SyntaxHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ensurance.Tests
{
    /// <summary>
    /// Summary description for ArrayEqualTests.
    /// </summary>
    [TestClass]
    public class ArrayEqualsFixture : EnsuranceHelper
    {
        [TestMethod]
        public void ArrayIsEqualToItself()
        {
            string[] array = {"one", "two", "three"};
            AreSame( array, array );
            AreEqual( array, array );
            Expect( array, Is.EqualTo( array ) );
        }

        [TestMethod]
        public void ArraysOfString()
        {
            string[] array1 = {"one", "two", "three"};
            string[] array2 = {"one", "two", "three"};
            IsFalse( array1 == array2 );
            AreEqual( array1, array2 );
            Expect( array1, Is.EqualTo( array2 ) );
            AreEqual( array2, array1 );
            Expect( array2, Is.EqualTo( array1 ) );
        }

        [TestMethod]
        public void ArraysOfInt()
        {
            int[] a = new int[] {1, 2, 3};
            int[] b = new int[] {1, 2, 3};
            AreEqual( a, b );
            AreEqual( b, a );
            Expect( a, Is.EqualTo( b ) );
            Expect( b, Is.EqualTo( a ) );
        }

        [TestMethod]
        public void ArraysOfDouble()
        {
            double[] a = new double[] {1.0, 2.0, 3.0};
            double[] b = new double[] {1.0, 2.0, 3.0};
            AreEqual( a, b );
            AreEqual( b, a );
            Expect( a, Is.EqualTo( b ) );
            Expect( b, Is.EqualTo( a ) );
        }

        [TestMethod]
        public void ArraysOfDecimal()
        {
            decimal[] a = new decimal[] {1.0m, 2.0m, 3.0m};
            decimal[] b = new decimal[] {1.0m, 2.0m, 3.0m};
            AreEqual( a, b );
            AreEqual( b, a );
            Expect( a, Is.EqualTo( b ) );
            Expect( b, Is.EqualTo( a ) );
        }

        [TestMethod]
        public void ArrayOfIntAndArrayOfDouble()
        {
            int[] a = new int[] {1, 2, 3};
            double[] b = new double[] {1.0, 2.0, 3.0};
            AreEqual( a, b );
            AreEqual( b, a );
            Expect( a, Is.EqualTo( b ) );
            Expect( b, Is.EqualTo( a ) );
        }

        [TestMethod]
        public void ArraysDeclaredAsDifferentTypes()
        {
            string[] array1 = {"one", "two", "three"};
            object[] array2 = {"one", "two", "three"};
            AreEqual( array1, array2, "String[] not equal to Object[]" );
            AreEqual( array2, array1, "Object[] not equal to String[]" );
            Expect( array1, Is.EqualTo( array2 ), "String[] not equal to Object[]" );
            Expect( array2, Is.EqualTo( array1 ), "Object[] not equal to String[]" );
        }

        [TestMethod]
        public void ArraysOfMixedTypes()
        {
            DateTime now = DateTime.Now;
            object[] array1 = new object[] {1, 2.0f, 3.5d, 7.000m, "Hello", now};
            object[] array2 = new object[] {1.0d, 2, 3.5, 7, "Hello", now};
            AreEqual( array1, array2 );
            AreEqual( array2, array1 );
            Expect( array1, Is.EqualTo( array2 ) );
            Expect( array2, Is.EqualTo( array1 ) );
        }

        [TestMethod]
        public void DoubleDimensionedArrays()
        {
            int[,] a = new int[,] {{1, 2, 3}, {4, 5, 6}, {7, 8, 9}};
            int[,] b = new int[,] {{1, 2, 3}, {4, 5, 6}, {7, 8, 9}};
            AreEqual( a, b );
            AreEqual( b, a );
            Expect( a, Is.EqualTo( b ) );
            Expect( b, Is.EqualTo( a ) );
        }

        [TestMethod]
        public void TripleDimensionedArrays()
        {
            int[,,] expected = new int[,,] {{{1, 2}, {3, 4}}, {{5, 6}, {7, 8}}};
            int[,,] actual = new int[,,] {{{1, 2}, {3, 4}}, {{5, 6}, {7, 8}}};

            AreEqual( expected, actual );
            Expect( actual, Is.EqualTo( expected ) );
        }

        [TestMethod]
        public void FiveDimensionedArrays()
        {
            int[,,,,] expected = new int[2,2,2,2,2] {{{{{1, 2}, {3, 4}}, {{5, 6}, {7, 8}}}, {{{1, 2}, {3, 4}}, {{5, 6}, {7, 8}}}}, {{{{1, 2}, {3, 4}}, {{5, 6}, {7, 8}}}, {{{1, 2}, {3, 4}}, {{5, 6}, {7, 8}}}}};
            int[,,,,] actual = new int[2,2,2,2,2] {{{{{1, 2}, {3, 4}}, {{5, 6}, {7, 8}}}, {{{1, 2}, {3, 4}}, {{5, 6}, {7, 8}}}}, {{{{1, 2}, {3, 4}}, {{5, 6}, {7, 8}}}, {{{1, 2}, {3, 4}}, {{5, 6}, {7, 8}}}}};

            AreEqual( expected, actual );
            Expect( actual, Is.EqualTo( expected ) );
        }

        [TestMethod]
        public void ArraysOfArrays()
        {
            int[][] a = new int[][] {new int[] {1, 2, 3}, new int[] {4, 5, 6}, new int[] {7, 8, 9}};
            int[][] b = new int[][] {new int[] {1, 2, 3}, new int[] {4, 5, 6}, new int[] {7, 8, 9}};
            AreEqual( a, b );
            AreEqual( b, a );
            Expect( a, Is.EqualTo( b ) );
            Expect( b, Is.EqualTo( a ) );
        }

        [TestMethod]
        public void JaggedArrays()
        {
            int[][] expected = new int[][] {new int[] {1, 2, 3}, new int[] {4, 5, 6, 7}, new int[] {8, 9}};
            int[][] actual = new int[][] {new int[] {1, 2, 3}, new int[] {4, 5, 6, 7}, new int[] {8, 9}};

            AreEqual( expected, actual );
            Expect( actual, Is.EqualTo( expected ) );
        }

        [TestMethod]
        public void ArraysPassedAsObjects()
        {
            object a = new int[] {1, 2, 3};
            object b = new double[] {1.0, 2.0, 3.0};
            AreEqual( a, b );
            AreEqual( b, a );
            Expect( a, Is.EqualTo( b ) );
            Expect( b, Is.EqualTo( a ) );
        }

        [TestMethod]
        public void ArrayAndCollection()
        {
            int[] a = new int[] {1, 2, 3};
            ICollection b = new ArrayList( a );
            AreEqual( a, b );
            AreEqual( b, a );
            Expect( a, Is.EqualTo( b ) );
            Expect( b, Is.EqualTo( a ) );
        }

        [TestMethod]
        public void ArraysWithDifferentRanksComparedAsCollection()
        {
            int[] expected = new int[] {1, 2, 3, 4};
            int[,] actual = new int[,] {{1, 2}, {3, 4}};

            AreNotEqual( expected, actual );
            Expect( actual, Is.Not.EqualTo( expected ) );
            Expect( actual, Is.EqualTo( expected ).AsCollection );
        }

        [TestMethod]
        public void ArraysWithDifferentDimensionsMatchedAsCollection()
        {
            int[,] expected = new int[,] {{1, 2, 3}, {4, 5, 6}};
            int[,] actual = new int[,] {{1, 2}, {3, 4}, {5, 6}};

            AreNotEqual( expected, actual );
            Expect( actual, Is.Not.EqualTo( expected ) );
            Expect( actual, Is.EqualTo( expected ).AsCollection );
        }
    }
}