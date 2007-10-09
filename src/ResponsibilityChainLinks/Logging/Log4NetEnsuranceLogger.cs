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
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using Ensurance.Properties;
using log4net;
using log4net.Config;

namespace Ensurance.ResponsibilityChainLinks.Logging
{
    public class Log4NetEnsuranceLogger : EnsuranceLoggerBase
    {
        private static readonly ILog _logger;
        private static readonly bool _isDebugEnabled, _isInfoEnabled, _isWarnEnabled, _isErrorEnabled, _isFatalEnabled;

        /// <summary>
        /// Initializes the <see cref="Log4NetEnsuranceLogger"/> class.
        /// </summary>
        static Log4NetEnsuranceLogger()
        {
            // We need to tell Ensurance where to find the config file for the
            // application loading it.
            XmlConfiguratorAttribute[] log4netConfigAttributes = Assembly.GetEntryAssembly().GetCustomAttributes( typeof (XmlConfiguratorAttribute), false ) as XmlConfiguratorAttribute[];
            System.Diagnostics.Debug.Assert( log4netConfigAttributes != null );

            if ( log4netConfigAttributes.Length == 0 )
            {
                using (MemoryStream log4netConfigStream = new MemoryStream( Encoding.UTF8.GetBytes( Resources.DefaultLoggingConfiguration ) ))
                {
                    XmlConfigurator.Configure( log4netConfigStream );
                }
            }
            else
            {
                foreach (XmlConfiguratorAttribute xmlConfiguratorAttribute in log4netConfigAttributes)
                {
                    xmlConfiguratorAttribute.ConfigFileExtension = xmlConfiguratorAttribute.ConfigFileExtension ?? "config";
                    string configFileName = String.Format( CultureInfo.CurrentCulture, "{0}.{1}", AppDomain.CurrentDomain.FriendlyName, xmlConfiguratorAttribute.ConfigFileExtension );
                    xmlConfiguratorAttribute.ConfigFile = Path.Combine( AppDomain.CurrentDomain.BaseDirectory, configFileName );
                    if ( File.Exists( xmlConfiguratorAttribute.ConfigFile ) )
                    {
                        XmlConfigurator.Configure( new FileInfo( xmlConfiguratorAttribute.ConfigFile ) );
                    }
                }
            }

            // With the configutation loaded, we can now initialize the logger.
            _logger = LogManager.GetLogger( _type );

            // The JIT compiler can optimize the if statements below if we pull
            // them off here.
            _isDebugEnabled = _logger.IsDebugEnabled;
            _isInfoEnabled = _logger.IsInfoEnabled;
            _isWarnEnabled = _logger.IsWarnEnabled;
            _isErrorEnabled = _logger.IsErrorEnabled;
            _isFatalEnabled = _logger.IsFatalEnabled;
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>The logger.</value>
        protected static ILog Logger
        {
            get { return _logger; }
        }

        /// <summary>
        /// Writes the message formatting it with the supplied arguments with
        /// LogLevel.Debug severity.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public override void Debug( string message, params object[] args )
        {
            if ( _isDebugEnabled )
            {
                _logger.DebugFormat( message, args );
            }
        }

        /// <summary>
        /// Writes the message formatting it with the supplied arguments with
        /// LogLevel.Info severity.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public override void Info( string message, params object[] args )
        {
            if ( _isInfoEnabled )
            {
                _logger.InfoFormat( message, args );
            }
        }

        /// <summary>
        /// Writes the message formatting it with the supplied arguments with
        /// LogLevel.Warn severity.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public override void Warn( string message, params object[] args )
        {
            if ( _isWarnEnabled )
            {
                _logger.WarnFormat( message, args );
            }
        }

        /// <summary>
        /// Writes the message formatting it with the supplied arguments with
        /// LogLevel.Error severity.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public override void Error( string message, params object[] args )
        {
            if ( _isErrorEnabled )
            {
                _logger.ErrorFormat( message, args );
            }
        }

        /// <summary>
        /// Writes the message formatting it with the supplied arguments with
        /// LogLevel.Fatal severity.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public override void Fatal( string message, params object[] args )
        {
            if ( _isFatalEnabled )
            {
                _logger.FatalFormat( message, args );
            }
        }
    }
}