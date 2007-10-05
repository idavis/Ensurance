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
    public class ExactTypeTest : ConstraintTestBase
    {
        [TestInitialize]
        public void SetUp()
        {
            Matcher = new ExactTypeConstraint( typeof (D1) );
            GoodValues = new object[] {new D1()};
            BadValues = new object[] {new B(), new D2()};
            Description = "<NUnit.Framework.Constraints.Tests.D1>";
        }
    }

    [TestClass]
    public class InstanceOfTypeTest : ConstraintTestBase
    {
        [TestInitialize]
        public void SetUp()
        {
            Matcher = new InstanceOfTypeConstraint( typeof (D1) );
            GoodValues = new object[] {new D1(), new D2()};
            BadValues = new object[] {new B()};
            Description = "instance of <NUnit.Framework.Constraints.Tests.D1>";
        }
    }

    [TestClass]
    public class AssignableFromTest : ConstraintTestBase
    {
        [TestInitialize]
        public void SetUp()
        {
            Matcher = new AssignableFromConstraint( typeof (D1) );
            GoodValues = new object[] {new D1(), new B()};
            BadValues = new object[] {new D2()};
            Description = "Type assignable from <NUnit.Framework.Constraints.Tests.D1>";
        }
    }

    internal class B
    {
    }

    internal class D1 : B
    {
    }

    internal class D2 : D1
    {
    }
}