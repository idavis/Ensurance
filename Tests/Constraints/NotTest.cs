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
using Ensurance.SyntaxHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ensurance.Tests.Constraints
{
    [TestClass]
    public class NotTest : ConstraintTestBase
    {
        [TestInitialize]
        public void SetUp()
        {
            Matcher = new NotConstraint( new EqualConstraint( null ) );
            GoodValues = new object[] {42, "Hello"};
            BadValues = new object[] {null};
            Description = "not null";
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void NotHonorsIgnoreCaseUsingConstructors()
        {
            Ensure.That( "abc", new NotConstraint( new EqualConstraint( "ABC" ).IgnoreCase ) );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void NotHonorsIgnoreCaseUsingPrefixNotation()
        {
            Ensure.That( "abc", Is.Not.EqualTo( "ABC" ).IgnoreCase );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void NotHonorsTolerance()
        {
            Ensure.That( 4.99d, Is.Not.EqualTo( 5.0d ).Within( .05d ) );
        }

        [TestMethod]
        public void CanUseNotOperator()
        {
            Ensure.That( 42, !new EqualConstraint( 99 ) );
        }
    }
}