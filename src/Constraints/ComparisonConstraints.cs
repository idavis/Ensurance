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
using Ensurance.MessageWriters;

namespace Ensurance.Constraints
{
    /// <summary>
    /// Abstract base class for constraints that compare values to determine if
    /// one is greater than, equal to or less than the other.
    /// </summary>
    public abstract class ComparisonConstraint : Constraint
    {
        /// <summary>
        /// if true, equal returns success
        /// </summary>
        private bool _equalReturnsOK;

        /// <summary>
        /// The value against which a comparison is to be made
        /// </summary>
        private IComparable _expected;

        /// <summary>
        /// if true, greater than returns success
        /// </summary>
        private bool _greaterThanReturnsOK;

        /// <summary>
        /// If true, less than returns success
        /// </summary>
        private bool _lessThanReturnsOK;

        /// <summary>
        /// The predicate used as a part of the description
        /// </summary>
        private string _predicate;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComparisonConstraint"/>
        /// class.
        /// </summary>
        /// <param name="value">The value against which to make a comparison.</param>
        /// <param name="ltOK">if set to <c>true</c> less succeeds.</param>
        /// <param name="eqOK">if set to <c>true</c> equal succeeds.</param>
        /// <param name="gtOK">if set to <c>true</c> greater succeeds.</param>
        /// <param name="predicate">String used in describing the constraint.</param>
        protected ComparisonConstraint( IComparable value, bool ltOK, bool eqOK, bool gtOK, string predicate )
        {
            _expected = value;
            _lessThanReturnsOK = ltOK;
            EqualReturnsOK = eqOK;
            _greaterThanReturnsOK = gtOK;
            _predicate = predicate;
        }

        /// <summary>
        /// if true, equal returns success
        /// </summary>
        protected internal bool EqualReturnsOK
        {
            get { return _equalReturnsOK; }
            set { _equalReturnsOK = value; }
        }

        /// <summary>
        /// The value against which a comparison is to be made
        /// </summary>
        protected internal IComparable Expected
        {
            get { return _expected; }
            set { _expected = value; }
        }

        /// <summary>
        /// if true, greater than returns success
        /// </summary>
        protected internal bool GreaterThanReturnsOK
        {
            get { return _greaterThanReturnsOK; }
            set { _greaterThanReturnsOK = value; }
        }

        /// <summary>
        /// If true, less than returns success
        /// </summary>
        protected internal bool LessThanReturnsOK
        {
            get { return _lessThanReturnsOK; }
            set { _lessThanReturnsOK = value; }
        }

        /// <summary>
        /// Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True for success, false for failure</returns>
        public override bool Matches( object actual )
        {
            Actual = actual;

            int icomp = Numerics.Compare( _expected, actual );
            return icomp < 0 && _greaterThanReturnsOK || icomp == 0 && EqualReturnsOK || icomp > 0 && _lessThanReturnsOK;
        }

        /// <summary>
        /// Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo( MessageWriter writer )
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            writer.WritePredicate( _predicate );
            writer.WriteExpectedValue( _expected );
        }
    }

    /// <summary>
    /// Tests whether a value is greater than the value supplied to its
    /// constructor
    /// </summary>
    public class GreaterThanConstraint : ComparisonConstraint
    {
        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="GreaterThanConstraint"/> class.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        public GreaterThanConstraint( IComparable expected ) : base( expected, false, false, true, "greater than" )
        {
        }
    }

    /// <summary>
    /// Tests whether a value is greater than or equal to the value supplied to
    /// its constructor
    /// </summary>
    public class GreaterThanOrEqualConstraint : ComparisonConstraint
    {
        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="GreaterThanOrEqualConstraint"/> class.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        public GreaterThanOrEqualConstraint( IComparable expected ) : base( expected, false, true, true, "greater than or equal to" )
        {
        }
    }

    /// <summary>
    /// Tests whether a value is less than the value supplied to its constructor
    /// </summary>
    public class LessThanConstraint : ComparisonConstraint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LessThanConstraint"/>
        /// class.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        public LessThanConstraint( IComparable expected ) : base( expected, true, false, false, "less than" )
        {
        }
    }

    /// <summary>
    /// Tests whether a value is less than or equal to the value supplied to its
    /// constructor
    /// </summary>
    public class LessThanOrEqualConstraint : ComparisonConstraint
    {
        /// <summary>
        /// Initializes a new instance of the <see
        /// cref="LessThanOrEqualConstraint"/> class.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        public LessThanOrEqualConstraint( IComparable expected ) : base( expected, true, true, false, "less than or equal to" )
        {
        }
    }
}