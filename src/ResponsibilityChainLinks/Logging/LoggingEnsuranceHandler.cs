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
using System.Diagnostics;
using System.Globalization;
using Ensurance.Constraints;
using Ensurance.MessageWriters;

namespace Ensurance.ResponsibilityChainLinks.Logging
{
#if !DEBUG
    [DebuggerNonUserCode]
#endif
    public class LoggingEnsuranceHandler : IEnsuranceResponsibilityChainLink
    {
        private IEnsuranceLogger _logger;

        private IEnsuranceResponsibilityChainLink _successor;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingEnsuranceHandler"/> class.
        /// </summary>
        public LoggingEnsuranceHandler() : this( new Log4NetEnsuranceLogger() )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingEnsuranceHandler"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public LoggingEnsuranceHandler( IEnsuranceLogger logger ) : this( logger, LogSeverity.Error )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingEnsuranceHandler"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="logSeverity">The log severity.</param>
        public LoggingEnsuranceHandler( IEnsuranceLogger logger, LogSeverity logSeverity )
        {
            if ( logger == null )
            {
                throw new ArgumentNullException( "Logger cannot be null." );
            }
            _logger = logger;
            _logger.DefaultLogSeverity = logSeverity;
        }

        #region IEnsuranceResponsibilityChainLink Members

        /// <summary>
        /// Gets or sets the successor.
        /// </summary>
        /// <value>The successor.</value>
        public IEnsuranceResponsibilityChainLink Successor
        {
            get { return _successor; }
            set { _successor = value; }
        }

        #endregion

        #region IEnsuranceHandler Members

        /// <summary>
        /// Handles an Ensurance failure for the given constraint. Implementors
        /// should always call
        /// <code>
        /// IEnsuranceResponsibilityChainLink handler = successor;
        /// if( handler != null)
        /// {
        ///     handler.Handle( constraint, message, args );
        /// }
        /// </code>
        /// So that the downstream handler can have a chance to process the failure.
        /// </summary>
        /// <param name="constraint">The constraint.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The args.</param>
        public void Handle( Constraint constraint, string message, params object[] args )
        {
            try
            {
                MessageWriter messagewriter = new TextMessageWriter();
                messagewriter.WriteLine();
                constraint.WriteMessageTo( messagewriter );
                messagewriter.Write( new StackTraceWriter().ToString() );

                string tmpMessage = String.Format( CultureInfo.CurrentCulture, "{0}{1}", message, messagewriter );

                switch ( _logger.DefaultLogSeverity )
                {
                    case LogSeverity.Debug:
                        _logger.Debug( tmpMessage, args );
                        break;
                    case LogSeverity.Info:
                        _logger.Info( tmpMessage, args );
                        break;
                    case LogSeverity.Warn:
                        _logger.Warn( tmpMessage, args );
                        break;
                    case LogSeverity.Error:
                        _logger.Error( tmpMessage, args );
                        break;
                    case LogSeverity.Fatal:
                        _logger.Fatal( tmpMessage, args );
                        break;
                }
            }
            finally
            {
                IEnsuranceResponsibilityChainLink handler = _successor;
                if ( handler != null )
                {
                    handler.Handle( constraint, message, args );
                }
            }
        }

        #endregion
    }
}