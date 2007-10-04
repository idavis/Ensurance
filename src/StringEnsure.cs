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

using System.ComponentModel;
using Ensurance.Constraints;

namespace Ensurance
{
    /// <summary>
    /// Basic Asserts on strings.
    /// </summary>
    public class StringEnsure<T> where T : IEnsuranceHandler
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

        #region Contains

        /// <summary>
        /// Asserts that a string is found within another string.
        /// </summary>
        /// <param name="expected">The expected string</param>
        /// <param name="actual">The string to be examined</param>
        /// <param name="message">The message to display in case of failure</param>
        /// <param name="args">Arguments used in formatting the message</param>
        public static void Contains( string expected, string actual, string message, params object[] args )
        {
            EnsureBase<T>.That( actual, new SubstringConstraint( expected ), message, args );
        }

        /// <summary>
        /// Asserts that a string is found within another string.
        /// </summary>
        /// <param name="expected">The expected string</param>
        /// <param name="actual">The string to be examined</param>
        /// <param name="message">The message to display in case of failure</param>
        public static void Contains( string expected, string actual, string message )
        {
            Contains( expected, actual, message, null );
        }

        /// <summary>
        /// Asserts that a string is found within another string.
        /// </summary>
        /// <param name="expected">The expected string</param>
        /// <param name="actual">The string to be examined</param>
        public static void Contains( string expected, string actual )
        {
            Contains( expected, actual, string.Empty, null );
        }

        #endregion

        #region StartsWith

        /// <summary>
        /// Asserts that a string starts with another string.
        /// </summary>
        /// <param name="expected">The expected string</param>
        /// <param name="actual">The string to be examined</param>
        /// <param name="message">The message to display in case of failure</param>
        /// <param name="args">Arguments used in formatting the message</param>
        public static void StartsWith( string expected, string actual, string message, params object[] args )
        {
            EnsureBase<T>.That( actual, new StartsWithConstraint( expected ), message, args );
        }

        /// <summary>
        /// Asserts that a string starts with another string.
        /// </summary>
        /// <param name="expected">The expected string</param>
        /// <param name="actual">The string to be examined</param>
        /// <param name="message">The message to display in case of failure</param>
        public static void StartsWith( string expected, string actual, string message )
        {
            StartsWith( expected, actual, message, null );
        }

        /// <summary>
        /// Asserts that a string starts with another string.
        /// </summary>
        /// <param name="expected">The expected string</param>
        /// <param name="actual">The string to be examined</param>
        public static void StartsWith( string expected, string actual )
        {
            StartsWith( expected, actual, string.Empty, null );
        }

        #endregion

        #region EndsWith

        /// <summary>
        /// Asserts that a string ends with another string.
        /// </summary>
        /// <param name="expected">The expected string</param>
        /// <param name="actual">The string to be examined</param>
        /// <param name="message">The message to display in case of failure</param>
        /// <param name="args">Arguments used in formatting the message</param>
        public static void EndsWith( string expected, string actual, string message, params object[] args )
        {
            EnsureBase<T>.That( actual, new EndsWithConstraint( expected ), message, args );
        }

        /// <summary>
        /// Asserts that a string ends with another string.
        /// </summary>
        /// <param name="expected">The expected string</param>
        /// <param name="actual">The string to be examined</param>
        /// <param name="message">The message to display in case of failure</param>
        public static void EndsWith( string expected, string actual, string message )
        {
            EndsWith( expected, actual, message, null );
        }

        /// <summary>
        /// Asserts that a string ends with another string.
        /// </summary>
        /// <param name="expected">The expected string</param>
        /// <param name="actual">The string to be examined</param>
        public static void EndsWith( string expected, string actual )
        {
            EndsWith( expected, actual, string.Empty, null );
        }

        #endregion

        #region AreEqualIgnoringCase

        /// <summary>
        /// Asserts that two strings are equal, without regard to case.
        /// </summary>
        /// <param name="expected">The expected string</param>
        /// <param name="actual">The actual string</param>
        /// <param name="message">The message to display in case of failure</param>
        /// <param name="args">Arguments used in formatting the message</param>
        public static void AreEqualIgnoringCase( string expected, string actual, string message, params object[] args )
        {
            EnsureBase<T>.That( actual, new EqualConstraint( expected ).IgnoreCase, message, args );
        }

        /// <summary>
        /// Asserts that two strings are equal, without regard to case.
        /// </summary>
        /// <param name="expected">The expected string</param>
        /// <param name="actual">The actual string</param>
        /// <param name="message">The message to display in case of failure</param>
        public static void AreEqualIgnoringCase( string expected, string actual, string message )
        {
            AreEqualIgnoringCase( expected, actual, message, null );
        }

        /// <summary>
        /// Asserts that two strings are equal, without regard to case.
        /// </summary>
        /// <param name="expected">The expected string</param>
        /// <param name="actual">The actual string</param>
        public static void AreEqualIgnoringCase( string expected, string actual )
        {
            AreEqualIgnoringCase( expected, actual, string.Empty, null );
        }

        #endregion

        #region IsMatch

        /// <summary>
        /// Asserts that a string matches an expected regular expression pattern.
        /// </summary>
        /// <param name="expected">The expected expression</param>
        /// <param name="actual">The actual string</param>
        /// <param name="message">The message to display in case of failure</param>
        /// <param name="args">Arguments used in formatting the message</param>
        public static void IsMatch( string expected, string actual, string message, params object[] args )
        {
            EnsureBase<T>.That( actual, new RegexConstraint( expected ), message, args );
        }

        /// <summary>
        /// Asserts that a string matches an expected regular expression pattern.
        /// </summary>
        /// <param name="expected">The expected expression</param>
        /// <param name="actual">The actual string</param>
        /// <param name="message">The message to display in case of failure</param>
        public static void IsMatch( string expected, string actual, string message )
        {
            IsMatch( expected, actual, message, null );
        }

        /// <summary>
        /// Asserts that a string matches an expected regular expression pattern.
        /// </summary>
        /// <param name="expected">The expected expression</param>
        /// <param name="actual">The actual string</param>
        public static void IsMatch( string expected, string actual )
        {
            IsMatch( expected, actual, string.Empty, null );
        }

        #endregion
    }
}