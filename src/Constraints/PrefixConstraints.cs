// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org/?p=license&r=2.4
// ****************************************************************

using System;
using System.Collections;
using Ensurance.MessageWriters;

namespace Ensurance.Constraints
{

    #region PrefixConstraint

    /// <summary>
    /// Abstract base class used for prefixes
    /// </summary>
    public abstract class PrefixConstraint : Constraint
    {
        /// <summary>
        /// The base constraint
        /// </summary>
        protected Constraint _baseConstraint;

        /// <summary>
        /// Construct given a base constraint
        /// </summary>
        /// <param name="baseConstraint"></param>
        protected PrefixConstraint( Constraint baseConstraint )
        {
            _baseConstraint = baseConstraint;
        }

        /// <summary>
        /// Set all modifiers applied to the prefix into
        /// the base constraint before matching
        /// </summary>
        protected void PassModifiersToBase()
        {
            if ( _caseInsensitive )
            {
                _baseConstraint = _baseConstraint.IgnoreCase;
            }
            if ( _tolerance != null )
            {
                _baseConstraint = _baseConstraint.Within( _tolerance );
            }
            if ( _compareAsCollection )
            {
                _baseConstraint = _baseConstraint.AsCollection;
            }
            if ( _compareWith != null )
            {
                _baseConstraint = _baseConstraint.Comparer( _compareWith );
            }
        }
    }

    #endregion

    #region NotConstraint

    /// <summary>
    /// NotConstraint negates the effect of some other constraint
    /// </summary>
    public class NotConstraint : PrefixConstraint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotConstraint"/> class.
        /// </summary>
        /// <param name="baseConstraint">The base constraint to be negated.</param>
        public NotConstraint( Constraint baseConstraint ) : base( baseConstraint )
        {
        }

        /// <summary>
        /// Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True for if the base constraint fails, false if it succeeds</returns>
        public override bool Matches( object actual )
        {
            _actual = actual;
            PassModifiersToBase();
            return !_baseConstraint.Matches( actual );
        }

        /// <summary>
        /// Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo( MessageWriter writer )
        {
            writer.WritePredicate( "not" );
            _baseConstraint.WriteDescriptionTo( writer );
        }

        /// <summary>
        /// Write the actual value for a failing constraint test to a MessageWriter.
        /// </summary>
        /// <param name="writer">The writer on which the actual value is displayed</param>
        public override void WriteActualValueTo( MessageWriter writer )
        {
            _baseConstraint.WriteActualValueTo( writer );
        }
    }

    #endregion

    #region AllItemsConstraint

    /// <summary>
    /// AllItemsConstraint applies another constraint to each
    /// item in a collection, succeeding if they all succeed.
    /// </summary>
    public class AllItemsConstraint : PrefixConstraint
    {
        /// <summary>
        /// Construct an AllItemsConstraint on top of an existing constraint
        /// </summary>
        /// <param name="itemConstraint"></param>
        public AllItemsConstraint( Constraint itemConstraint ) : base( itemConstraint )
        {
        }

        /// <summary>
        /// Apply the item constraint to each item in the collection,
        /// failing if any item fails.
        /// </summary>
        /// <param name="actual"></param>
        /// <returns></returns>
        public override bool Matches( object actual )
        {
            _actual = actual;

            PassModifiersToBase();

            ICollection actualCollection = actual as ICollection;
            if ( actualCollection == null )
            {
                throw new ArgumentException( "The actual value must be a collection", "actual" );
            }

            foreach (object item in actualCollection)
            {
                if ( !_baseConstraint.Matches( item ) )
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Write a description of this constraint to a MessageWriter
        /// </summary>
        /// <param name="writer"></param>
        public override void WriteDescriptionTo( MessageWriter writer )
        {
            writer.WritePredicate( "all items" );
            _baseConstraint.WriteDescriptionTo( writer );
        }
    }

    #endregion

    #region SomeItemsConstraint

    /// <summary>
    /// SomeItemsConstraint applies another constraint to each
    /// item in a collection, succeeding if any of them succeeds.
    /// </summary>
    public class SomeItemsConstraint : PrefixConstraint
    {
        /// <summary>
        /// Construct a SomeItemsConstraint on top of an existing constraint
        /// </summary>
        /// <param name="itemConstraint"></param>
        public SomeItemsConstraint( Constraint itemConstraint ) : base( itemConstraint )
        {
        }

        /// <summary>
        /// Apply the item constraint to each item in the collection,
        /// failing if any item fails.
        /// </summary>
        /// <param name="actual"></param>
        /// <returns></returns>
        public override bool Matches( object actual )
        {
            _actual = actual;

            PassModifiersToBase();

            if ( !( actual is ICollection ) )
            {
                throw new ArgumentException( "The actual value must be a collection", "actual" );
            }

            foreach (object item in (ICollection) actual)
            {
                if ( _baseConstraint.Matches( item ) )
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Write a description of this constraint to a MessageWriter
        /// </summary>
        /// <param name="writer"></param>
        public override void WriteDescriptionTo( MessageWriter writer )
        {
            writer.WritePredicate( "some item" );
            _baseConstraint.WriteDescriptionTo( writer );
        }
    }

    #endregion

    #region NoItemConstraint

    /// <summary>
    /// SomeItemsConstraint applies another constraint to each
    /// item in a collection, succeeding if any of them succeeds.
    /// </summary>
    public class NoItemConstraint : PrefixConstraint
    {
        /// <summary>
        /// Construct a SomeItemsConstraint on top of an existing constraint
        /// </summary>
        /// <param name="itemConstraint"></param>
        public NoItemConstraint( Constraint itemConstraint ) : base( itemConstraint )
        {
        }

        /// <summary>
        /// Apply the item constraint to each item in the collection,
        /// failing if any item fails.
        /// </summary>
        /// <param name="actual"></param>
        /// <returns></returns>
        public override bool Matches( object actual )
        {
            _actual = actual;

            PassModifiersToBase();

            if ( !( actual is ICollection ) )
            {
                throw new ArgumentException( "The actual value must be a collection", "actual" );
            }

            foreach (object item in (ICollection) actual)
            {
                if ( _baseConstraint.Matches( item ) )
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Write a description of this constraint to a MessageWriter
        /// </summary>
        /// <param name="writer"></param>
        public override void WriteDescriptionTo( MessageWriter writer )
        {
            writer.WritePredicate( "no item" );
            _baseConstraint.WriteDescriptionTo( writer );
        }
    }

    #endregion
}