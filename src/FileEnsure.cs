// ****************************************************************
// This is free software licensed under the NUnit license. You
// may obtain a copy of the license as well as information regarding
// copyright ownership at http://nunit.org/?p=license&r=2.4.
// ****************************************************************

using System.ComponentModel;
using System.IO;
using Ensurance.Constraints;

namespace Ensurance
{
    /// <summary>
    /// Summary description for FileEnsure.
    /// </summary>
    public class FileEnsure<T> where T : IEnsuranceHandler
    {
        #region Equals and ReferenceEquals

        /// <summary>
        /// The Equals method throws an EnsuranceException. This is done 
        /// to make sure there is no mistake by calling this function.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        [EditorBrowsable( EditorBrowsableState.Never )]
        public new static bool Equals( object a, object b )
        {
            throw new EnsuranceException( "EnsureBase<T>.Equals should not be used for Assertions" );
        }

        /// <summary>
        /// override the default ReferenceEquals to throw an EnsuranceException. This 
        /// implementation makes sure there is no mistake in calling this function 
        /// as part of EnsureBase<T>. 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public new static void ReferenceEquals( object a, object b )
        {
            throw new EnsuranceException( "EnsureBase<T>.ReferenceEquals should not be used for Assertions" );
        }

        #endregion

        #region Constructor

        /// <summary>
        /// We don't actually want any instances of this object, but some people
        /// like to inherit from it to add other static methods. Hence, the
        /// protected constructor disallows any instances of this object. 
        /// </summary>
        protected FileEnsure()
        {
        }

        #endregion

        #region AreEqual

        #region Streams

        /// <summary>
        /// Verifies that two Streams are equal.  Two Streams are considered
        /// equal if both are null, or if both have the same value byte for byte.
        /// If they are not equal an <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">The expected Stream</param>
        /// <param name="actual">The actual Stream</param>
        /// <param name="message">The message to display if Streams are not equal</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void AreEqual( Stream expected, Stream actual, string message, params object[] args )
        {
            EnsureBase<T>.That( actual, new EqualConstraint( expected ), message, args );
        }

        /// <summary>
        /// Verifies that two Streams are equal.  Two Streams are considered
        /// equal if both are null, or if both have the same value byte for byte.
        /// If they are not equal an <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">The expected Stream</param>
        /// <param name="actual">The actual Stream</param>
        /// <param name="message">The message to display if objects are not equal</param>
        public static void AreEqual( Stream expected, Stream actual, string message )
        {
            AreEqual( expected, actual, message, null );
        }

        /// <summary>
        /// Verifies that two Streams are equal.  Two Streams are considered
        /// equal if both are null, or if both have the same value byte for byte.
        /// If they are not equal an <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">The expected Stream</param>
        /// <param name="actual">The actual Stream</param>
        public static void AreEqual( Stream expected, Stream actual )
        {
            AreEqual( expected, actual, string.Empty, null );
        }

        #endregion

        #region FileInfo

        /// <summary>
        /// Verifies that two files are equal.  Two files are considered
        /// equal if both are null, or if both have the same value byte for byte.
        /// If they are not equal an <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">A file containing the value that is expected</param>
        /// <param name="actual">A file containing the actual value</param>
        /// <param name="message">The message to display if Streams are not equal</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void AreEqual( FileInfo expected, FileInfo actual, string message, params object[] args )
        {
            using (FileStream exStream = expected.OpenRead())
            {
                using (FileStream acStream = actual.OpenRead())
                {
                    AreEqual( exStream, acStream, message, args );
                }
            }
        }

        /// <summary>
        /// Verifies that two files are equal.  Two files are considered
        /// equal if both are null, or if both have the same value byte for byte.
        /// If they are not equal an <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">A file containing the value that is expected</param>
        /// <param name="actual">A file containing the actual value</param>
        /// <param name="message">The message to display if objects are not equal</param>
        public static void AreEqual( FileInfo expected, FileInfo actual, string message )
        {
            AreEqual( expected, actual, message, null );
        }

        /// <summary>
        /// Verifies that two files are equal.  Two files are considered
        /// equal if both are null, or if both have the same value byte for byte.
        /// If they are not equal an <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">A file containing the value that is expected</param>
        /// <param name="actual">A file containing the actual value</param>
        public static void AreEqual( FileInfo expected, FileInfo actual )
        {
            AreEqual( expected, actual, string.Empty, null );
        }

        #endregion

        #region String

        /// <summary>
        /// Verifies that two files are equal.  Two files are considered
        /// equal if both are null, or if both have the same value byte for byte.
        /// If they are not equal an <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">The path to a file containing the value that is expected</param>
        /// <param name="actual">The path to a file containing the actual value</param>
        /// <param name="message">The message to display if Streams are not equal</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void AreEqual( string expected, string actual, string message, params object[] args )
        {
            using (FileStream exStream = File.OpenRead( expected ))
            {
                using (FileStream acStream = File.OpenRead( actual ))
                {
                    AreEqual( exStream, acStream, message, args );
                }
            }
        }

        /// <summary>
        /// Verifies that two files are equal.  Two files are considered
        /// equal if both are null, or if both have the same value byte for byte.
        /// If they are not equal an <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">The path to a file containing the value that is expected</param>
        /// <param name="actual">The path to a file containing the actual value</param>
        /// <param name="message">The message to display if objects are not equal</param>
        public static void AreEqual( string expected, string actual, string message )
        {
            AreEqual( expected, actual, message, null );
        }

        /// <summary>
        /// Verifies that two files are equal.  Two files are considered
        /// equal if both are null, or if both have the same value byte for byte.
        /// If they are not equal an <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">The path to a file containing the value that is expected</param>
        /// <param name="actual">The path to a file containing the actual value</param>
        public static void AreEqual( string expected, string actual )
        {
            AreEqual( expected, actual, string.Empty, null );
        }

        #endregion

        #endregion

        #region AreNotEqual

        #region Streams

        /// <summary>
        /// Asserts that two Streams are not equal. If they are equal
        /// an <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">The expected Stream</param>
        /// <param name="actual">The actual Stream</param>
        /// <param name="message">The message to be displayed when the two Stream are the same.</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void AreNotEqual( Stream expected, Stream actual, string message, params object[] args )
        {
            EnsureBase<T>.That( actual, new NotConstraint( new EqualConstraint( expected ) ), message, args );
        }

        /// <summary>
        /// Asserts that two Streams are not equal. If they are equal
        /// an <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">The expected Stream</param>
        /// <param name="actual">The actual Stream</param>
        /// <param name="message">The message to be displayed when the Streams are the same.</param>
        public static void AreNotEqual( Stream expected, Stream actual, string message )
        {
            AreNotEqual( expected, actual, message, null );
        }

        /// <summary>
        /// Asserts that two Streams are not equal. If they are equal
        /// an <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">The expected Stream</param>
        /// <param name="actual">The actual Stream</param>
        public static void AreNotEqual( Stream expected, Stream actual )
        {
            AreNotEqual( expected, actual, string.Empty, null );
        }

        #endregion

        #region FileInfo

        /// <summary>
        /// Asserts that two files are not equal. If they are equal
        /// an <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">A file containing the value that is expected</param>
        /// <param name="actual">A file containing the actual value</param>
        /// <param name="message">The message to display if Streams are not equal</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void AreNotEqual( FileInfo expected, FileInfo actual, string message, params object[] args )
        {
            using (FileStream exStream = expected.OpenRead())
            {
                using (FileStream acStream = actual.OpenRead())
                {
                    AreNotEqual( exStream, acStream, message, args );
                }
            }
        }

        /// <summary>
        /// Asserts that two files are not equal. If they are equal
        /// an <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">A file containing the value that is expected</param>
        /// <param name="actual">A file containing the actual value</param>
        /// <param name="message">The message to display if objects are not equal</param>
        public static void AreNotEqual( FileInfo expected, FileInfo actual, string message )
        {
            AreNotEqual( expected, actual, message, null );
        }

        /// <summary>
        /// Asserts that two files are not equal. If they are equal
        /// an <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">A file containing the value that is expected</param>
        /// <param name="actual">A file containing the actual value</param>
        public static void AreNotEqual( FileInfo expected, FileInfo actual )
        {
            AreNotEqual( expected, actual, string.Empty, null );
        }

        #endregion

        #region String

        /// <summary>
        /// Asserts that two files are not equal. If they are equal
        /// an <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">The path to a file containing the value that is expected</param>
        /// <param name="actual">The path to a file containing the actual value</param>
        /// <param name="message">The message to display if Streams are not equal</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void AreNotEqual( string expected, string actual, string message, params object[] args )
        {
            using (FileStream exStream = File.OpenRead( expected ))
            {
                using (FileStream acStream = File.OpenRead( actual ))
                {
                    AreNotEqual( exStream, acStream, message, args );
                }
            }
        }

        /// <summary>
        /// Asserts that two files are not equal. If they are equal
        /// an <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">The path to a file containing the value that is expected</param>
        /// <param name="actual">The path to a file containing the actual value</param>
        /// <param name="message">The message to display if objects are not equal</param>
        public static void AreNotEqual( string expected, string actual, string message )
        {
            AreNotEqual( expected, actual, message, null );
        }

        /// <summary>
        /// Asserts that two files are not equal. If they are equal
        /// an <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">The path to a file containing the value that is expected</param>
        /// <param name="actual">The path to a file containing the actual value</param>
        public static void AreNotEqual( string expected, string actual )
        {
            AreNotEqual( expected, actual, string.Empty, null );
        }

        #endregion

        #endregion
    }
}