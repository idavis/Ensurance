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
using Ensurance.MessageWriters;

namespace Ensurance.Constraints
{
    /// <summary>
    /// The Constraint class is the base of all built-in or
    /// user-defined constraints in NUnit. It provides the operator
    /// overloads used to combine constraints.
    /// </summary>
    public abstract class Constraint
    {
        #region UnsetObject Class

        /// <summary>
        /// Class used to detect any derived constraints
        /// that fail to set the actual value in their
        /// Matches override.
        /// </summary>
        private class UnsetObject
        {
            public override string ToString()
            {
                return "UNSET";
            }
        }

        #endregion

        #region Static and Instance Fields

        /// <summary>
        /// Static UnsetObject used to detect derived constraints
        /// failing to set the actual value.
        /// </summary>
        protected static readonly object UNSET = new UnsetObject();

        /// <summary>
        /// The actual value being tested against a constraint
        /// </summary>
        private object _actual = UNSET;

        /// <summary>
        /// If true, all string comparisons will ignore case
        /// </summary>
        private bool _caseInsensitive;

        /// <summary>
        /// If true, arrays will be treated as collections, allowing
        /// those of different dimensions to be compared
        /// </summary>
        private bool _compareAsCollection;

        /// <summary>
        /// IComparer object used in comparisons for some constraints.
        /// </summary>
        private IComparer _compareWith;

        /// <summary>
        /// If non-zero, equality comparisons within the specified 
        /// tolerance will succeed.
        /// </summary>
        private object _tolerance;

        #endregion

        #region Properties

        /// <summary>
        /// Flag the constraint to ignore case and return self.
        /// </summary>
        public virtual Constraint IgnoreCase
        {
            get
            {
                _caseInsensitive = true;
                return this;
            }
        }

        /// <summary>
        /// Flag the constraint to compare arrays as collections
        /// and return self.
        /// </summary>
        public Constraint AsCollection
        {
            get
            {
                _compareAsCollection = true;
                return this;
            }
        }

        internal virtual bool IsMappable
        {
            get { return false; }
        }

        /// <summary>
        /// The actual value being tested against a constraint
        /// </summary>
        protected internal object Actual
        {
            get { return _actual; }
            set { _actual = value; }
        }

        /// <summary>
        /// If true, all string comparisons will ignore case
        /// </summary>
        protected internal bool CaseInsensitive
        {
            get { return _caseInsensitive; }
            set { _caseInsensitive = value; }
        }

        /// <summary>
        /// If true, arrays will be treated as collections, allowing
        /// those of different dimensions to be compared
        /// </summary>
        protected internal bool CompareAsCollection
        {
            get { return _compareAsCollection; }
            set { _compareAsCollection = value; }
        }

        /// <summary>
        /// IComparer object used in comparisons for some constraints.
        /// </summary>
        protected internal IComparer CompareWith
        {
            get { return _compareWith; }
            set { _compareWith = value; }
        }

        /// <summary>
        /// If non-zero, equality comparisons within the specified 
        /// tolerance will succeed.
        /// </summary>
        protected internal object Tolerance
        {
            get { return _tolerance; }
            set { _tolerance = value; }
        }

        /// <summary>
        /// Flag the constraint to use a tolerance when determining equality.
        /// Currently only used for doubles and floats.
        /// </summary>
        /// <param name="tolerance">Tolerance to be used</param>
        /// <returns>Self.</returns>
        public Constraint Within( object tolerance )
        {
            _tolerance = tolerance;
            return this;
        }

        /// <summary>
        /// Flag the constraint to use the supplied IComparer object.
        /// </summary>
        /// <param name="comparer">The IComparer object to use.</param>
        /// <returns>Self.</returns>
        public Constraint Comparer( IComparer comparer )
        {
            _compareWith = comparer;
            return this;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Write the failure message to the MessageWriter provided
        /// as an argument. The default implementation simply passes
        /// the constraint and the actual value to the writer, which
        /// then displays the constraint description and the value.
        /// 
        /// Constraints that need to provide additional details,
        /// such as where the error occured can override this.
        /// </summary>
        /// <param name="writer">The MessageWriter on which to display the message</param>
        public virtual void WriteMessageTo( MessageWriter writer )
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            writer.DisplayDifferences( this );
        }

        /// <summary>
        /// Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True for success, false for failure</returns>
        public abstract bool Matches( object actual );

        /// <summary>
        /// Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public abstract void WriteDescriptionTo( MessageWriter writer );

        /// <summary>
        /// Write the actual value for a failing constraint test to a
        /// MessageWriter. The default implementation simply writes
        /// the raw value of actual, leaving it to the writer to
        /// perform any formatting.
        /// </summary>
        /// <param name="writer">The writer on which the actual value is displayed</param>
        public virtual void WriteActualValueTo( MessageWriter writer )
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            writer.WriteActualValue( _actual );
        }

        #endregion

        #region Operator Overloads

        /// <summary>
        /// This operator creates a constraint that is satisfied only if both 
        /// argument constraints are satisfied.
        /// </summary>
        public static Constraint operator &( Constraint left, Constraint right )
        {
            return new AndConstraint( left, right );
        }

        /// <summary>
        /// This operator creates a constraint that is satisfied only if both 
        /// argument constraints are satisfied.
        /// </summary>
        public static Constraint BitwiseAnd( Constraint left, Constraint right )
        {
            return new AndConstraint(left, right);
        }

        /// <summary>
        /// This operator creates a constraint that is satisfied if either 
        /// of the argument constraints is satisfied.
        /// </summary>
        public static Constraint operator |( Constraint left, Constraint right )
        {
            return new OrConstraint( left, right );
        }

        /// <summary>
        /// This operator creates a constraint that is satisfied if either 
        /// of the argument constraints is satisfied.
        /// </summary>
        public static Constraint BitwiseOr( Constraint left, Constraint right )
        {
            return new OrConstraint( left, right );
        }

        /// <summary>
        /// This operator creates a constraint that is satisfied if the 
        /// argument constraint is not satisfied.
        /// </summary>
        public static Constraint operator !( Constraint m )
        {
            return new NotConstraint( m ?? new EqualConstraint( null ) );
        }

        /// <summary>
        /// This operator creates a constraint that is satisfied if the 
        /// argument constraint is not satisfied.
        /// </summary>
        public static Constraint LogicalNot( Constraint m )
        {
            return new NotConstraint( m ?? new EqualConstraint( null ) );
        }

        #endregion
    }
}