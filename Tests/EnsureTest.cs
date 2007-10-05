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

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ensurance.Tests
{
    /// <summary>
    ///This is a test class for Ensurance.Ensure.With and is intended
    ///to contain all Ensurance.Ensure.With Unit Tests
    ///</summary>
    [TestClass()]
    public class WithTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void EnsureWithThrowThrows()
        {
            Ensure.With.Throw.AreEqual( 5, 4 );
        }

        [TestMethod]
        public void EnsureWithThrowDoesNotThrow()
        {
            Ensure.With.Throw.AreEqual( 5, 5 );
        }

        //[TestMethod]
        //public void EnsureWithDebuggingBreaks()
        //{
        //    Ensure.With.Debugging.AreEqual(5, 4);
        //}
    }
}