#region Copyright & License

//
// Author: Ian Davis <ian.f.davis@gmail.com> Copyright (c) 2007, Ian Davs
//
// Portions of this software were developed for NUnit. See NOTICE.txt for more
// information. 
//
// Licensed under the Apache License, Version 2.0 (the "License"); you may not
// use this file except in compliance with the License. You may obtain a copy of
// the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
// WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
// License for the specific language governing permissions and limitations under
// the License.
//

#endregion

using System;
using System.Reflection;
using Ensurance.MessageWriters;
using Ensurance.Properties;

namespace Ensurance.Constraints
{
    /// <summary>
    /// Summary description for PropertyConstraint.
    /// </summary>
    public class PropertyConstraint : PrefixConstraint
    {
        private string _name;
        private bool _propertyExists;
        private object _propValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyConstraint"/>
        /// class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="baseConstraint">The constraint to apply to the property.</param>
        public PropertyConstraint( string name, Constraint baseConstraint ) : base( baseConstraint )
        {
            _name = name;
        }

        /// <summary>
        /// Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True for success, false for failure</returns>
        public override bool Matches( object actual )
        {
            Actual = actual;

            // TODO: Should be argument exception?
            if ( actual == null )
            {
                return false;
            }

            PropertyInfo property = actual.GetType().GetProperty( _name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance );

            if ( property == null )
            {
                return _propertyExists = false;
            }

            if ( BaseConstraint == null )
            {
                return true;
            }

            _propValue = property.GetValue( actual, null );
            return BaseConstraint.Matches( _propValue );
        }

        /// <summary>
        /// Write the constraint description to a MessageWriter
        /// </summary>
        /// <exception cref="ArgumentNullException">if the message writer is null.</exception>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo( MessageWriter writer )
        {
            if ( writer == null )
            {
                throw new ArgumentNullException( "writer" );
            }

            writer.WritePredicate( string.Format( Resources.PropertyName_1, _name ) );
            BaseConstraint.WriteDescriptionTo( writer );
        }

        /// <summary>
        /// Write the actual value for a failing constraint test to a
        /// MessageWriter. The default implementation simply writes the raw
        /// value of actual, leaving it to the writer to perform any formatting.
        /// </summary>
        /// <exception cref="ArgumentNullException">if the message writer is null.</exception>
        /// <param name="writer">The writer on which the actual value is displayed</param>
        public override void WriteActualValueTo( MessageWriter writer )
        {
            if ( writer == null )
            {
                throw new ArgumentNullException( "writer" );
            }

            if ( _propertyExists )
            {
                writer.WriteActualValue( _propValue );
            }
            else
            {
                writer.WriteActualValue( Actual.GetType() );
            }
        }
    }
}