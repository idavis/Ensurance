// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org/?p=license&r=2.4
// ****************************************************************

namespace Ensurance.Constraints
{
    /// <summary>
    /// Abstract base class used for prefixes
    /// </summary>
    public abstract class AbstractPrefixConstraint : Constraint
    {
        /// <summary>
        /// The base constraint
        /// </summary>
        protected Constraint _baseConstraint;

        /// <summary>
        /// Construct given a base constraint
        /// </summary>
        /// <param name="baseConstraint"></param>
        protected AbstractPrefixConstraint( Constraint baseConstraint )
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
}