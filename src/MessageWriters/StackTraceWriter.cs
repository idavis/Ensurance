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
using System.IO;
using System.Reflection;
using System.Text;
using Ensurance.Constraints;

namespace Ensurance.MessageWriters
{
    /// <summary>
    /// 
    /// </summary>
#if !DEBUG
    [DebuggerNonUserCode]
#endif

    public class StackTraceWriter : TextMessageWriter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StackTraceWriter"/> class.
        /// </summary>
        public StackTraceWriter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StackTraceWriter"/> class.
        /// </summary>
        /// <param name="textWriter">The textWriter.</param>
        public StackTraceWriter( TextWriter textWriter ) : base( textWriter )
        {
        }

        /// <summary>
        /// Handles an Ensurance failure for the given constraint. Implementors
        /// should always call
        /// <code>
        /// if( successor != null)
        /// {
        ///     successor.Handle( constraint, message, args );
        /// }
        /// </code>
        /// So that the downstream handler can have a chance to process the failure.
        /// </summary>
        /// <param name="constraint">The constraint.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The args.</param>
        public override void Handle( Constraint constraint, string message, params object[] args )
        {
            TextWriter.Write( ToString() );

            IEnsuranceResponsibilityChainLink handler = Successor;
            if ( handler != null )
            {
                handler.Handle( constraint, message, args );
            }
        }

        /// <summary>
        /// Gets the preamble message.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            int currentFrame = 2;
            StringBuilder preamble = new StringBuilder();
            StackTrace stackTrace = new StackTrace();
            string typeName;
            do
            {
                // Move up the stack trace frame by frame
                currentFrame++;
                StackFrame stackFrame = stackTrace.GetFrame( currentFrame );
                typeName = stackFrame.GetMethod().ReflectedType.FullName;
                // Once we have found a method that is not within the calling type we break;
            } while ( typeName.Contains( "Ensurance." ) ||
                      typeName.Contains( "System." ) ||
                      typeName.Contains( "Microsoft." ) );

            // get the last Ensure call
            CreatePreambleStringForMethod( stackTrace.GetFrame( currentFrame - 1 ), preamble );

            // Process the rest of the stack excluding Microsoft Classes.
            for (int i = currentFrame; i < stackTrace.FrameCount; i++)
            {
                CreatePreambleStringForMethod( stackTrace.GetFrame( i ), preamble );
            }

            return preamble.ToString();
        }

        /// <summary>
        /// Creates the preamble string for method.
        /// </summary>
        /// <param name="currentFrame">The current frame.</param>
        /// <param name="preamble">The preamble.</param>
        private static void CreatePreambleStringForMethod( StackFrame currentFrame, StringBuilder preamble )
        {
            MethodBase stackFrameMethod = currentFrame.GetMethod();
            string typeName = stackFrameMethod.ReflectedType.FullName;

            if ( typeName.Contains( "System." ) || typeName.Contains( "Microsoft." ) )
            {
                return;
            }

            typeName = typeName.Replace( "EnsureBase`1", "Ensure" );

            // log Namespace, Class and Method Name
            preamble.AppendFormat( "at\t{0}.{1}( ", typeName, stackFrameMethod.Name );

            // log parameter types and names
            ParameterInfo[] parameters = stackFrameMethod.GetParameters();
            int parameterIndex = 0;

            while ( parameterIndex < parameters.Length )
            {
                ParameterInfo currentParameterInfo = parameters[parameterIndex];
                preamble.AppendFormat( "{0} {1}{2}",
                                       currentParameterInfo.ParameterType.Name,
                                       currentParameterInfo.Name,
                                       ( ++parameterIndex != parameters.Length ) ? ", " : string.Empty );
            }

            preamble.AppendFormat( " ){0}", Environment.NewLine );
        }
    }
}