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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ensurance.Tests
{
    [TestClass]
    public class ConditionAssertTests : MessageChecker
    {
        [TestMethod]
        public void IsTrue()
        {
            Ensure.IsTrue( true );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void IsTrueFails()
        {
            expectedMessage =
                "  Expected: True" + Environment.NewLine +
                "  But was:  False" + Environment.NewLine;
            Ensure.IsTrue( false );
        }

        [TestMethod]
        public void IsFalse()
        {
            Ensure.IsFalse( false );
        }

        [TestMethod]
        [ExpectedException( typeof (EnsuranceException) )]
        public void IsFalseFails()
        {
            expectedMessage =
                "  Expected: False" + Environment.NewLine +
                "  But was:  True" + Environment.NewLine;
            Ensure.IsFalse( true );
        }

        [TestMethod]
        public void IsNull()
        {
            Ensure.IsNull( null );
        }

        [TestMethod]
        [ExpectedException( typeof (EnsuranceException) )]
        public void IsNullFails()
        {
            String s1 = "S1";
            expectedMessage =
                "  Expected: null" + Environment.NewLine +
                "  But was:  \"S1\"" + Environment.NewLine;
            Ensure.IsNull( s1 );
        }

        [TestMethod]
        public void IsNotNull()
        {
            String s1 = "S1";
            Ensure.IsNotNull( s1 );
        }

        [TestMethod]
        [ExpectedException( typeof (EnsuranceException) )]
        public void IsNotNullFails()
        {
            expectedMessage =
                "  Expected: not null" + Environment.NewLine +
                "  But was:  null" + Environment.NewLine;
            Ensure.IsNotNull( null );
        }

        [TestMethod]
        public void IsNaN()
        {
            Ensure.IsNaN( double.NaN );
        }

        [TestMethod]
        [ExpectedException( typeof (EnsuranceException) )]
        public void IsNaNFails()
        {
            expectedMessage =
                "  Expected: NaN" + Environment.NewLine +
                "  But was:  10.0d" + Environment.NewLine;
            Ensure.IsNaN( 10.0 );
        }

        [TestMethod]
        public void IsEmpty()
        {
            Ensure.IsEmpty( "", "Failed on empty String" );
            Ensure.IsEmpty( new int[0], "Failed on empty Array" );
            Ensure.IsEmpty( new ArrayList(), "Failed on empty ArrayList" );
            Ensure.IsEmpty( new Hashtable(), "Failed on empty Hashtable" );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void IsEmptyFailsOnString()
        {
            expectedMessage =
                "  Expected: <empty>" + Environment.NewLine +
                "  But was:  \"Hi!\"" + Environment.NewLine;
            Ensure.IsEmpty( "Hi!" );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void IsEmptyFailsOnNullString()
        {
            expectedMessage =
                "  Expected: <empty>" + Environment.NewLine +
                "  But was:  null" + Environment.NewLine;
            Ensure.IsEmpty( (string) null );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void IsEmptyFailsOnNonEmptyArray()
        {
            expectedMessage =
                "  Expected: <empty>" + Environment.NewLine +
                "  But was:  < 1, 2, 3 >" + Environment.NewLine;
            Ensure.IsEmpty( new int[] {1, 2, 3} );
        }

        [TestMethod]
        public void IsNotEmpty()
        {
            int[] array = new int[] {1, 2, 3};
            ArrayList list = new ArrayList( array );
            Hashtable hash = new Hashtable();
            hash.Add( "array", array );

            Ensure.IsNotEmpty( "Hi!", "Failed on String" );
            Ensure.IsNotEmpty( array, "Failed on Array" );
            Ensure.IsNotEmpty( list, "Failed on ArrayList" );
            Ensure.IsNotEmpty( hash, "Failed on Hashtable" );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void IsNotEmptyFailsOnEmptyString()
        {
            expectedMessage =
                "  Expected: not <empty>" + Environment.NewLine +
                "  But was:  <string.Empty>" + Environment.NewLine;
            Ensure.IsNotEmpty( "" );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void IsNotEmptyFailsOnEmptyArray()
        {
            expectedMessage =
                "  Expected: not <empty>" + Environment.NewLine +
                "  But was:  <empty>" + Environment.NewLine;
            Ensure.IsNotEmpty( new int[0] );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void IsNotEmptyFailsOnEmptyArrayList()
        {
            expectedMessage =
                "  Expected: not <empty>" + Environment.NewLine +
                "  But was:  <empty>" + Environment.NewLine;
            Ensure.IsNotEmpty( new ArrayList() );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void IsNotEmptyFailsOnEmptyHashTable()
        {
            expectedMessage =
                "  Expected: not <empty>" + Environment.NewLine +
                "  But was:  <empty>" + Environment.NewLine;
            Ensure.IsNotEmpty( new Hashtable() );
        }
    }
}