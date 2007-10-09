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
using Ensurance.MessageWriters;

namespace Ensurance.Constraints
{
    /// <summary>
    /// TypeConstraint is the abstract base for constraints
    /// that take a Type as their expected value.
    /// </summary>
    public abstract class TypeConstraint : Constraint
    {
        /// <summary>
        /// The expected Type used by the constraint
        /// </summary>
        private Type _expectedType;

        /// <summary>
        /// Construct a TypeConstraint for a given Type
        /// </summary>
        /// <param name="type"></param>
        protected TypeConstraint( Type type )
        {
            _expectedType = type;
        }

        /// <summary>
        /// The expected Type used by the constraint
        /// </summary>
        protected internal Type ExpectedType
        {
            get { return _expectedType; }
            set { _expectedType = value; }
        }

        /// <summary>
        /// Write the actual value for a failing constraint test to a
        /// MessageWriter. TypeCOnstraints override this method to write
        /// the name of the type.
        /// </summary>
        /// <param name="writer">The writer on which the actual value is displayed</param>
        public override void WriteActualValueTo( MessageWriter writer )
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }

            writer.WriteActualValue( Actual == null ? null : Actual.GetType() );
        }
    }

    /// <summary>
    /// ExactTypeConstraint is used to test that an object
    /// is of the exact type provided in the constructor
    /// </summary>
    public class ExactTypeConstraint : TypeConstraint
    {
        /// <summary>
        /// Construct an ExactTypeConstraint for a given Type
        /// </summary>
        /// <param name="type"></param>
        public ExactTypeConstraint( Type type ) : base( type )
        {
        }

        /// <summary>
        /// Test that an object is of the exact type specified
        /// </summary>
        /// <param name="actual"></param>
        /// <returns></returns>
        public override bool Matches( object actual )
        {
            Actual = actual;
            return actual != null && actual.GetType() == ExpectedType;
        }

        /// <summary>
        /// Write the description of this constraint to a MessageWriter
        /// </summary>
        /// <param name="writer"></param>
        public override void WriteDescriptionTo( MessageWriter writer )
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            writer.WriteExpectedValue( ExpectedType );
        }
    }

    /// <summary>
    /// InstanceOfTypeConstraint is used to test that an object
    /// is of the same type provided or derived from it.
    /// </summary>
    public class InstanceOfTypeConstraint : TypeConstraint
    {
        /// <summary>
        /// Construct an InstanceOfTypeConstraint for the type provided
        /// </summary>
        /// <param name="type"></param>
        public InstanceOfTypeConstraint( Type type ) : base( type )
        {
        }

        /// <summary>
        /// Test whether an object is of the specified type or a derived type
        /// </summary>
        /// <param name="actual"></param>
        /// <returns></returns>
        public override bool Matches( object actual )
        {
            Actual = actual;
            return actual != null && ExpectedType.IsInstanceOfType( actual );
        }

        /// <summary>
        /// Write a description of this constraint to a MessageWriter
        /// </summary>
        /// <param name="writer"></param>
        public override void WriteDescriptionTo( MessageWriter writer )
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            writer.WritePredicate( "instance of" );
            writer.WriteExpectedValue( ExpectedType );
        }
    }

    /// <summary>
    /// AssignableFromConstraint is used to test that an object
    /// can be assigned from a given Type.
    /// </summary>
    public class AssignableFromConstraint : TypeConstraint
    {
        /// <summary>
        /// Construct an AssignableFromConstraint for the type provided
        /// </summary>
        /// <param name="type"></param>
        public AssignableFromConstraint( Type type ) : base( type )
        {
        }

        /// <summary>
        /// Test whether an object can be assigned from the specified type
        /// </summary>
        /// <param name="actual"></param>
        /// <returns></returns>
        public override bool Matches( object actual )
        {
            Actual = actual;
            return actual != null && actual.GetType().IsAssignableFrom( ExpectedType );
        }

        /// <summary>
        /// Write a description of this constraint to a MessageWriter
        /// </summary>
        /// <param name="writer"></param>
        public override void WriteDescriptionTo( MessageWriter writer )
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            writer.WritePredicate( "Type assignable from" );
            writer.WriteExpectedValue( ExpectedType );
        }
    }
}