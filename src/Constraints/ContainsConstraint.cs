// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org/?p=license&r=2.4
// ****************************************************************

using Ensurance.MessageWriters;

namespace Ensurance.Constraints
{
    /// <summary>
    /// ContainsConstraint tests a whether a string contains a substring
    /// or a collection contains an object. It postpones the decision of
    /// which test to use until the type of the actual argument is known.
    /// This allows testing whether a string is contained in a collection
    /// or as a substring of another string using the same syntax.
    /// </summary>
    public class ContainsConstraint : Constraint
    {
        private object _expected;
        private Constraint _realConstraint;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainsConstraint"/> class.
        /// </summary>
        /// <param name="expected">The expected.</param>
        public ContainsConstraint( object expected )
        {
            _expected = expected;
        }

        private Constraint RealConstraint
        {
            get
            {
                if ( _realConstraint == null )
                {
                    if ( _actual is string )
                    {
                        _realConstraint = new SubstringConstraint( (string) _expected );
                    }
                    else
                    {
                        _realConstraint = new CollectionContainsConstraint( _expected );
                    }
                }

                return _realConstraint;
            }
            set { _realConstraint = value; }
        }

        /// <summary>
        /// Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True for success, false for failure</returns>
        public override bool Matches( object actual )
        {
            _actual = actual;

            if ( _caseInsensitive )
            {
                RealConstraint = RealConstraint.IgnoreCase;
            }

            return RealConstraint.Matches( actual );
        }

        /// <summary>
        /// Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo( MessageWriter writer )
        {
            RealConstraint.WriteDescriptionTo( writer );
        }
    }
}