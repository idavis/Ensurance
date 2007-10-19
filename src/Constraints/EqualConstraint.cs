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
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Ensurance.MessageWriters;
using Ensurance.Properties;

namespace Ensurance.Constraints
{
    /// <summary>
    /// EqualConstraint is able to compare an actual value with the expected
    /// value provided in its constructor.
    /// </summary>
    public class EqualConstraint : Constraint
    {
        private const int BUFFER_SIZE = 4096;

        private object _expected;

        private List<long> _failurePoints;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="EqualConstraint"/>
        /// class.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        public EqualConstraint( object expected )
        {
            _expected = expected;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Test whether the constraint is satisfied by a given value
        /// </summary>
        /// <param name="actual">The value to be tested</param>
        /// <returns>True for success, false for failure</returns>
        public override bool Matches( object actual )
        {
            Actual = actual;
            _failurePoints = new List<long>();

            return ObjectsEqual( _expected, actual );
        }

        /// <summary>
        /// Write a failure message. Overridden to provide custom failure
        /// messages for EqualConstraint.
        /// </summary>
        /// <param name="writer">The MessageWriter to write to</param>
        public override void WriteMessageTo( MessageWriter writer )
        {
            DisplayDifferences( writer, _expected, Actual, 0 );
        }


        /// <summary>
        /// Write description of this constraint
        /// </summary>
        /// <param name="writer">The MessageWriter to write to</param>
        /// <exception cref="ArgumentNullException">if the message writer is null.</exception>
        public override void WriteDescriptionTo( MessageWriter writer )
        {
            if ( writer == null )
            {
                throw new ArgumentNullException( "writer" );
            }
            writer.WriteExpectedValue( _expected );

            if ( Tolerance != null )
            {
                writer.WriteConnector( "+/-" );
                writer.WriteExpectedValue( Tolerance );
            }

            if ( CaseInsensitive )
            {
                writer.WriteModifier( Resources.IgnoringCase );
            }
        }

        /// <summary>
        /// Displays the differences.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="expected">The expected.</param>
        /// <param name="actual">The actual.</param>
        /// <param name="depth">The depth.</param>
        private void DisplayDifferences( MessageWriter writer, object expected, object actual, int depth )
        {
            if ( expected is string && actual is string )
            {
                DisplayStringDifferences( writer, (string) expected, (string) actual );
            }
            else if ( expected is ICollection && actual is ICollection )
            {
                DisplayCollectionDifferences( writer, expected as ICollection, actual as ICollection, depth );
            }
            else if ( expected is Stream && actual is Stream )
            {
                DisplayStreamDifferences( writer, expected as Stream, actual as Stream, depth );
            }
            else if ( Tolerance != null )
            {
                writer.DisplayDifferences( expected, actual, Tolerance );
            }
            else
            {
                writer.DisplayDifferences( expected, actual );
            }
        }

        #endregion

        #region ObjectsEqual

        /// <summary>
        /// Determines whether two objects are equal.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The actual value.</param>
        /// <returns></returns>
        private bool ObjectsEqual( object expected, object actual )
        {
            if ( expected == null && actual == null )
            {
                return true;
            }

            if ( expected == null || actual == null )
            {
                return false;
            }

            Type expectedType = expected.GetType();
            Type actualType = actual.GetType();

            if ( expectedType.IsArray && actualType.IsArray && !CompareAsCollection )
            {
                return ArraysEqual( (Array) expected, (Array) actual );
            }

            if ( expected is ICollection && actual is ICollection )
            {
                return CollectionsEqual( (ICollection) expected, (ICollection) actual );
            }

            if ( expected is Stream && actual is Stream )
            {
                return StreamsEqual( (Stream) expected, (Stream) actual );
            }

            if ( CompareWith != null )
            {
                return CompareWith.Compare( expected, actual ) == 0;
            }

            if ( Numerics.IsNumericType( expected ) && Numerics.IsNumericType( actual ) )
            {
                return Numerics.AreEqual( expected, actual, Tolerance );
            }

            if ( expected is string && actual is string )
            {
                return string.Compare( (string) expected, (string) actual, CaseInsensitive, CultureInfo.CurrentCulture ) == 0;
            }

            return expected.Equals( actual );
        }

        /// <summary>
        /// Helper method to compare two arrays
        /// </summary>
        protected virtual bool ArraysEqual( Array expected, Array actual )
        {
            int rank = expected.Rank;

            if ( rank != actual.Rank )
            {
                return false;
            }

            for (int r = 1; r < rank; r++)
            {
                if ( expected.GetLength( r ) != actual.GetLength( r ) )
                {
                    return false;
                }
            }

            return CollectionsEqual( expected, actual );
        }

        /// <summary>
        /// Helper method to compare two collections
        /// </summary>
        private bool CollectionsEqual( ICollection expected, ICollection actual )
        {
            IEnumerator expectedEnum = expected.GetEnumerator();
            IEnumerator actualEnum = actual.GetEnumerator();

            int count;
            for (count = 0; expectedEnum.MoveNext() && actualEnum.MoveNext(); count++)
            {
                if ( !ObjectsEqual( expectedEnum.Current, actualEnum.Current ) )
                {
                    break;
                }
            }

            if ( count == expected.Count && count == actual.Count )
            {
                return true;
            }

            _failurePoints.Insert( 0, count );
            return false;
        }

        /// <summary>
        /// Helper method to compare two streams
        /// </summary>
        private bool StreamsEqual( Stream expected, Stream actual )
        {
            if ( expected.Length != actual.Length )
            {
                return false;
            }

            byte[] bufferExpected = new byte[BUFFER_SIZE];
            byte[] bufferActual = new byte[BUFFER_SIZE];

            BinaryReader binaryReaderExpected = new BinaryReader( expected );
            BinaryReader binaryReaderActual = new BinaryReader( actual );

            binaryReaderExpected.BaseStream.Seek( 0, SeekOrigin.Begin );
            binaryReaderActual.BaseStream.Seek( 0, SeekOrigin.Begin );

            for (long readByte = 0; readByte < expected.Length; readByte += BUFFER_SIZE)
            {
                binaryReaderExpected.Read( bufferExpected, 0, BUFFER_SIZE );
                binaryReaderActual.Read( bufferActual, 0, BUFFER_SIZE );

                for (int count = 0; count < BUFFER_SIZE; ++count)
                {
                    if ( bufferExpected[count] != bufferActual[count] )
                    {
                        _failurePoints.Insert( 0, readByte + count );
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion

        #region DisplayStringDifferences

        /// <summary>
        /// Displays the string differences.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="expected">The expected.</param>
        /// <param name="actual">The actual.</param>
        private void DisplayStringDifferences( MessageWriter writer, string expected, string actual )
        {
            int mismatch = MsgUtils.FindMismatchPosition( expected, actual, 0, CaseInsensitive );

            if ( expected.Length == actual.Length )
            {
                writer.WriteMessageLine( Resources.StringsDiffer_1, expected.Length, mismatch );
            }
            else
            {
                writer.WriteMessageLine( Resources.StringsDiffer_2, expected.Length, actual.Length, mismatch );
            }

            writer.DisplayStringDifferences( expected, actual, mismatch, CaseInsensitive );
        }

        #endregion

        #region DisplayStreamDifferences

        /// <summary>
        /// Displays the stream differences.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="expected">The expected.</param>
        /// <param name="actual">The actual.</param>
        /// <param name="depth">The depth.</param>
        private void DisplayStreamDifferences( MessageWriter writer, Stream expected, Stream actual, int depth )
        {
            if ( expected.Length == actual.Length )
            {
                long offset = _failurePoints[depth];
                writer.WriteMessageLine( Resources.StreamsDiffer_1, expected.Length, offset );
            }
            else
            {
                writer.WriteMessageLine( Resources.StreamsDiffer_2, expected.Length, actual.Length );
            }
        }

        #endregion

        #region DisplayCollectionDifferences

        /// <summary>
        /// Display the failure information for two collections that did not
        /// match.
        /// </summary>
        /// <param name="writer">The MessageWriter on which to display</param>
        /// <param name="expected">The expected collection.</param>
        /// <param name="actual">The actual collection</param>
        /// <param name="depth">The depth of this failure in a set of nested collections</param>
        private void DisplayCollectionDifferences( MessageWriter writer, ICollection expected, ICollection actual, int depth )
        {
            int failurePoint = _failurePoints.Count > depth ? (int) _failurePoints[depth] : -1;

            DisplayCollectionTypesAndSizes( writer, expected, actual, depth );

            if ( failurePoint >= 0 )
            {
                DisplayFailurePoint( writer, expected, actual, failurePoint, depth );
                if ( failurePoint < expected.Count && failurePoint < actual.Count )
                {
                    DisplayDifferences(
                        writer,
                        GetValueFromCollection( expected, failurePoint ),
                        GetValueFromCollection( actual, failurePoint ),
                        ++depth );
                }
                else if ( expected.Count < actual.Count )
                {
                    writer.Write( string.Format( "  {0}:    ", Resources.Extra ) );
                    writer.WriteCollectionElements( actual, failurePoint, 3 );
                }
                else
                {
                    writer.Write(string.Format( "  {0}:  ", Resources.Missing ));
                    writer.WriteCollectionElements( expected, failurePoint, 3 );
                }
            }
        }

        /// <summary>
        /// Displays a single line showing the types and sizes of the expected
        /// and actual collections or arrays. If both are identical, the value
        /// is only shown once.
        /// </summary>
        /// <param name="writer">The MessageWriter on which to display</param>
        /// <param name="expected">The expected collection or array</param>
        /// <param name="actual">The actual collection or array</param>
        /// <param name="indent">The indentation level for the message line</param>
        private static void DisplayCollectionTypesAndSizes( MessageWriter writer, ICollection expected, ICollection actual, int indent )
        {
            string sExpected = MsgUtils.GetTypeRepresentation( expected );
            if ( !( expected is Array ) )
            {
                sExpected += string.Format( CultureInfo.CurrentCulture, Resources.WithElements_1, expected.Count );
            }

            string sActual = MsgUtils.GetTypeRepresentation( actual );
            if ( !( actual is Array ) )
            {
                sActual += string.Format( CultureInfo.CurrentCulture, Resources.WithElements_1, expected.Count );
            }

            if ( sExpected == sActual )
            {
                writer.WriteMessageLine( indent, Resources.CollectionType_1, sExpected );
            }
            else
            {
                writer.WriteMessageLine( indent, Resources.CollectionType_2, sExpected, sActual );
            }
        }

        /// <summary>
        /// Displays a single line showing the point in the expected and actual
        /// arrays at which the comparison failed. If the arrays have different
        /// structures or dimensions, both values are shown.
        /// </summary>
        /// <param name="writer">The MessageWriter on which to display</param>
        /// <param name="expected">The expected array</param>
        /// <param name="actual">The actual array</param>
        /// <param name="failurePoint">Index of the failure point in the underlying collections</param>
        /// <param name="indent">The indentation level for the message line</param>
        private static void DisplayFailurePoint( MessageWriter writer, ICollection expected, ICollection actual, int failurePoint, int indent )
        {
            Array expectedArray = expected as Array;
            Array actualArray = actual as Array;

            int expectedRank = expectedArray != null ? expectedArray.Rank : 1;
            int actualRank = actualArray != null ? actualArray.Rank : 1;

            bool useOneIndex = expectedRank == actualRank;

            if ( expectedArray != null && actualArray != null )
            {
                for (int r = 1; r < expectedRank && useOneIndex; r++)
                {
                    if ( expectedArray.GetLength( r ) != actualArray.GetLength( r ) )
                    {
                        useOneIndex = false;
                    }
                }
            }

            int[] expectedIndices = MsgUtils.GetArrayIndicesFromCollectionIndex( expected, failurePoint );
            if ( useOneIndex )
            {
                writer.WriteMessageLine( indent, Resources.ValuesDiffer_1, MsgUtils.GetArrayIndicesAsString( expectedIndices ) );
            }
            else
            {
                int[] actualIndices = MsgUtils.GetArrayIndicesFromCollectionIndex( actual, failurePoint );
                writer.WriteMessageLine( indent,
                                         Resources.ValuesDiffer_2,
                                         MsgUtils.GetArrayIndicesAsString( expectedIndices ),
                                         MsgUtils.GetArrayIndicesAsString( actualIndices ) );
            }
        }

        /// <summary>
        /// Gets the value from collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        private static object GetValueFromCollection( ICollection collection, int index )
        {
            Array array = collection as Array;

            if ( array != null && array.Rank > 1 )
            {
                return array.GetValue( MsgUtils.GetArrayIndicesFromCollectionIndex( array, index ) );
            }

            IList collectionAsIList = collection as IList;
            if ( collectionAsIList != null )
            {
                return collectionAsIList[index];
            }

            foreach (object obj in collection)
            {
                if ( --index < 0 )
                {
                    return obj;
                }
            }

            return null;
        }

        #endregion
    }
}