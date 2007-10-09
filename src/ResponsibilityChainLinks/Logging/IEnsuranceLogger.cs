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

namespace Ensurance.ResponsibilityChainLinks.Logging
{
    public interface IEnsuranceLogger
    {
        /// <summary>
        /// Gets or sets the default log severity.
        /// </summary>
        /// <value>The default log severity.</value>
        LogSeverity DefaultLogSeverity { get; set; }

        /// <summary>
        /// Writes the message formatting it with the supplied arguments with
        /// LogLevel.Debug severity.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        void Debug( string message, params object[] args );

        /// <summary>
        /// Writes the message formatting it with the supplied arguments with
        /// LogLevel.Info severity.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        void Info( string message, params object[] args );

        /// <summary>
        /// Writes the message formatting it with the supplied arguments with
        /// LogLevel.Warn severity.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        void Warn( string message, params object[] args );

        /// <summary>
        /// Writes the message formatting it with the supplied arguments with
        /// LogLevel.Error severity.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        void Error( string message, params object[] args );

        /// <summary>
        /// Writes the message formatting it with the supplied arguments with
        /// LogLevel.Fatal severity.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        void Fatal( string message, params object[] args );
    }
}