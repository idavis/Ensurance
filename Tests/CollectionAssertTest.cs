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
    /// Test Library for the NUnit Ensure class.
    /// </summary>
    [TestClass]
    public class CollectionAssertTest : MessageChecker
    {
        #region AllItemsAreInstancesOfType

        [TestMethod]
        public void ItemsOfType()
        {
            ArrayList al = new ArrayList();
            al.Add( "x" );
            al.Add( "y" );
            al.Add( "z" );
            Ensure.AllItemsAreInstancesOfType( al, typeof (string) );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void ItemsOfTypeFailure()
        {
            ArrayList al = new ArrayList();
            al.Add( "x" );
            al.Add( "y" );
            al.Add( new object() );

            expectedMessage =
                "  Expected: all items instance of <System.String>" + Environment.NewLine +
                "  But was:  < \"x\", \"y\", <System.Object> >" + Environment.NewLine;
            Ensure.AllItemsAreInstancesOfType( al, typeof (string) );
        }

        #endregion

        #region AllItemsAreNotNull

        [TestMethod]
        public void ItemsNotNull()
        {
            ArrayList al = new ArrayList();
            al.Add( "x" );
            al.Add( "y" );
            al.Add( "z" );

            Ensure.AllItemsAreNotNull( al );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void ItemsNotNullFailure()
        {
            ArrayList al = new ArrayList();
            al.Add( "x" );
            al.Add( null );
            al.Add( "z" );

            expectedMessage =
                "  Expected: all items not null" + Environment.NewLine +
                "  But was:  < \"x\", null, \"z\" >" + Environment.NewLine;
            Ensure.AllItemsAreNotNull( al );
        }

        #endregion

        #region AllItemsAreUnique

        [TestMethod]
        public void Unique_WithObjects()
        {
            Ensure.AllItemsAreUnique(
                new ICollectionAdapter( new object(), new object(), new object() ) );
        }

        [TestMethod]
        public void Unique_WithStrings()
        {
            Ensure.AllItemsAreUnique( new ICollectionAdapter( "x", "y", "z" ) );
        }

        [TestMethod]
        public void Unique_WithNull()
        {
            Ensure.AllItemsAreUnique( new ICollectionAdapter( "x", "y", null, "z" ) );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void UniqueFailure()
        {
            expectedMessage =
                "  Expected: all items unique" + Environment.NewLine +
                "  But was:  < \"x\", \"y\", \"x\" >" + Environment.NewLine;
            Ensure.AllItemsAreUnique( new ICollectionAdapter( "x", "y", "x" ) );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void UniqueFailure_WithTwoNulls()
        {
            Ensure.AllItemsAreUnique( new ICollectionAdapter( "x", null, "y", null, "z" ) );
        }

        #endregion

        #region AreEqual

        [TestMethod]
        public void AreEqual()
        {
            ArrayList set1 = new ArrayList();
            ArrayList set2 = new ArrayList();
            set1.Add( "x" );
            set1.Add( "y" );
            set1.Add( "z" );
            set2.Add( "x" );
            set2.Add( "y" );
            set2.Add( "z" );

            Ensure.AreEqual( set1, set2 );
            Ensure.AreEqual( set1, set2, new TestComparer() );

            Ensure.AreEqual( set1, set2 );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void AreEqualFailCount()
        {
            ArrayList set1 = new ArrayList();
            ArrayList set2 = new ArrayList();
            set1.Add( "x" );
            set1.Add( "y" );
            set1.Add( "z" );
            set2.Add( "x" );
            set2.Add( "y" );
            set2.Add( "z" );
            set2.Add( "a" );

            expectedMessage =
                "  Expected and actual are both <System.Collections.ArrayList> with 3 elements" + Environment.NewLine +
                "  Values differ at index [3]" + Environment.NewLine +
                "  Extra:    < \"a\" >";
            Ensure.AreEqual( set1, set2, new TestComparer() );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void AreEqualFail()
        {
            ArrayList set1 = new ArrayList();
            ArrayList set2 = new ArrayList();
            set1.Add( "x" );
            set1.Add( "y" );
            set1.Add( "z" );
            set2.Add( "x" );
            set2.Add( "y" );
            set2.Add( "a" );

            expectedMessage =
                "  Expected and actual are both <System.Collections.ArrayList> with 3 elements" + Environment.NewLine +
                "  Values differ at index [2]" + Environment.NewLine +
                "  String lengths are both 1. Strings differ at index 0." + Environment.NewLine +
                "  Expected: \"z\"" + Environment.NewLine +
                "  But was:  \"a\"" + Environment.NewLine +
                "  -----------^" + Environment.NewLine;
            Ensure.AreEqual( set1, set2, new TestComparer() );
        }

        [TestMethod]
        public void AreEqual_HandlesNull()
        {
            object[] set1 = new object[3];
            object[] set2 = new object[3];

            Ensure.AreEqual( set1, set2 );
            Ensure.AreEqual( set1, set2, new TestComparer() );
        }

        [TestMethod]
        public void EnsureComparerIsUsed()
        {
            // Create two collections
            int[] array1 = new int[2];
            int[] array2 = new int[2];

            array1[0] = 4;
            array1[1] = 5;

            array2[0] = 99;
            array2[1] = -99;

            Ensure.AreEqual( array1, array2, new AlwaysEqualComparer() );
        }

        #endregion

        #region AreEquivalent

        [TestMethod]
        public void Equivalent()
        {
            ICollection set1 = new ICollectionAdapter( "x", "y", "z" );
            ICollection set2 = new ICollectionAdapter( "z", "y", "x" );

            Ensure.AreEquivalent( set1, set2 );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void EquivalentFailOne()
        {
            ICollection set1 = new ICollectionAdapter( "x", "y", "z" );
            ICollection set2 = new ICollectionAdapter( "x", "y", "x" );

            expectedMessage =
                "  Expected: equivalent to < \"x\", \"y\", \"z\" >" + Environment.NewLine +
                "  But was:  < \"x\", \"y\", \"x\" >" + Environment.NewLine;
            Ensure.AreEquivalent( set1, set2 );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void EquivalentFailTwo()
        {
            ICollection set1 = new ICollectionAdapter( "x", "y", "x" );
            ICollection set2 = new ICollectionAdapter( "x", "y", "z" );

            expectedMessage =
                "  Expected: equivalent to < \"x\", \"y\", \"x\" >" + Environment.NewLine +
                "  But was:  < \"x\", \"y\", \"z\" >" + Environment.NewLine;
            Ensure.AreEquivalent( set1, set2 );
        }

        [TestMethod]
        public void AreEquivalentHandlesNull()
        {
            ICollection set1 = new ICollectionAdapter( null, "x", null, "z" );
            ICollection set2 = new ICollectionAdapter( "z", null, "x", null );

            Ensure.AreEquivalent( set1, set2 );
        }

        #endregion

        #region AreNotEqual

        [TestMethod]
        public void AreNotEqual()
        {
            ArrayList set1 = new ArrayList();
            ArrayList set2 = new ArrayList();
            set1.Add( "x" );
            set1.Add( "y" );
            set1.Add( "z" );
            set2.Add( "x" );
            set2.Add( "y" );
            set2.Add( "x" );

            Ensure.AreNotEqual( set1, set2 );
            Ensure.AreNotEqual( set1, set2, new TestComparer() );
            Ensure.AreNotEqual( set1, set2, "test" );
            Ensure.AreNotEqual( set1, set2, new TestComparer(), "test" );
            Ensure.AreNotEqual( set1, set2, "test {0}", "1" );
            Ensure.AreNotEqual( set1, set2, new TestComparer(), "test {0}", "1" );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void AreNotEqual_Fails()
        {
            ArrayList set1 = new ArrayList();
            ArrayList set2 = new ArrayList();
            set1.Add( "x" );
            set1.Add( "y" );
            set1.Add( "z" );
            set2.Add( "x" );
            set2.Add( "y" );
            set2.Add( "z" );

            expectedMessage =
                "  Expected: not < \"x\", \"y\", \"z\" >" + Environment.NewLine +
                "  But was:  < \"x\", \"y\", \"z\" >" + Environment.NewLine;
            Ensure.AreNotEqual( set1, set2 );
        }

        [TestMethod]
        public void AreNotEqual_HandlesNull()
        {
            object[] set1 = new object[3];
            ArrayList set2 = new ArrayList();
            set2.Add( "x" );
            set2.Add( "y" );
            set2.Add( "z" );

            Ensure.AreNotEqual( set1, set2 );
            Ensure.AreNotEqual( set1, set2, new TestComparer() );
        }

        #endregion

        #region AreNotEquivalent

        [TestMethod]
        public void NotEquivalent()
        {
            ArrayList set1 = new ArrayList();
            ArrayList set2 = new ArrayList();

            set1.Add( "x" );
            set1.Add( "y" );
            set1.Add( "z" );

            set2.Add( "x" );
            set2.Add( "y" );
            set2.Add( "x" );

            Ensure.AreNotEquivalent( set1, set2 );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void NotEquivalent_Fails()
        {
            ArrayList set1 = new ArrayList();
            ArrayList set2 = new ArrayList();

            set1.Add( "x" );
            set1.Add( "y" );
            set1.Add( "z" );

            set2.Add( "x" );
            set2.Add( "z" );
            set2.Add( "y" );

            expectedMessage =
                "  Expected: not equivalent to < \"x\", \"y\", \"z\" >" + Environment.NewLine +
                "  But was:  < \"x\", \"z\", \"y\" >" + Environment.NewLine;
            Ensure.AreNotEquivalent( set1, set2 );
        }

        [TestMethod]
        public void NotEquivalentHandlesNull()
        {
            ArrayList set1 = new ArrayList();
            ArrayList set2 = new ArrayList();

            set1.Add( "x" );
            set1.Add( null );
            set1.Add( "z" );

            set2.Add( "x" );
            set2.Add( null );
            set2.Add( "x" );

            Ensure.AreNotEquivalent( set1, set2 );
        }

        #endregion

        #region Contains

        [TestMethod]
        public void Contains_IList()
        {
            ArrayList al = new ArrayList();
            al.Add( "x" );
            al.Add( "y" );
            al.Add( "z" );

            Ensure.Contains( "x", al );
        }

        [TestMethod]
        public void Contains_ICollection()
        {
            ICollectionAdapter ca = new ICollectionAdapter( new string[] {"x", "y", "z"} );

            Ensure.Contains( "x", ca );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void ContainsFails_ILIst()
        {
            ArrayList al = new ArrayList();
            al.Add( "x" );
            al.Add( "y" );
            al.Add( "z" );

            expectedMessage =
                "  Expected: collection containing \"a\"" + Environment.NewLine +
                "  But was:  < \"x\", \"y\", \"z\" >" + Environment.NewLine;
            Ensure.Contains( "a", al );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void ContainsFails_ICollection()
        {
            ICollectionAdapter ca = new ICollectionAdapter( new string[] {"x", "y", "z"} );

            expectedMessage =
                "  Expected: collection containing \"a\"" + Environment.NewLine +
                "  But was:  < \"x\", \"y\", \"z\" >" + Environment.NewLine;
            Ensure.Contains( "a", ca );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void ContainsFails_EmptyIList()
        {
            ArrayList al = new ArrayList();

            expectedMessage =
                "  Expected: collection containing \"x\"" + Environment.NewLine +
                "  But was:  <empty>" + Environment.NewLine;
            Ensure.Contains( "x", al );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void ContainsFails_EmptyICollection()
        {
            ICollectionAdapter ca = new ICollectionAdapter( new object[0] );

            expectedMessage =
                "  Expected: collection containing \"x\"" + Environment.NewLine +
                "  But was:  <empty>" + Environment.NewLine;
            Ensure.Contains( "x", ca );
        }

        [TestMethod]
        public void ContainsNull_IList()
        {
            Object[] oa = new object[] {1, 2, 3, null, 4, 5};
            Ensure.Contains( null, oa );
        }

        [TestMethod]
        public void ContainsNull_ICollection()
        {
            ICollectionAdapter ca = new ICollectionAdapter( new object[] {1, 2, 3, null, 4, 5} );
            Ensure.Contains( null, ca );
        }

        #endregion

        #region DoesNotContain

        [TestMethod]
        public void DoesNotContain()
        {
            ArrayList al = new ArrayList();
            al.Add( "x" );
            al.Add( "y" );
            al.Add( "z" );

            Ensure.DoesNotContain( al, "a" );
        }

        [TestMethod]
        public void DoesNotContain_Empty()
        {
            ArrayList al = new ArrayList();

            Ensure.DoesNotContain( al, "x" );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void DoesNotContain_Fails()
        {
            ArrayList al = new ArrayList();
            al.Add( "x" );
            al.Add( "y" );
            al.Add( "z" );

            expectedMessage =
                "  Expected: not collection containing \"y\"" + Environment.NewLine +
                "  But was:  < \"x\", \"y\", \"z\" >" + Environment.NewLine;
            Ensure.DoesNotContain( al, "y" );
        }

        #endregion

        #region IsSubsetOf

        [TestMethod]
        public void IsSubsetOf()
        {
            ArrayList set1 = new ArrayList();
            set1.Add( "x" );
            set1.Add( "y" );
            set1.Add( "z" );

            ArrayList set2 = new ArrayList();
            set2.Add( "y" );
            set2.Add( "z" );

            Ensure.IsSubsetOf( set2, set1 );
            EnsuranceHelper.Expect( set2, Is.SubsetOf( set1 ) );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void IsSubsetOf_Fails()
        {
            ArrayList set1 = new ArrayList();
            set1.Add( "x" );
            set1.Add( "y" );
            set1.Add( "z" );

            ArrayList set2 = new ArrayList();
            set2.Add( "y" );
            set2.Add( "z" );
            set2.Add( "a" );

            expectedMessage =
                "  Expected: subset of < \"y\", \"z\", \"a\" >" + Environment.NewLine +
                "  But was:  < \"x\", \"y\", \"z\" >" + Environment.NewLine;
            Ensure.IsSubsetOf( set1, set2 );
        }

        [TestMethod]
        public void IsSubsetOfHandlesNull()
        {
            ArrayList set1 = new ArrayList();
            set1.Add( "x" );
            set1.Add( null );
            set1.Add( "z" );

            ArrayList set2 = new ArrayList();
            set2.Add( null );
            set2.Add( "z" );

            Ensure.IsSubsetOf( set2, set1 );
            EnsuranceHelper.Expect( set2, Is.SubsetOf( set1 ) );
        }

        #endregion

        #region IsNotSubsetOf

        [TestMethod]
        public void IsNotSubsetOf()
        {
            ArrayList set1 = new ArrayList();
            set1.Add( "x" );
            set1.Add( "y" );
            set1.Add( "z" );

            ArrayList set2 = new ArrayList();
            set1.Add( "y" );
            set1.Add( "z" );
            set2.Add( "a" );

            Ensure.IsNotSubsetOf( set1, set2 );
            EnsuranceHelper.Expect( set1, Is.Not.SubsetOf( set2 ) );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void IsNotSubsetOf_Fails()
        {
            ArrayList set1 = new ArrayList();
            set1.Add( "x" );
            set1.Add( "y" );
            set1.Add( "z" );

            ArrayList set2 = new ArrayList();
            set2.Add( "y" );
            set2.Add( "z" );

            expectedMessage =
                "  Expected: not subset of < \"x\", \"y\", \"z\" >" + Environment.NewLine +
                "  But was:  < \"y\", \"z\" >" + Environment.NewLine;
            Ensure.IsNotSubsetOf( set2, set1 );
        }

        [TestMethod]
        public void IsNotSubsetOfHandlesNull()
        {
            ArrayList set1 = new ArrayList();
            set1.Add( "x" );
            set1.Add( null );
            set1.Add( "z" );

            ArrayList set2 = new ArrayList();
            set1.Add( null );
            set1.Add( "z" );
            set2.Add( "a" );

            Ensure.IsNotSubsetOf( set1, set2 );
        }

        #endregion
    }

    public class TestComparer : IComparer
    {
        #region IComparer Members

        public int Compare( object x, object y )
        {
            if ( x == null && y == null )
            {
                return 0;
            }

            if ( x == null || y == null )
            {
                return -1;
            }

            if ( x.Equals( y ) )
            {
                return 0;
            }

            return -1;
        }

        #endregion
    }

    public class AlwaysEqualComparer : IComparer
    {
        #region IComparer Members

        int IComparer.Compare( object x, object y )
        {
            // This comparer ALWAYS returns zero (equal)!
            return 0;
        }

        #endregion
    }
}