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
    /// Summary description for BetweenConstraint.
    /// </summary>
    public class RangeConstraint : Constraint
    {
        private IComparable _high;
        private bool _includeHigh = false;
        private bool _includeLow = true;
        private IComparable _low;

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeConstraint"/> class.
        /// </summary>
        /// <param name="low">The low.</param>
        /// <param name="high">The high.</param>
        /// <param name="includeLow">if set to <c>true</c> [include low].</param>
        /// <param name="includeHigh">if set to <c>true</c> [include high].</param>
        public RangeConstraint( IComparable low, IComparable high, bool includeLow, bool includeHigh )
        {
            _low = low;
            _high = high;
            _includeLow = includeLow;
            _includeHigh = includeHigh;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeConstraint"/> class.
        /// </summary>
        /// <param name="low">The low.</param>
        /// <param name="high">The high.</param>
        /// <param name="inclusive">if set to <c>true</c> [inclusive].</param>
        public RangeConstraint( IComparable low, IComparable high, bool inclusive ) : this( low, high, inclusive, inclusive )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeConstraint"/> class.
        /// </summary>
        /// <param name="low">The low.</param>
        /// <param name="high">The high.</param>
        public RangeConstraint( IComparable low, IComparable high ) : this( low, high, true, false )
        {
        }

        /// <summary>
        /// Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True for success, false for failure</returns>
        public override bool Matches( object actual )
        {
            _actual = actual;

            if ( actual == null )
            {
                return false;
            }

            Type actualType = actual.GetType();
            if ( actualType != _low.GetType() || actualType != _high.GetType() )
            {
                return false;
            }

            int lowCompare = _low.CompareTo( actual );
            if ( lowCompare > 0 || !_includeLow && lowCompare == 0 )
            {
                return false;
            }

            int highCompare = _high.CompareTo( actual );
            return highCompare > 0 || _includeHigh && highCompare == 0;
        }

        /// <summary>
        /// Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        public override void WriteDescriptionTo( MessageWriter writer )
        {
            writer.WritePredicate( "between" );
            writer.WriteExpectedValue( _low );
            writer.WriteConnector( "and" );
            writer.WriteExpectedValue( _high );
        }
    }
}