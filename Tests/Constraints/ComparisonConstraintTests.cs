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
using Ensurance.Constraints;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ensurance.Tests.Constraints
{
    [TestClass]
    public class ComparisonConstraintTestBase : ConstraintTestBase
    {
        [TestMethod, ExpectedException( typeof (ArgumentNullException) )]
        public void NullGivesError()
        {
            object actual = null;
            Ensure.That( actual, Matcher );
        }

        [TestMethod, ExpectedException( typeof (ArgumentNullException) )]
        public void BadTypeGivesError()
        {
            Ensure.That( "big", Matcher );
        }
    }

    [TestClass]
    public class GreaterThanTest : ComparisonConstraintTestBase
    {
        [TestInitialize]
        public void SetUp()
        {
            Matcher = new GreaterThanConstraint( 5 );
            GoodValues = new object[] {6};
            BadValues = new object[] {4, 5};
            Description = "greater than 5";
        }
    }

    [TestClass]
    public class GreaterThanOrEqualTest : ComparisonConstraintTestBase
    {
        [TestInitialize]
        public void SetUp()
        {
            Matcher = new GreaterThanOrEqualConstraint( 5 );
            GoodValues = new object[] {6, 5};
            BadValues = new object[] {4};
            Description = "greater than or equal to 5";
        }
    }

    [TestClass]
    public class LessThanTest : ComparisonConstraintTestBase
    {
        [TestInitialize]
        public void SetUp()
        {
            Matcher = new LessThanConstraint( 5 );
            GoodValues = new object[] {4};
            BadValues = new object[] {6, 5};
            Description = "less than 5";
        }
    }

    [TestClass]
    public class LessThanOrEqualTest : ComparisonConstraintTestBase
    {
        [TestInitialize]
        public void SetUp()
        {
            Matcher = new LessThanOrEqualConstraint( 5 );
            GoodValues = new object[] {4, 5};
            BadValues = new object[] {6};
            Description = "less than or equal to 5";
        }
    }
}