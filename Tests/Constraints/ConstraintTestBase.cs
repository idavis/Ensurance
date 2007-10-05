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
using Ensurance.MessageWriters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ensurance.Tests.Constraints
{
    [TestClass]
    public class ConstraintTestBase
    {
        protected object[] BadValues;
        protected string Description;
        protected object[] GoodValues;
        protected Constraint Matcher;

        [TestMethod]
        public void SucceedsOnGoodValues()
        {
            if ( Matcher != null )
            {
                // We are being called from an inhereting class
                foreach (object value in GoodValues)
                {
                    Ensure.That( value, Matcher, "Test should succeed with {0}", value );
                }
            }
        }

        [TestMethod]
        public void FailsOnBadValues()
        {
            if ( Matcher != null )
            {
                // We are being called from an inhereting class
                foreach (object value in BadValues)
                {
                    Ensure.That( Matcher.Matches( value ), new EqualConstraint( false ), "Test should fail with value {0}", value );
                }
            }
        }

        [TestMethod]
        public void ProvidesProperDescription()
        {
            if ( Matcher != null )
            {
                // We are being called from an inhereting class
                TextMessageWriter writer = new TextMessageWriter();
                Matcher.WriteDescriptionTo( writer );
                Ensure.That( writer.ToString(), new EqualConstraint( Description ), null );
            }
        }

        [TestMethod]
        public void ProvidesProperFailureMessage()
        {
            if ( Matcher != null )
            {
                // We are being called from an inhereting class
                object badValue = BadValues[0];
                string badString = badValue == null ? "null" : badValue.ToString();

                TextMessageWriter writer = new TextMessageWriter();
                Matcher.Matches( badValue );
                Matcher.WriteMessageTo( writer );
                Ensure.That( writer.ToString(), new SubstringConstraint( Description ) );
                Ensure.That( writer.ToString(), new SubstringConstraint( badString ) );
                Ensure.That( writer.ToString(), new NotConstraint( new SubstringConstraint( "<UNSET>" ) ) );
            }
        }
    }
}