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

using Ensurance.MessageWriters;

namespace Ensurance
{
    /// <summary>
    /// Summary description for IConstraint.
    /// </summary>
    public interface IConstraint
    {
        /// <summary>
        /// Write the failure message to the MessageWriter provided as an
        /// argument.
        /// </summary>
        /// <param name="writer">The MessageWriter on which to display the message</param>
        void WriteMessageTo( MessageWriter writer );

        /// <summary>
        /// Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True for success, false for failure</returns>
        bool Matches( object actual );

        /// <summary>
        /// Write the constraint description to a MessageWriter
        /// </summary>
        /// <param name="writer">The writer on which the description is displayed</param>
        void WriteDescriptionTo( MessageWriter writer );

        /// <summary>
        /// Write the actual value for a failing constraint test to a
        /// MessageWriter. Depending on the specific constraint, this might be
        /// something other than a the raw value... such as the actual Type for
        /// certain Type Constraints.
        /// </summary>
        /// <param name="writer"></param>
        void WriteActualValueTo( MessageWriter writer );
    }
}