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
using Ensurance.MessageWriters;
using Ensurance.SyntaxHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ensurance.Tests
{
    /// <summary>
    /// Summary description for ArrayEqualsFailureMessageFixture.
    /// </summary>
    [TestClass]
    public class ArrayEqualsFailureMessageFixture : MessageChecker
    {
        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void ArraysHaveDifferentRanks()
        {
            int[] expected = new int[] {1, 2, 3, 4};
            int[,] actual = new int[,] {{1, 2}, {3, 4}};

            expectedMessage =
                "  Expected is <System.Int32[4]>, actual is <System.Int32[2,2]>" + Environment.NewLine;
            EnsuranceHelper.Expect( actual, Is.EqualTo( expected ) );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void ExpectedArrayIsLonger()
        {
            int[] expected = new int[] {1, 2, 3, 4, 5};
            int[] actual = new int[] {1, 2, 3};

            expectedMessage =
                "  Expected is <System.Int32[5]>, actual is <System.Int32[3]>" + Environment.NewLine +
                "  Values differ at index [3]" + Environment.NewLine +
                "  Missing:  < 4, 5 >";
            EnsuranceHelper.Expect( actual, Is.EqualTo( expected ) );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void ActualArrayIsLonger()
        {
            int[] expected = new int[] {1, 2, 3};
            int[] actual = new int[] {1, 2, 3, 4, 5, 6, 7};

            expectedMessage =
                "  Expected is <System.Int32[3]>, actual is <System.Int32[7]>" + Environment.NewLine +
                "  Values differ at index [3]" + Environment.NewLine +
                "  Extra:    < 4, 5, 6... >";
            EnsuranceHelper.Expect( actual, Is.EqualTo( expected ) );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void FailureOnSingleDimensionedArrays()
        {
            int[] expected = new int[] {1, 2, 3};
            int[] actual = new int[] {1, 5, 3};

            expectedMessage =
                "  Expected and actual are both <System.Int32[3]>" + Environment.NewLine +
                "  Values differ at index [1]" + Environment.NewLine +
                TextMessageWriter.Pfx_Expected + "2" + Environment.NewLine +
                TextMessageWriter.Pfx_Actual + "5" + Environment.NewLine;
            EnsuranceHelper.Expect( actual, Is.EqualTo( expected ) );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void DoubleDimensionedArrays()
        {
            int[,] expected = new int[,] {{1, 2, 3}, {4, 5, 6}};
            int[,] actual = new int[,] {{1, 3, 2}, {4, 0, 6}};

            expectedMessage =
                "  Expected and actual are both <System.Int32[2,3]>" + Environment.NewLine +
                "  Values differ at index [0,1]" + Environment.NewLine +
                TextMessageWriter.Pfx_Expected + "2" + Environment.NewLine +
                TextMessageWriter.Pfx_Actual + "3" + Environment.NewLine;
            EnsuranceHelper.Expect( actual, Is.EqualTo( expected ) );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void TripleDimensionedArrays()
        {
            int[,,] expected = new int[,,] {{{1, 2}, {3, 4}}, {{5, 6}, {7, 8}}};
            int[,,] actual = new int[,,] {{{1, 2}, {3, 4}}, {{0, 6}, {7, 8}}};

            expectedMessage =
                "  Expected and actual are both <System.Int32[2,2,2]>" + Environment.NewLine +
                "  Values differ at index [1,0,0]" + Environment.NewLine +
                TextMessageWriter.Pfx_Expected + "5" + Environment.NewLine +
                TextMessageWriter.Pfx_Actual + "0" + Environment.NewLine;
            EnsuranceHelper.Expect( actual, Is.EqualTo( expected ) );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void FiveDimensionedArrays()
        {
            int[,,,,] expected = new int[2,2,2,2,2] {{{{{1, 2}, {3, 4}}, {{5, 6}, {7, 8}}}, {{{1, 2}, {3, 4}}, {{5, 6}, {7, 8}}}}, {{{{1, 2}, {3, 4}}, {{5, 6}, {7, 8}}}, {{{1, 2}, {3, 4}}, {{5, 6}, {7, 8}}}}};
            int[,,,,] actual = new int[2,2,2,2,2] {{{{{1, 2}, {4, 3}}, {{5, 6}, {7, 8}}}, {{{1, 2}, {3, 4}}, {{5, 6}, {7, 8}}}}, {{{{1, 2}, {3, 4}}, {{5, 6}, {7, 8}}}, {{{1, 2}, {3, 4}}, {{5, 6}, {7, 8}}}}};

            expectedMessage =
                "  Expected and actual are both <System.Int32[2,2,2,2,2]>" + Environment.NewLine +
                "  Values differ at index [0,0,0,1,0]" + Environment.NewLine +
                TextMessageWriter.Pfx_Expected + "3" + Environment.NewLine +
                TextMessageWriter.Pfx_Actual + "4" + Environment.NewLine;
            EnsuranceHelper.Expect( actual, Is.EqualTo( expected ) );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void JaggedArrays()
        {
            int[][] expected = new int[][] {new int[] {1, 2, 3}, new int[] {4, 5, 6, 7}, new int[] {8, 9}};
            int[][] actual = new int[][] {new int[] {1, 2, 3}, new int[] {4, 5, 0, 7}, new int[] {8, 9}};

            expectedMessage =
                "  Expected and actual are both <System.Int32[3][]>" + Environment.NewLine +
                "  Values differ at index [1]" + Environment.NewLine +
                "    Expected and actual are both <System.Int32[4]>" + Environment.NewLine +
                "    Values differ at index [2]" + Environment.NewLine +
                TextMessageWriter.Pfx_Expected + "6" + Environment.NewLine +
                TextMessageWriter.Pfx_Actual + "0" + Environment.NewLine;
            EnsuranceHelper.Expect( actual, Is.EqualTo( expected ) );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void JaggedArrayComparedToSimpleArray()
        {
            int[] expected = new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9};
            int[][] actual = new int[][] {new int[] {1, 2, 3}, new int[] {4, 5, 0, 7}, new int[] {8, 9}};

            expectedMessage =
                "  Expected is <System.Int32[9]>, actual is <System.Int32[3][]>" + Environment.NewLine +
                "  Values differ at index [0]" + Environment.NewLine +
                TextMessageWriter.Pfx_Expected + "1" + Environment.NewLine +
                TextMessageWriter.Pfx_Actual + "< 1, 2, 3 >" + Environment.NewLine;
            EnsuranceHelper.Expect( actual, Is.EqualTo( expected ) );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void ArraysWithDifferentRanksAsCollection()
        {
            int[] expected = new int[] {1, 2, 3, 4};
            int[,] actual = new int[,] {{1, 0}, {3, 4}};

            expectedMessage =
                "  Expected is <System.Int32[4]>, actual is <System.Int32[2,2]>" + Environment.NewLine +
                "  Values differ at expected index [1], actual index [0,1]" + Environment.NewLine +
                TextMessageWriter.Pfx_Expected + "2" + Environment.NewLine +
                TextMessageWriter.Pfx_Actual + "0" + Environment.NewLine;
            EnsuranceHelper.Expect( actual, Is.EqualTo( expected ).AsCollection );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void ArraysWithDifferentDimensionsAsCollection()
        {
            int[,] expected = new int[,] {{1, 2, 3}, {4, 5, 6}};
            int[,] actual = new int[,] {{1, 2}, {3, 0}, {5, 6}};

            expectedMessage =
                "  Expected is <System.Int32[2,3]>, actual is <System.Int32[3,2]>" + Environment.NewLine +
                "  Values differ at expected index [1,0], actual index [1,1]" + Environment.NewLine +
                TextMessageWriter.Pfx_Expected + "4" + Environment.NewLine +
                TextMessageWriter.Pfx_Actual + "0" + Environment.NewLine;
            EnsuranceHelper.Expect( actual, Is.EqualTo( expected ).AsCollection );
        }

//		[TestMethod,ExpectedException(typeof(EnsuranceException))]
//		public void ExpectedArrayIsLonger()
//		{
//			string[] array1 = { "one", "two", "three" };
//			string[] array2 = { "one", "two", "three", "four", "five" };
//
//			expectedMessage =
//				"  Expected is <System.String[5]>, actual is <System.String[3]>" + Environment.NewLine +
//				"  Values differ at index [3]" + Environment.NewLine +
//				"  Missing:  < \"four\", \"five\" >";
//			EnsuranceHelper.Expect(array1, Is.EqualTo(array2));
//		}

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void SameLengthDifferentContent()
        {
            string[] array1 = {"one", "two", "three"};
            string[] array2 = {"one", "two", "ten"};

            expectedMessage =
                "  Expected and actual are both <System.String[3]>" + Environment.NewLine +
                "  Values differ at index [2]" + Environment.NewLine +
                "  Expected string length 3 but was 5. Strings differ at index 1." + Environment.NewLine +
                "  Expected: \"ten\"" + Environment.NewLine +
                "  But was:  \"three\"" + Environment.NewLine +
                "  ------------^" + Environment.NewLine;
            EnsuranceHelper.Expect( array1, Is.EqualTo( array2 ) );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void ArraysDeclaredAsDifferentTypes()
        {
            string[] array1 = {"one", "two", "three"};
            object[] array2 = {"one", "three", "two"};

            expectedMessage =
                "  Expected is <System.Object[3]>, actual is <System.String[3]>" + Environment.NewLine +
                "  Values differ at index [1]" + Environment.NewLine +
                "  Expected string length 5 but was 3. Strings differ at index 1." + Environment.NewLine +
                "  Expected: \"three\"" + Environment.NewLine +
                "  But was:  \"two\"" + Environment.NewLine +
                "  ------------^" + Environment.NewLine;
            EnsuranceHelper.Expect( array1, Is.EqualTo( array2 ) );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void ArrayAndCollection_Failure()
        {
            int[] a = new int[] {1, 2, 3};
            ArrayList b = new ArrayList();
            b.Add( 1 );
            b.Add( 3 );
            Ensure.AreEqual( a, b );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void DifferentArrayTypesEqualFails()
        {
            string[] array1 = {"one", "two", "three"};
            object[] array2 = {"one", "three", "two"};

            expectedMessage =
                "  Expected is <System.String[3]>, actual is <System.Object[3]>" + Environment.NewLine +
                "  Values differ at index [1]" + Environment.NewLine +
                "  Expected string length 3 but was 5. Strings differ at index 1." + Environment.NewLine +
                "  Expected: \"two\"" + Environment.NewLine +
                "  But was:  \"three\"" + Environment.NewLine +
                "  ------------^" + Environment.NewLine;
            Ensure.AreEqual( array1, array2 );
        }
    }
}