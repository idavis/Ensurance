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
using Ensurance.Properties;

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
        private Constraint _baseConstraint;

        /// <summary>
        /// Construct given a base constraint
        /// </summary>
        /// <param name="baseConstraint"></param>
        protected PrefixConstraint( Constraint baseConstraint )
        {
            _baseConstraint = baseConstraint;
        }

        /// <summary>
        /// The base constraint
        /// </summary>
        protected internal Constraint BaseConstraint
        {
            get { return _baseConstraint; }
            set { _baseConstraint = value; }
        }

        /// <summary>
        /// Set all modifiers applied to the prefix into
        /// the base constraint before matching
        /// </summary>
        protected void PassModifiersToBase()
        {
            if ( CaseInsensitive )
            {
                _baseConstraint = _baseConstraint.IgnoreCase;
            }
            if ( Tolerance != null )
            {
                _baseConstraint = _baseConstraint.Within( Tolerance );
            }
            if ( CompareAsCollection )
            {
                _baseConstraint = _baseConstraint.AsCollection;
            }
            if ( CompareWith != null )
            {
                _baseConstraint = _baseConstraint.Comparer( CompareWith );
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
            Actual = actual;
            PassModifiersToBase();
            return !BaseConstraint.Matches( actual );
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
            writer.WritePredicate( "not" );
            BaseConstraint.WriteDescriptionTo( writer );
        }

        /// <summary>
        /// Write the actual value for a failing constraint test to a MessageWriter.
        /// </summary>
        /// <param name="writer">The writer on which the actual value is displayed</param>
        public override void WriteActualValueTo( MessageWriter writer )
        {
            BaseConstraint.WriteActualValueTo( writer );
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
            Actual = actual;

            PassModifiersToBase();

            ICollection actualCollection = actual as ICollection;
            if ( actualCollection == null )
            {
                throw new ArgumentException( Resources.ValueMustBeCollection, "actual" );
            }

            foreach (object item in actualCollection)
            {
                if ( !BaseConstraint.Matches( item ) )
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
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            writer.WritePredicate( "all items" );
            BaseConstraint.WriteDescriptionTo( writer );
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
            Actual = actual;

            PassModifiersToBase();

            if ( !( actual is ICollection ) )
            {
                throw new ArgumentException( Resources.ValueMustBeCollection, "actual");
            }

            foreach (object item in (ICollection) actual)
            {
                if ( BaseConstraint.Matches( item ) )
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
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }

            writer.WritePredicate( "some item" );
            BaseConstraint.WriteDescriptionTo( writer );
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
            Actual = actual;

            PassModifiersToBase();

            if ( !( actual is ICollection ) )
            {
                throw new ArgumentException( Resources.ValueMustBeCollection, "actual");
            }

            foreach (object item in (ICollection) actual)
            {
                if ( BaseConstraint.Matches( item ) )
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
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            writer.WritePredicate( "no item" );
            BaseConstraint.WriteDescriptionTo( writer );
        }
    }

    #endregion
}