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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ensurance.Tests
{
    [TestClass]
    public class NotSameFixture : MessageChecker
    {
        private readonly string s1 = "S1";
        private readonly string s2 = "S2";

        [TestMethod]
        public void NotSame()
        {
            Ensure.AreNotSame( s1, s2 );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void NotSameFails()
        {
            expectedMessage =
                "  Expected: not same as \"S1\"" + Environment.NewLine +
                "  But was:  \"S1\"" + Environment.NewLine;
            Ensure.AreNotSame( s1, s1 );
        }
    }
}