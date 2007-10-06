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
    /// <summary>
    /// This is a test class for Ensurance.EnsureBase&lt;T&gt; methods
    /// that take predicates as parameters.
    ///</summary>
    [TestClass]
    public class EnsurePredicateTests
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

        public bool ReturnParamValue( bool value )
        {
            return value;
        }

        public bool ThrowArgumentNullException( bool value )
        {
            throw new ArgumentNullException();
        }

        #region Predicates Pass

        /// <summary>
        ///A test for IsFalse (Predicate&lt;T&gt;, T)
        ///</summary>
        [TestMethod]
        public void IsFalsePassesTest()
        {
            Ensure.IsFalse( ReturnParamValue, false );
        }

        /// <summary>
        ///A test for IsFalse (Predicate&lt;T&gt;, T, string)
        ///</summary>
        [TestMethod]
        public void IsFalseWithMessagePassesTest()
        {
            string message = null;

            Ensure.IsFalse( ReturnParamValue, false, message );
        }

        /// <summary>
        ///A test for IsFalse (Predicate&lt;T&gt;, T, string, params object[])
        ///</summary>
        [TestMethod]
        public void IsFalseWithMessageAndArgsPassesTest()
        {
            string message = null;

            object[] args = null;

            Ensure.IsFalse( ReturnParamValue, false, message, args );
        }

        /// <summary>
        ///A test for IsTrue (Predicate&lt;T&gt;, T)
        ///</summary>
        [TestMethod]
        public void IsTruePassesTest()
        {
            Ensure.IsTrue( ReturnParamValue, true );
        }

        /// <summary>
        ///A test for IsTrue (Predicate&lt;T&gt;, T, string)
        ///</summary>
        [TestMethod]
        public void IsTrueWithMessagePassesTest()
        {
            string message = null;

            Ensure.IsTrue( ReturnParamValue, true, message );
        }

        /// <summary>
        ///A test for IsTrue (Predicate&lt;T&gt;, T, string, params object[])
        ///</summary>
        [TestMethod]
        public void IsTrueWithMessageAndArgsPassesTest()
        {
            string message = null;

            object[] args = null;

            Ensure.IsTrue( ReturnParamValue, true, message, args );
        }

        /// <summary>
        ///A test for That (Predicate&lt;T&gt;, T)
        ///</summary>
        [TestMethod]
        public void ThatPassesTest()
        {
            Ensure.That( ReturnParamValue, true );
        }

        /// <summary>
        ///A test for That (Predicate&lt;T&gt;, T, string)
        ///</summary>
        [TestMethod]
        public void ThatWithMessagePassesTest()
        {
            string message = null;

            Ensure.That( ReturnParamValue, true, message );
        }

        /// <summary>
        ///A test for That (Predicate&lt;T&gt;, T, string, params object[])
        ///</summary>
        [TestMethod]
        public void ThatWithMessageAndArgsPassesTest()
        {
            string message = null;

            object[] args = null;

            Ensure.That( ReturnParamValue, true, message, args );
        }

        #endregion

        #region Predicates Pass

        /// <summary>
        ///A test for IsFalse (Predicate&lt;T&gt;, T)
        ///</summary>
        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void IsFalseFailsTest()
        {
            Ensure.IsFalse( ReturnParamValue, true );
        }

        /// <summary>
        ///A test for IsFalse (Predicate&lt;T&gt;, T, string)
        ///</summary>
        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void IsFalseWithMessageFailsTest()
        {
            string message = null;

            Ensure.IsFalse( ReturnParamValue, true, message );
        }

        /// <summary>
        ///A test for IsFalse (Predicate&lt;T&gt;, T, string, params object[])
        ///</summary>
        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void IsFalseWithMessageAndArgsFailsTest()
        {
            string message = null;

            object[] args = null;

            Ensure.IsFalse( ReturnParamValue, true, message, args );
        }

        /// <summary>
        ///A test for IsTrue (Predicate&lt;T&gt;, T)
        ///</summary>
        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void IsTrueFailsTest()
        {
            Ensure.IsTrue( ReturnParamValue, false );
        }

        /// <summary>
        ///A test for IsTrue (Predicate&lt;T&gt;, T, string)
        ///</summary>
        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void IsTrueWithMessageFailsTest()
        {
            string message = null;

            Ensure.IsTrue( ReturnParamValue, false, message );
        }

        /// <summary>
        ///A test for IsTrue (Predicate&lt;T&gt;, T, string, params object[])
        ///</summary>
        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void IsTrueWithMessageAndArgsFailsTest()
        {
            string message = null;

            object[] args = null;

            Ensure.IsTrue( ReturnParamValue, false, message, args );
        }

        /// <summary>
        ///A test for That (Predicate&lt;T&gt;, T)
        ///</summary>
        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void ThatFailsTest()
        {
            Ensure.That( ReturnParamValue, false );
        }

        /// <summary>
        ///A test for That (Predicate&lt;T&gt;, T, string)
        ///</summary>
        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void ThatWithMessageFailsTest()
        {
            string message = null;

            Ensure.That( ReturnParamValue, false, message );
        }

        /// <summary>
        ///A test for That (Predicate&lt;T&gt;, T, string, params object[])
        ///</summary>
        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void ThatWithMessageAndArgsFailsTest()
        {
            string message = null;

            object[] args = null;

            Ensure.That( ReturnParamValue, false, message, args );
        }

        #endregion

        #region Null Predicates

        /// <summary>
        ///A test for IsFalse (Predicate&lt;T&gt;, T)
        ///</summary>
        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void IsFalseWithNullPredicateFailsTest()
        {
            Ensure.IsFalse( null, true );
        }

        /// <summary>
        ///A test for IsTrue (Predicate&lt;T&gt;, T)
        ///</summary>
        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void IsTrueWithNullPredicateFailsTest()
        {
            Ensure.IsTrue( null, false );
        }

        /// <summary>
        ///A test for That (Predicate&lt;T&gt;, T)
        ///</summary>
        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void ThatWithNullPredicateFailsTest()
        {
            Ensure.That( null, false );
        }

        #endregion

        #region Predicate Throws Exception

        /// <summary>
        ///A test for IsFalse (Predicate&lt;T&gt;, T)
        ///</summary>
        [TestMethod, ExpectedException( typeof (ArgumentNullException) )]
        public void IsFalseWithPredicateExceptionFailsTest()
        {
            Ensure.IsFalse( ThrowArgumentNullException, true );
        }

        /// <summary>
        ///A test for IsTrue (Predicate&lt;T&gt;, T)
        ///</summary>
        [TestMethod, ExpectedException( typeof (ArgumentNullException) )]
        public void IsTrueWithPredicateExceptionFailsTest()
        {
            Ensure.IsTrue( ThrowArgumentNullException, false );
        }

        /// <summary>
        ///A test for That (Predicate&lt;T&gt;, T)
        ///</summary>
        [TestMethod, ExpectedException( typeof (ArgumentNullException) )]
        public void ThatWithPredicateExceptionFailsTest()
        {
            Ensure.That( ThrowArgumentNullException, false );
        }

        #endregion
    }
}