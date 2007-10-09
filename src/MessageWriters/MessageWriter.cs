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
using System.Globalization;
using System.IO;
using System.Text;
using Ensurance.Constraints;

namespace Ensurance.MessageWriters
{
    /// <summary>
    /// MessageWriter is the abstract base for classes that write constraint
    /// descriptions and messages in some form. The class has separate methods
    /// for writing various components of a message, allowing implementations to
    /// tailor the presentation as needed.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerNonUserCode]
#endif

    public abstract class MessageWriter : IEnsuranceResponsibilityChainLink, IDisposable
    {
        private IEnsuranceResponsibilityChainLink _successor;
        private TextWriter _textWriter;
        private bool _disposed;

        /// <summary>
        /// Construct a MessageWriter given a culture
        /// </summary>
        protected MessageWriter()
        {
            _textWriter = new StringWriter( CultureInfo.InvariantCulture );
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageWriter"/> class.
        /// </summary>
        /// <param name="textWriter">The target.</param>
        protected MessageWriter( TextWriter textWriter )
        {
            _textWriter = textWriter;
        }

        /// <summary>
        /// Abstract method to get the max line length
        /// </summary>
        public abstract int MaxLineLength { get; }

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

        /// <summary>
        /// Handles an Ensurance failure for the given constraint. Implementors
        /// should always call
        /// <code>
        /// if( successor != null) {
        ///     successor.Handle( constraint, message, args );
        /// }
        /// </code>
        /// So that the downstream handler can have a chance to process the
        /// failure.
        /// </summary>
        /// <param name="constraint">The constraint.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The args.</param>
        public abstract void Handle( Constraint constraint, string message, params object[] args );

        #endregion

        /// <summary>
        /// Method to write single line  message with optional args, usually
        /// written to precede the general failure message.
        /// </summary>
        /// <param name="message">The message to be written</param>
        /// <param name="args">Any arguments used in formatting the message</param>
        public void WriteMessageLine( string message, params object[] args )
        {
            WriteMessageLine( 0, message, args );
        }

        /// <summary>
        /// Method to write single line  message with optional args, usually
        /// written to precede the general failure message, at a givel 
        /// indentation level.
        /// </summary>
        /// <param name="level">The indentation level of the message</param>
        /// <param name="message">The message to be written</param>
        /// <param name="args">Any arguments used in formatting the message</param>
        public abstract void WriteMessageLine( int level, string message, params object[] args );

        /// <summary>
        /// Display Expected and Actual lines for a constraint. This is called
        /// by MessageWriter's default implementation of WriteMessageTo and
        /// provides the generic two-line display. 
        /// </summary>
        /// <param name="constraint">The constraint that failed</param>
        public abstract void DisplayDifferences( Constraint constraint );

        /// <summary>
        /// Display Expected and Actual lines for given values. This method may
        /// be called by constraints that need more control over the display of
        /// actual and expected values than is provided by the default
        /// implementation.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value causing the failure</param>
        public abstract void DisplayDifferences( object expected, object actual );

        /// <summary>
        /// Display Expected and Actual lines for given values, including a
        /// tolerance value on the Expected line.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value causing the failure</param>
        /// <param name="tolerance">The tolerance within which the test was made</param>
        public abstract void DisplayDifferences( object expected, object actual, object tolerance );

        /// <summary>
        /// Display the expected and actual string values on separate lines. If
        /// the mismatch parameter is >=0, an additional line is displayed line
        /// containing a caret that points to the mismatch point.
        /// </summary>
        /// <param name="expected">The expected string value</param>
        /// <param name="actual">The actual string value</param>
        /// <param name="mismatch">The point at which the strings don't match or -1</param>
        /// <param name="ignoreCase">If true, case is ignored in locating the point where the strings differ</param>
        public abstract void DisplayStringDifferences( string expected, string actual, int mismatch, bool ignoreCase );

        /// <summary>
        /// Writes the text for a connector.
        /// </summary>
        /// <param name="connector">The connector.</param>
        public abstract void WriteConnector( string connector );

        /// <summary>
        /// Writes the text for a predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        public abstract void WritePredicate( string predicate );

        /// <summary>
        /// Writes the text for an expected value.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        public abstract void WriteExpectedValue( object expected );

        /// <summary>
        /// Writes the text for a modifier
        /// </summary>
        /// <param name="modifier">The modifier.</param>
        public abstract void WriteModifier( string modifier );

        /// <summary>
        /// Writes the text for an actual value.
        /// </summary>
        /// <param name="actual">The actual value.</param>
        public abstract void WriteActualValue( object actual );

        /// <summary>
        /// Writes the text for a generalized value.
        /// </summary>
        /// <param name="val">The value.</param>
        public abstract void WriteValue( object val );

        /// <summary>
        /// Writes the text for a collection value, starting at a particular
        /// point, to a max length
        /// </summary>
        /// <param name="collection">The collection containing elements to write.</param>
        /// <param name="start">The starting point of the elements to write</param>
        /// <param name="max">The maximum number of elements to write</param>
        public abstract void WriteCollectionElements( ICollection collection, int start, int max );

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the
        /// current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current
        /// <see cref="T:System.Object"></see>.
        /// </returns>
        public override string ToString()
        {
            return _textWriter.ToString();
        }

        #region TextWriter Interface

        /// <summary>
        /// Gets the format provider.
        /// </summary>
        /// <value>The format provider.</value>
        public IFormatProvider FormatProvider
        {
            get { return _textWriter.FormatProvider; }
        }

        /// <summary>
        /// Gets the encoding.
        /// </summary>
        /// <value>The encoding.</value>
        public Encoding Encoding
        {
            get { return _textWriter.Encoding; }
        }

        /// <summary>
        /// Gets or sets the new line.
        /// </summary>
        /// <value>The new line.</value>
        public string NewLine
        {
            get { return _textWriter.NewLine; }
            set { _textWriter.NewLine = value; }
        }

        /// <summary>
        /// Gets the null.
        /// </summary>
        /// <value>The null.</value>
        public TextWriter Null
        {
            get { return TextWriter.Null; }
        }

        /// <summary>
        /// Gets or sets the text writer.
        /// </summary>
        /// <value>The text writer.</value>
        protected internal TextWriter TextWriter
        {
            get { return _textWriter; }
            set { _textWriter = value; }
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public void Close()
        {
            _textWriter.Close();
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        protected virtual void Dispose( bool disposing )
        {
            if ( !_disposed && disposing )
            {
                _textWriter.Dispose();
                _textWriter = null;
            }
            _disposed = true;
        }

        /// <summary>
        /// Flushes this instance.
        /// </summary>
        public void Flush()
        {
            _textWriter.Flush();
        }

        public virtual TextWriter Synchronized( TextWriter writer )
        {
            return TextWriter.Synchronized( writer );
        }

        public virtual void Write( char value )
        {
            _textWriter.Write( value );
        }

        public virtual void Write( Char[] buffer )
        {
            _textWriter.Write( buffer );
        }

        public virtual void Write( Char[] buffer, int index, int count )
        {
            _textWriter.Write( buffer, index, count );
        }

        public virtual void Write( bool value )
        {
            _textWriter.Write( value );
        }

        public virtual void Write( int value )
        {
            _textWriter.Write( value );
        }

        public virtual void Write( uint value )
        {
            _textWriter.Write( value );
        }

        public virtual void Write( long value )
        {
            _textWriter.Write( value );
        }

        public virtual void Write( ulong value )
        {
            _textWriter.Write( value );
        }

        public virtual void Write( float value )
        {
            _textWriter.Write( value );
        }

        public virtual void Write( double value )
        {
            _textWriter.Write( value );
        }

        public virtual void Write( decimal value )
        {
            _textWriter.Write( value );
        }

        public virtual void Write( string value )
        {
            _textWriter.Write( value );
        }

        public virtual void Write( object value )
        {
            _textWriter.Write( value );
        }

        public virtual void Write( string format, object arg0 )
        {
            _textWriter.Write( format, arg0 );
        }

        public virtual void Write( string format, object arg0, object arg1 )
        {
            _textWriter.Write( format, arg0, arg1 );
        }

        public virtual void Write( string format, object arg0, object arg1, object arg2 )
        {
            _textWriter.Write( format, arg0, arg1, arg2 );
        }

        public virtual void Write( string format, Object[] arg )
        {
            _textWriter.Write( format, arg );
        }

        public void WriteLine()
        {
            _textWriter.WriteLine();
        }

        public virtual void WriteLine( char value )
        {
            _textWriter.WriteLine( value );
        }

        public virtual void WriteLine( Char[] buffer )
        {
            _textWriter.WriteLine( buffer );
        }

        public virtual void WriteLine( Char[] buffer, int index, int count )
        {
            _textWriter.WriteLine( buffer, index, count );
        }

        public virtual void WriteLine( bool value )
        {
            _textWriter.WriteLine( value );
        }

        public virtual void WriteLine( int value )
        {
            _textWriter.WriteLine( value );
        }

        public virtual void WriteLine( uint value )
        {
            _textWriter.WriteLine( value );
        }

        public virtual void WriteLine( long value )
        {
            _textWriter.WriteLine( value );
        }

        public virtual void WriteLine( ulong value )
        {
            _textWriter.WriteLine( value );
        }

        public virtual void WriteLine( float value )
        {
            _textWriter.WriteLine( value );
        }

        public virtual void WriteLine( double value )
        {
            _textWriter.WriteLine( value );
        }

        public virtual void WriteLine( decimal value )
        {
            _textWriter.WriteLine( value );
        }

        public virtual void WriteLine( string value )
        {
            _textWriter.WriteLine( value );
        }

        public virtual void WriteLine( object value )
        {
            _textWriter.WriteLine( value );
        }

        public virtual void WriteLine( string format, object arg0 )
        {
            _textWriter.WriteLine( format, arg0 );
        }

        public virtual void WriteLine( string format, object arg0, object arg1 )
        {
            _textWriter.WriteLine( format, arg0, arg1 );
        }

        public virtual void WriteLine( string format, object arg0, object arg1, object arg2 )
        {
            _textWriter.WriteLine( format, arg0, arg1, arg2 );
        }

        public virtual void WriteLine( string format, Object[] arg )
        {
            _textWriter.WriteLine( format, arg );
        }

        #endregion
    }
}