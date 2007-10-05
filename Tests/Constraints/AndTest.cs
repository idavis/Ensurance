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

using Ensurance.Constraints;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ensurance.Tests.Constraints
{
    [TestClass]
    public class AndTest : ConstraintTestBase
    {
        [TestInitialize]
        public void SetUp()
        {
            Matcher = new AndConstraint( new GreaterThanConstraint( 40 ), new LessThanConstraint( 50 ) );
            GoodValues = new object[] {42};
            BadValues = new object[] {37, 53};
            Description = "greater than 40 and less than 50";
        }

        [TestMethod]
        public void CanCombineTestsWithAndOperator()
        {
            Ensure.That( 42, new GreaterThanConstraint( 40 ) & new LessThanConstraint( 50 ) );
        }
    }
}