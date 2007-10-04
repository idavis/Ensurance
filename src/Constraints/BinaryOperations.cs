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

using Ensurance.MessageWriters;

namespace Ensurance.Constraints
{
    /// <summary>
    /// BinaryOperation is the abstract base of all constraints
    /// that combine two other constraints in some fashion.
    /// </summary>
    public abstract class BinaryOperation : Constraint
    {
        /// <summary>
        /// The first constraint being combined
        /// </summary>
        protected Constraint left;

        /// <summary>
        /// The second constraint being combined
        /// </summary>
        protected Constraint right;

        /// <summary>
        /// Construct a BinaryOperation from two other constraints
        /// </summary>
        /// <param name="left">The first constraint</param>
        /// <param name="right">The second constraint</param>
        public BinaryOperation( Constraint left, Constraint right )
        {
            this.left = left;
            this.right = right;
        }
    }

    /// <summary>
    /// AndConstraint succeeds only if both members succeed.
    /// </summary>
    public class AndConstraint : BinaryOperation
    {
        /// <summary>
        /// Create an AndConstraint from two other constraints
        /// </summary>
        /// <param name="left">The first constraint</param>
        /// <param name="right">The second constraint</param>
        public AndConstraint( Constraint left, Constraint right )
            : base( left, right )
        {
        }

        /// <summary>
        /// Apply both member constraints to an actual value, succeeding 
        /// succeeding only if both of them succeed.
        /// </summary>
        /// <param name="actual">The actual value</param>
        /// <returns>True if the constraints both succeeded</returns>
        public override bool Matches( object actual )
        {
            _actual = actual;
            return left.Matches( actual ) && right.Matches( actual );
        }

        /// <summary>
        /// Write a description for this contraint to a MessageWriter
        /// </summary>
        /// <param name="writer">The MessageWriter to receive the description</param>
        public override void WriteDescriptionTo( MessageWriter writer )
        {
            left.WriteDescriptionTo( writer );
            writer.WriteConnector( "and" );
            right.WriteDescriptionTo( writer );
        }
    }

    /// <summary>
    /// OrConstraint succeeds if either member succeeds
    /// </summary>
    public class OrConstraint : BinaryOperation
    {
        /// <summary>
        /// Create an OrConstraint from two other constraints
        /// </summary>
        /// <param name="left">The first constraint</param>
        /// <param name="right">The second constraint</param>
        public OrConstraint( Constraint left, Constraint right )
            : base( left, right )
        {
        }

        /// <summary>
        /// Apply the member constraints to an actual value, succeeding 
        /// succeeding as soon as one of them succeeds.
        /// </summary>
        /// <param name="actual">The actual value</param>
        /// <returns>True if either constraint succeeded</returns>
        public override bool Matches( object actual )
        {
            _actual = actual;
            return left.Matches( actual ) || right.Matches( actual );
        }

        /// <summary>
        /// Write a description for this contraint to a MessageWriter
        /// </summary>
        /// <param name="writer">The MessageWriter to receive the description</param>
        public override void WriteDescriptionTo( MessageWriter writer )
        {
            left.WriteDescriptionTo( writer );
            writer.WriteConnector( "or" );
            right.WriteDescriptionTo( writer );
        }
    }
}