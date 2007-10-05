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
using Ensurance.Constraints;
using Ensurance.MessageWriters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ensurance.Tests.Constraints
{
    [TestClass]
    public class AllItemsTests : MessageChecker
    {
        [TestMethod]
        public void AllItemsAreNotNull()
        {
            object[] c = new object[] {1, "hello", 3, Environment.OSVersion};
            Ensure.That( c, new AllItemsConstraint( new NotConstraint( new EqualConstraint( null ) ) ) );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void AllItemsAreNotNullFails()
        {
            object[] c = new object[] {1, "hello", null, 3};
            expectedMessage =
                TextMessageWriter.Pfx_Expected + "all items not null" + Environment.NewLine +
                TextMessageWriter.Pfx_Actual + "< 1, \"hello\", null, 3 >" + Environment.NewLine;
            Ensure.That( c, new AllItemsConstraint( new NotConstraint( new EqualConstraint( null ) ) ) );
        }

        [TestMethod]
        public void AllItemsAreInRange()
        {
            int[] c = new int[] {12, 27, 19, 32, 45, 99, 26};
            Ensure.That( c, new AllItemsConstraint( new GreaterThanConstraint( 10 ) & new LessThanConstraint( 100 ) ) );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void AllItemsAreInRangeFailureMessage()
        {
            int[] c = new int[] {12, 27, 19, 32, 107, 99, 26};
            expectedMessage =
                TextMessageWriter.Pfx_Expected + "all items greater than 10 and less than 100" + Environment.NewLine +
                TextMessageWriter.Pfx_Actual + "< 12, 27, 19, 32, 107, 99, 26 >" + Environment.NewLine;
            Ensure.That( c, new AllItemsConstraint( new GreaterThanConstraint( 10 ) & new LessThanConstraint( 100 ) ) );
        }

        [TestMethod]
        public void AllItemsAreInstancesOfType()
        {
            object[] c = new object[] {'a', 'b', 'c'};
            Ensure.That( c, new AllItemsConstraint( new InstanceOfTypeConstraint( typeof (char) ) ) );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void AllItemsAreInstancesOfTypeFailureMessage()
        {
            object[] c = new object[] {'a', "b", 'c'};
            expectedMessage =
                TextMessageWriter.Pfx_Expected + "all items instance of <System.Char>" + Environment.NewLine +
                TextMessageWriter.Pfx_Actual + "< 'a', \"b\", 'c' >" + Environment.NewLine;
            Ensure.That( c, new AllItemsConstraint( new InstanceOfTypeConstraint( typeof (char) ) ) );
        }
    }

    [TestClass]
    public class CollectionContainsTests
    {
        [TestMethod]
        public void CanTestContentsOfArray()
        {
            object item = "xyz";
            object[] c = new object[] {123, item, "abc"};
            Ensure.That( c, new CollectionContainsConstraint( item ) );
        }

        [TestMethod]
        public void CanTestContentsOfArrayList()
        {
            object item = "xyz";
            ArrayList list = new ArrayList( new object[] {123, item, "abc"} );
            Ensure.That( list, new CollectionContainsConstraint( item ) );
        }

        [TestMethod]
        public void CanTestContentsOfSortedList()
        {
            object item = "xyz";
            SortedList list = new SortedList();
            list.Add( "a", 123 );
            list.Add( "b", item );
            list.Add( "c", "abc" );
            Ensure.That( list.Values, new CollectionContainsConstraint( item ) );
            Ensure.That( list.Keys, new CollectionContainsConstraint( "b" ) );
        }

        [TestMethod]
        public void CanTestContentsOfCollectionNotImplementingIList()
        {
            ICollectionAdapter ints = new ICollectionAdapter( new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9} );
            Ensure.That( ints, new CollectionContainsConstraint( 9 ) );
        }
    }
}