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
using System.Runtime.Serialization;

namespace Ensurance
{
    /// <summary>
    /// Thrown when an ensurance failed.
    /// </summary>
    [Serializable]
    public class EnsuranceException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnsuranceException"/> class.
        /// </summary>
        public EnsuranceException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnsuranceException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception</param>
        public EnsuranceException( string message ) : base( message )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnsuranceException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception</param>
        /// <param name="inner">The exception that caused the current exception</param>
        public EnsuranceException( string message, Exception inner ) : base( message, inner )
        {
        }

        /// <summary>
        /// Serialization Constructor
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected EnsuranceException( SerializationInfo info,
                                      StreamingContext context ) : base( info, context )
        {
        }
    }
}