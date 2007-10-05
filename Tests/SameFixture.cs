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
    public class SameFixture : MessageChecker
    {
        [TestMethod]
        public void Same()
        {
            string s1 = "S1";
            Ensure.AreSame( s1, s1 );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void SameFails()
        {
            Exception ex1 = new Exception( "one" );
            Exception ex2 = new Exception( "two" );
            expectedMessage =
                "  Expected: same as <System.Exception: one>" + Environment.NewLine +
                "  But was:  <System.Exception: two>" + Environment.NewLine;
            Ensure.AreSame( ex1, ex2 );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void SameValueTypes()
        {
            int index = 2;
            expectedMessage =
                "  Expected: same as 2" + Environment.NewLine +
                "  But was:  2" + Environment.NewLine;
            Ensure.AreSame( index, index );
        }
    }
}