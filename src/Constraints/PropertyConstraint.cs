// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org/?p=license&r=2.4
// ****************************************************************

using System.Reflection;
using Ensurance.MessageWriters;

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
        /// Initializes a new instance of the <see cref="PropertyConstraint"/> class.
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
            _actual = actual;

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

            if ( _baseConstraint == null )
            {
                return true;
            }

            _propValue = property.GetValue( actual, null );
            return _baseConstraint.Matches( _propValue );
        }

        /// <summary>
        /// Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo( MessageWriter writer )
        {
            writer.WritePredicate( "Property \"" + _name + "\"" );
            _baseConstraint.WriteDescriptionTo( writer );
        }

        /// <summary>
        /// Write the actual value for a failing constraint test to a
        /// MessageWriter. The default implementation simply writes
        /// the raw value of actual, leaving it to the writer to
        /// perform any formatting.
        /// </summary>
        /// <param name="writer">The writer on which the actual value is displayed</param>
        public override void WriteActualValueTo( MessageWriter writer )
        {
            if ( _propertyExists )
            {
                writer.WriteActualValue( _propValue );
            }
            else
            {
                writer.WriteActualValue( _actual.GetType() );
            }
        }
    }
}