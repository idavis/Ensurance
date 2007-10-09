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

namespace Ensurance.ResponsibilityChainLinks.Logging
{
    public abstract class EnsuranceLoggerBase : IEnsuranceLogger
    {
        protected static readonly Type _type = typeof (Ensure);
        private LogSeverity _defaultLogSeverity;

        #region IEnsuranceLogger Members

        /// <summary>
        /// Gets or sets the default log severity.
        /// </summary>
        /// <value>The default log severity.</value>
        public LogSeverity DefaultLogSeverity
        {
            get { return _defaultLogSeverity; }
            set { _defaultLogSeverity = value; }
        }

        /// <summary>
        /// Writes the message formatting it with the supplied arguments
        /// with LogLevel.Debug severity.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public abstract void Debug( string message, params object[] args );

        /// <summary>
        /// Writes the message formatting it with the supplied arguments
        /// with LogLevel.Info severity.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public abstract void Info( string message, params object[] args );

        /// <summary>
        /// Writes the message formatting it with the supplied arguments
        /// with LogLevel.Warn severity.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public abstract void Warn( string message, params object[] args );

        /// <summary>
        /// Writes the message formatting it with the supplied arguments
        /// with LogLevel.Error severity.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public abstract void Error( string message, params object[] args );

        /// <summary>
        /// Writes the message formatting it with the supplied arguments
        /// with LogLevel.Fatal severity.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public abstract void Fatal( string message, params object[] args );

        #endregion
    }
}