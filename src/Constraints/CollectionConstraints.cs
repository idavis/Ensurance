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
using System.Collections;
using System.Collections.Generic;
using Ensurance.MessageWriters;
using Ensurance.Properties;

namespace Ensurance.Constraints
{

    #region CollectionConstraint

    /// <summary>
    /// CollectionConstraint is the abstract base class for constraints that
    /// operate on collections.
    /// </summary>
    public abstract class CollectionConstraint : Constraint
    {
        /// <summary>
        /// Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True for success, false for failure</returns>
        public override bool Matches( object actual )
        {
            Actual = actual;

            ICollection collection = actual as ICollection;
            if ( collection == null )
            {
                throw new ArgumentException( Resources.ValueMustBeCollection, "actual");
            }

            return doMatch( collection );
        }

        /// <summary>
        /// Protected method to be implemented by derived classes
        /// </summary>
        /// <param name="collecton"></param>
        /// <returns></returns>
        protected abstract bool doMatch( ICollection collecton );

        #region Nested type: CollectionTally

        /// <summary>
        /// CollectionTally counts (tallies) the number of occurences of each
        /// object in one or more enuerations.
        /// </summary>
        protected internal class CollectionTally
        {
            // Internal dictionary used to count occurences

            // We use this for any null entries found, since the key to a
            // dictionary may not be null.
            private static object NULL = new object();
            private Dictionary<object, int> _tallyDictionary = new Dictionary<object, int>();

            /// <summary>
            /// Construct a CollectionTally object from a collection
            /// </summary>
            /// <param name="c"></param>
            public CollectionTally( IEnumerable c )
            {
                foreach (object obj in c)
                {
                    setTally( obj, getTally( obj ) + 1 );
                }
            }

            /// <summary>
            /// Get the count of the number of times an object is present in the
            /// tally
            /// </summary>
            public int this[ object obj ]
            {
                get { return getTally( obj ); }
            }

            private int getTally( object obj )
            {
                if ( obj == null )
                {
                    obj = NULL;
                }

                object val = null;
                if (_tallyDictionary.ContainsKey(obj))
                {
                    val = _tallyDictionary[obj];
                }
                
                return val == null ? 0 : (int) val;
            }

            private void setTally( object obj, int tally )
            {
                if ( obj == null )
                {
                    obj = NULL;
                }
                if (!_tallyDictionary.ContainsKey(obj))
                {
                    _tallyDictionary.Add(obj, tally);
                }
                else
                {
                    _tallyDictionary[obj] = tally;
                }
            }

            /// <summary>
            /// Remove the counts for a collection from the tally, so long as
            /// their are sufficient items to remove. The tallies are not
            /// permitted to become negative.
            /// </summary>
            /// <param name="c">The collection to remove</param>
            /// <returns>True if there were enough items to remove, otherwise false</returns>
            public bool CanRemove( IEnumerable c )
            {
                foreach (object obj in c)
                {
                    int tally = getTally( obj );
                    if ( tally > 0 )
                    {
                        setTally( obj, tally - 1 );
                    }
                    else
                    {
                        return false;
                    }
                }

                return true;
            }

            /// <summary>
            /// Test whether all the counts are equal to a given value
            /// </summary>
            /// <param name="count">The value to be looked for</param>
            /// <returns>True if all counts are equal to the value, otherwise false</returns>
            public bool AllCountsEqualTo( int count )
            {
                foreach (int entry in _tallyDictionary.Values)
                {
                    if ( entry != count )
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        #endregion
    }

    #endregion

    #region UniqueItemsConstraint

    /// <summary>
    /// UniqueItemsConstraint tests whether all the items in a collection are
    /// unique.
    /// </summary>
    public class UniqueItemsConstraint : CollectionConstraint
    {
        /// <summary>
        /// Apply the item constraint to each item in the collection, failing if
        /// any item fails.
        /// </summary>
        /// <param name="actual"></param>
        /// <returns></returns>
        protected override bool doMatch( ICollection actual )
        {
            return new CollectionTally( actual ).AllCountsEqualTo( 1 );
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

            writer.Write( "all items unique" );
        }
    }

    #endregion

    #region CollectionContainsConstraint

    /// <summary>
    /// CollectionContainsConstraint is used to test whether a collection
    /// contains an _expected object as a member.
    /// </summary>
    public class CollectionContainsConstraint : CollectionConstraint
    {
        private object _expected;

        /// <summary>
        /// Construct a CollectionContainsConstraint
        /// </summary>
        /// <param name="expected"></param>
        public CollectionContainsConstraint( object expected )
        {
            _expected = expected;
        }

        /// <summary>
        /// Test whether the expected item is contained in the collection
        /// </summary>
        /// <param name="actual"></param>
        /// <returns></returns>
        protected override bool doMatch( ICollection actual )
        {
            foreach (object obj in actual)
            {
                if ( Equals( obj, _expected ) )
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Write a descripton of the constraint to a MessageWriter
        /// </summary>
        /// <param name="writer"></param>
        public override void WriteDescriptionTo( MessageWriter writer )
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            writer.WritePredicate( "collection containing" );
            writer.WriteExpectedValue( _expected );
        }
    }

    #endregion

    #region CollectionEquivalentConstraint

    /// <summary>
    /// CollectionEquivalentCOnstraint is used to determine whether two
    /// collections are equivalent.
    /// </summary>
    public class CollectionEquivalentConstraint : CollectionConstraint
    {
        private IEnumerable _expected;

        /// <summary>
        /// Construct a CollectionEquivalentConstraint
        /// </summary>
        /// <param name="expected"></param>
        public CollectionEquivalentConstraint( IEnumerable expected )
        {
            _expected = expected;
        }

        /// <summary>
        /// Test whether two collections are equivalent
        /// </summary>
        /// <param name="actual"></param>
        /// <returns></returns>
        protected override bool doMatch( ICollection actual )
        {
            // This is just an optimization
            if ( _expected is ICollection )
            {
                if ( actual.Count != ( (ICollection) _expected ).Count )
                {
                    return false;
                }
            }

            CollectionTally tally = new CollectionTally( _expected );
            return tally.CanRemove( actual ) && tally.AllCountsEqualTo( 0 );
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
            writer.WritePredicate( "equivalent to" );
            writer.WriteExpectedValue( _expected );
        }
    }

    #endregion

    #region CollectionSubsetConstraint

    /// <summary>
    /// CollectionSubsetConstraint is used to determine whether one collection
    /// is a subset of another
    /// </summary>
    public class CollectionSubsetConstraint : CollectionConstraint
    {
        private IEnumerable _expected;

        /// <summary>
        /// Construct a CollectionSubsetConstraint
        /// </summary>
        /// <param name="expected">The collection that the actual value is expected to be a subset of</param>
        public CollectionSubsetConstraint( IEnumerable expected )
        {
            _expected = expected;
        }

        /// <summary>
        /// Test whether the actual collection is a subset of the expected
        /// collection provided.
        /// </summary>
        /// <param name="actual"></param>
        /// <returns></returns>
        protected override bool doMatch( ICollection actual )
        {
            return new CollectionTally( _expected ).CanRemove( actual );
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
            writer.WritePredicate( "subset of" );
            writer.WriteExpectedValue( _expected );
        }
    }

    #endregion
}