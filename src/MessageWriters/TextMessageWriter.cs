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
using Ensurance.Constraints;

namespace Ensurance.MessageWriters
{
    /// <summary>
    /// TextMessageWriter writes constraint descriptions and messages in
    /// displayable form as a text stream. It tailors the display of individual
    /// message components to form the standard message format of NUnit
    /// assertion failure messages.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerNonUserCode]
#endif

    public class TextMessageWriter : MessageWriter
    {
        #region Message Formats and Constants

        private const string Fmt_Char = "'{0}'";
        private const string Fmt_Connector = " {0} ";
        private const string Fmt_DateTime = "yyyy-MM-dd HH:mm:ss.fff";
        private const string Fmt_Default = "<{0}>";
        private const string Fmt_EmptyCollection = "<empty>";
        private const string Fmt_EmptyString = "<string.Empty>";
        //private const string Fmt_Label = "{0}";
        private const string Fmt_Modifier = ", {0}";

        private const string Fmt_Null = "null";
        private const string Fmt_Predicate = "{0} ";

        private const string Fmt_String = "\"{0}\"";
        private const string Fmt_ValueType = "{0}";
        private const int MAX_LINE_LENGTH = 78;

        /// <summary>
        /// Prefix used for the actual value line of a message
        /// </summary>
        public const string Pfx_Actual = "  But was:  ";

        /// <summary>
        /// Prefix used for the expected value line of a message
        /// </summary>
        public const string Pfx_Expected = "  Expected: ";

        /// <summary>
        /// Length of a message prefix
        /// </summary>
        public static readonly int PrefixLength = Pfx_Expected.Length;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="TextMessageWriter"/>
        /// class.
        /// </summary>
        public TextMessageWriter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextMessageWriter"/>
        /// class.
        /// </summary>
        /// <param name="textWriter">The textWriter.</param>
        public TextMessageWriter( TextWriter textWriter ) : base( textWriter )
        {
        }

        #region Properties

        /// <summary>
        /// Gets the maximum line length for this writer
        /// </summary>
        public override int MaxLineLength
        {
            get { return MAX_LINE_LENGTH; }
        }

        #endregion

        #region Public Methods - High Level

        /// <summary>
        /// Method to write single line  message with optional args, usually
        /// written to precede the general failure message, at a givel 
        /// indentation level.
        /// </summary>
        /// <param name="level">The indentation level of the message</param>
        /// <param name="message">The message to be written</param>
        /// <param name="args">Any arguments used in formatting the message</param>
        public override void WriteMessageLine( int level, string message, params object[] args )
        {
            if ( message != null )
            {
                while ( level-- >= 0 )
                {
                    TextWriter.Write( "  " );
                }

                if ( args != null && args.Length > 0 )
                {
                    message = string.Format( CultureInfo.CurrentCulture, message, args );
                }

                TextWriter.WriteLine( message );
            }
        }

        /// <summary>
        /// Display Expected and Actual lines for a constraint. This is called
        /// by MessageWriter's default implementation of WriteMessageTo and
        /// provides the generic two-line display. 
        /// </summary>
        /// <param name="constraint">The constraint that failed</param>
        public override void DisplayDifferences( Constraint constraint )
        {
            WriteExpectedLine( constraint );
            WriteActualLine( constraint );
        }

        /// <summary>
        /// Display Expected and Actual lines for given values. This method may
        /// be called by constraints that need more control over the display of
        /// actual and expected values than is provided by the default
        /// implementation.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value causing the failure</param>
        public override void DisplayDifferences( object expected, object actual )
        {
            WriteExpectedLine( expected );
            WriteActualLine( actual );
        }

        /// <summary>
        /// Display Expected and Actual lines for given values, including a
        /// tolerance value on the expected line.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value causing the failure</param>
        /// <param name="tolerance">The tolerance within which the test was made</param>
        public override void DisplayDifferences( object expected, object actual, object tolerance )
        {
            WriteExpectedLine( expected, tolerance );
            WriteActualLine( actual );
        }

        /// <summary>
        /// Display the expected and actual string values on separate lines. If
        /// the mismatch parameter is >=0, an additional line is displayed line
        /// containing a caret that points to the mismatch point.
        /// </summary>
        /// <param name="expected">The expected string value</param>
        /// <param name="actual">The actual string value</param>
        /// <param name="mismatch">The point at which the strings don't match or -1</param>
        /// <param name="ignoreCase">If true, case is ignored in string comparisons</param>
        public override void DisplayStringDifferences( string expected, string actual, int mismatch, bool ignoreCase )
        {
            // Maximum string we can display without truncating
            int maxStringLength = MAX_LINE_LENGTH
                                  - PrefixLength // Allow for prefix
                                  - 2; // 2 quotation marks

            expected = MsgUtils.ConvertWhitespace( MsgUtils.ClipString( expected, maxStringLength, mismatch ) );
            actual = MsgUtils.ConvertWhitespace( MsgUtils.ClipString( actual, maxStringLength, mismatch ) );

            // The mismatch position may have changed due to clipping or white
            // space conversion
            mismatch = MsgUtils.FindMismatchPosition( expected, actual, 0, ignoreCase );

            TextWriter.Write( Pfx_Expected );
            WriteExpectedValue( expected );
            if ( ignoreCase )
            {
                WriteModifier( "ignoring case" );
            }
            TextWriter.WriteLine();
            WriteActualLine( actual );
            //DisplayDifferences(expected, actual);
            if ( mismatch >= 0 )
            {
                WriteCaretLine( mismatch );
            }
        }

        #endregion

        #region Public Methods - Low Level

        /// <summary>
        /// Writes the text for a connector.
        /// </summary>
        /// <param name="connector">The connector.</param>
        public override void WriteConnector( string connector )
        {
            TextWriter.Write( Fmt_Connector, connector );
        }

        /// <summary>
        /// Writes the text for a predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        public override void WritePredicate( string predicate )
        {
            TextWriter.Write( Fmt_Predicate, predicate );
        }

        //public override void WriteLabel(string label)
        //{
        //    Write(Fmt_Label, label);
        //}

        /// <summary>
        /// Write the text for a modifier.
        /// </summary>
        /// <param name="modifier">The modifier.</param>
        public override void WriteModifier( string modifier )
        {
            TextWriter.Write( Fmt_Modifier, modifier );
        }


        /// <summary>
        /// Writes the text for an expected value.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        public override void WriteExpectedValue( object expected )
        {
            WriteValue( expected );
        }

        /// <summary>
        /// Writes the text for an actual value.
        /// </summary>
        /// <param name="actual">The actual value.</param>
        public override void WriteActualValue( object actual )
        {
            WriteValue( actual );
        }

        /// <summary>
        /// Writes the text for a generalized value.
        /// </summary>
        /// <param name="val">The value.</param>
        public override void WriteValue( object val )
        {
            if ( val == null )
            {
                TextWriter.Write( Fmt_Null );
            }
            else if ( val.GetType().IsArray )
            {
                WriteArray( (Array) val );
            }
            else if ( val is ICollection )
            {
                WriteCollectionElements( (ICollection) val, 0, 10 );
            }
            else if ( val is string )
            {
                WriteString( (string) val );
            }
            else if ( val is char )
            {
                WriteChar( (char) val );
            }
            else if ( val is double )
            {
                WriteDouble( (double) val );
            }
            else if ( val is float )
            {
                WriteFloat( (float) val );
            }
            else if ( val is decimal )
            {
                WriteDecimal( (decimal) val );
            }
            else if ( val is DateTime )
            {
                WriteDateTime( (DateTime) val );
            }
            else if ( val.GetType().IsValueType )
            {
                TextWriter.Write( Fmt_ValueType, val );
            }
            else
            {
                TextWriter.Write( Fmt_Default, val );
            }
        }

        /// <summary>
        /// Writes the text for a collection value, starting at a particular
        /// point, to a max length
        /// </summary>
        /// <param name="collection">The collection containing elements to write.</param>
        /// <param name="start">The starting point of the elements to write</param>
        /// <param name="max">The maximum number of elements to write</param>
        public override void WriteCollectionElements( ICollection collection, int start, int max )
        {
            if ( collection.Count == 0 )
            {
                TextWriter.Write( Fmt_EmptyCollection );
                return;
            }

            int count = 0;
            int index = 0;
            TextWriter.Write( "< " );

            foreach (object obj in collection)
            {
                if ( index++ >= start )
                {
                    if ( count > 0 )
                    {
                        TextWriter.Write( ", " );
                    }
                    WriteValue( obj );
                    if ( ++count >= max )
                    {
                        break;
                    }
                }
            }

            if ( index < collection.Count )
            {
                TextWriter.Write( "..." );
            }

            TextWriter.Write( " >" );
        }

        /// <summary>
        /// Writes the array.
        /// </summary>
        /// <param name="array">The array.</param>
        private void WriteArray( Array array )
        {
            if ( array.Length == 0 )
            {
                TextWriter.Write( Fmt_EmptyCollection );
                return;
            }

            int rank = array.Rank;
            int[] products = new int[rank];

            for (int product = 1, r = rank; --r >= 0;)
            {
                products[r] = product *= array.GetLength( r );
            }

            int count = 0;
            foreach (object obj in array)
            {
                if ( count > 0 )
                {
                    TextWriter.Write( ", " );
                }

                bool startSegment = false;
                for (int r = 0; r < rank; r++)
                {
                    startSegment = startSegment || count % products[r] == 0;
                    if ( startSegment )
                    {
                        TextWriter.Write( "< " );
                    }
                }

                WriteValue( obj );

                ++count;

                bool nextSegment = false;
                for (int r = 0; r < rank; r++)
                {
                    nextSegment = nextSegment || count % products[r] == 0;
                    if ( nextSegment )
                    {
                        TextWriter.Write( " >" );
                    }
                }
            }
        }

        /// <summary>
        /// Writes the string.
        /// </summary>
        /// <param name="s">The s.</param>
        private void WriteString( string s )
        {
            if ( string.IsNullOrEmpty( s ) )
            {
                TextWriter.Write( Fmt_EmptyString );
            }
            else
            {
                TextWriter.Write( Fmt_String, s );
            }
        }

        /// <summary>
        /// Writes the char.
        /// </summary>
        /// <param name="c">The c.</param>
        private void WriteChar( char c )
        {
            TextWriter.Write( Fmt_Char, c );
        }

        /// <summary>
        /// Writes the double.
        /// </summary>
        /// <param name="d">The d.</param>
        private void WriteDouble( double d )
        {
            if ( double.IsNaN( d ) || double.IsInfinity( d ) )
            {
                TextWriter.Write( d );
            }
            else
            {
                string s = d.ToString( "G17", CultureInfo.InvariantCulture );

                if ( s.IndexOf( '.' ) > 0 )
                {
                    TextWriter.Write( s + "d" );
                }
                else
                {
                    TextWriter.Write( s + ".0d" );
                }
            }
        }

        /// <summary>
        /// Writes the float.
        /// </summary>
        /// <param name="f">The f.</param>
        private void WriteFloat( float f )
        {
            if ( float.IsNaN( f ) || float.IsInfinity( f ) )
            {
                TextWriter.Write( f );
            }
            else
            {
                string s = f.ToString( "G9", CultureInfo.InvariantCulture );

                if ( s.IndexOf( '.' ) > 0 )
                {
                    TextWriter.Write( s + "f" );
                }
                else
                {
                    TextWriter.Write( s + ".0f" );
                }
            }
        }

        /// <summary>
        /// Writes the decimal.
        /// </summary>
        /// <param name="d">The d.</param>
        private void WriteDecimal( Decimal d )
        {
            TextWriter.Write( d.ToString( "G29", CultureInfo.InvariantCulture ) + "m" );
        }

        /// <summary>
        /// Writes the date time.
        /// </summary>
        /// <param name="dt">The dt.</param>
        private void WriteDateTime( DateTime dt )
        {
            TextWriter.Write( dt.ToString( Fmt_DateTime, CultureInfo.InvariantCulture ) );
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Write the generic 'Expected' line for a constraint
        /// </summary>
        /// <param name="constraint">The constraint that failed</param>
        private void WriteExpectedLine( Constraint constraint )
        {
            TextWriter.Write( Pfx_Expected );
            constraint.WriteDescriptionTo( this );
            TextWriter.WriteLine();
        }

        /// <summary>
        /// Write the generic 'Expected' line for a given value
        /// </summary>
        /// <param name="expected">The expected value</param>
        private void WriteExpectedLine( object expected )
        {
            TextWriter.Write( Pfx_Expected );
            WriteExpectedValue( expected );
            TextWriter.WriteLine();
        }

        /// <summary>
        /// Write the generic 'Expected' line for a given value and tolerance.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="tolerance">The tolerance within which the test was made</param>
        private void WriteExpectedLine( object expected, object tolerance )
        {
            TextWriter.Write( Pfx_Expected );
            WriteExpectedValue( expected );
            WriteConnector( "+/-" );
            WriteExpectedValue( tolerance );
            TextWriter.WriteLine();
        }

        /// <summary>
        /// Write the generic 'Actual' line for a constraint
        /// </summary>
        /// <param name="constraint">The constraint for which the actual value is to be written</param>
        private void WriteActualLine( Constraint constraint )
        {
            TextWriter.Write( Pfx_Actual );
            constraint.WriteActualValueTo( this );
            TextWriter.WriteLine();
        }

        /// <summary>
        /// Write the generic 'Actual' line for a given value
        /// </summary>
        /// <param name="actual">The actual value causing a failure</param>
        private void WriteActualLine( object actual )
        {
            TextWriter.Write( Pfx_Actual );
            WriteActualValue( actual );
            TextWriter.WriteLine();
        }

        private void WriteCaretLine( int mismatch )
        {
            // We subtract 2 for the initial 2 blanks and add back 1 for the
            // initial quote
            TextWriter.WriteLine( "  {0}^", new string( '-', PrefixLength + mismatch - 2 + 1 ) );
        }

        #endregion

        /// <summary>
        /// Handles an Ensurance failure for the given constraint. Implementors
        /// should always call
        /// <code>
        /// if( successor != null ) {
        ///    successor.Handle( constraint, message, args ); 
        /// }
        /// </code>
        /// So that the downstream handler can have a chance to process the
        /// failure.
        /// </summary>
        /// <param name="constraint">The constraint.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The args.</param>
        public override void Handle( Constraint constraint, string message, params object[] args )
        {
            try
            {
                if ( !string.IsNullOrEmpty( message ) )
                {
                    WriteMessageLine( message, args );
                }

                constraint.WriteMessageTo( this );
            }
            finally
            {
                IEnsuranceResponsibilityChainLink handler = Successor;
                if ( handler != null )
                {
                    handler.Handle( constraint, message, args );
                }
            }
        }
    }
}