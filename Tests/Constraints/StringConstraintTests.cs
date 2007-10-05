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
using Ensurance.MessageWriters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ensurance.Tests.Constraints
{
    [TestClass]
    public class SubstringTest : ConstraintTestBase
    {
        [TestInitialize]
        public void SetUp()
        {
            Matcher = new SubstringConstraint( "hello" );
            GoodValues = new object[] {"hello", "hello there", "I said hello", "say hello to fred"};
            BadValues = new object[] {"goodbye", "What the hell?", string.Empty, null};
            Description = "String containing \"hello\"";
        }

        public void HandleException( Exception ex )
        {
            Ensure.That( ex.Message, new EqualConstraint(
                                         TextMessageWriter.Pfx_Expected + "String containing \"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa...\"" + Environment.NewLine +
                                         TextMessageWriter.Pfx_Actual + "\"xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx...\"" + Environment.NewLine ) );
        }
    }

    [TestClass]
    public class SubstringTestIgnoringCase : ConstraintTestBase
    {
        [TestInitialize]
        public void SetUp()
        {
            Matcher = new SubstringConstraint( "hello" ).IgnoreCase;
            GoodValues = new object[] {"Hello", "HellO there", "I said HELLO", "say hello to fred"};
            BadValues = new object[] {"goodbye", "What the hell?", string.Empty, null};
            Description = "String containing \"hello\", ignoring case";
        }
    }

    [TestClass]
    public class StartsWithTest : ConstraintTestBase
    {
        [TestInitialize]
        public void SetUp()
        {
            Matcher = new StartsWithConstraint( "hello" );
            GoodValues = new object[] {"hello", "hello there"};
            BadValues = new object[] {"goodbye", "What the hell?", "I said hello", "say hello to fred", string.Empty, null};
            Description = "String starting with \"hello\"";
        }
    }

    [TestClass]
    public class StartsWithTestIgnoringCase : ConstraintTestBase
    {
        [TestInitialize]
        public void SetUp()
        {
            Matcher = new StartsWithConstraint( "hello" ).IgnoreCase;
            GoodValues = new object[] {"Hello", "HELLO there"};
            BadValues = new object[] {"goodbye", "What the hell?", "I said hello", "say Hello to fred", string.Empty, null};
            Description = "String starting with \"hello\", ignoring case";
        }
    }

    [TestClass]
    public class EndsWithTest : ConstraintTestBase
    {
        [TestInitialize]
        public void SetUp()
        {
            Matcher = new EndsWithConstraint( "hello" );
            GoodValues = new object[] {"hello", "I said hello"};
            BadValues = new object[] {"goodbye", "What the hell?", "hello there", "say hello to fred", string.Empty, null};
            Description = "String ending with \"hello\"";
        }
    }

    [TestClass]
    public class EndsWithTestIgnoringCase : ConstraintTestBase //, IExpectException
    {
        [TestInitialize]
        public void SetUp()
        {
            Matcher = new EndsWithConstraint( "hello" ).IgnoreCase;
            GoodValues = new object[] {"HELLO", "I said Hello"};
            BadValues = new object[] {"goodbye", "What the hell?", "hello there", "say hello to fred", string.Empty, null};
            Description = "String ending with \"hello\", ignoring case";
        }
    }

    [TestClass]
    public class EqualIgnoringCaseTest : ConstraintTestBase
    {
        [TestInitialize]
        public void SetUp()
        {
            Matcher = new EqualConstraint( "Hello World!" ).IgnoreCase;
            GoodValues = new object[] {"hello world!", "Hello World!", "HELLO world!"};
            BadValues = new object[] {"goodbye", "Hello Friends!", string.Empty, null};
            Description = "\"Hello World!\", ignoring case";
        }
    }
}