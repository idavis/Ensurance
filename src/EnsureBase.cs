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
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using Ensurance.Constraints;
using Ensurance.SyntaxHelpers;

namespace Ensurance
{
    /// <summary>
    /// The Assert class contains a collection of static methods that
    /// implement the most common assertions used in NUnit.
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class EnsureBase<T> where T : IEnsuranceHandler
    {
        #region Private Members

        private static readonly MethodInfo handleMethodInfo = typeof(T).GetMethod("Handle", BindingFlags.Static | BindingFlags.NonPublic);

        #endregion

        #region Constructor

        /// <summary>
        /// We don't actually want any instances of this object, but some people
        /// like to inherit from it to add other static methods. Hence, the
        /// protected constructor disallows any instances of this object. 
        /// </summary>
        protected EnsureBase()
        {
        }

        #endregion

		#region Equals and ReferenceEquals

		/// <summary>
		/// The Equals method throws an EnsuranceException. This is done 
		/// to make sure there is no mistake by calling this function.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static new bool Equals(object a, object b)
		{
			throw new EnsuranceException("Equals should not be used for Assertions");
		}

		/// <summary>
		/// override the default ReferenceEquals to throw an EnsuranceException. This 
		/// implementation makes sure there is no mistake in calling this function 
		/// as part of  
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		public static new void ReferenceEquals(object a, object b)
		{
			throw new EnsuranceException("ReferenceEquals should not be used for Assertions");
		}

		#endregion
				
		#region IsTrue

		/// <summary>
		/// Asserts that a condition is true. If the condition is false the method throws
		/// an <see cref="EnsuranceException"/>.
		/// </summary> 
		/// <param name="condition">The evaluated condition</param>
		/// <param name="message">The message to display if the condition is false</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void IsTrue(bool condition, string message, params object[] args) 
		{
            That(condition, Is.True, message, args);
		}
    
		/// <summary>
		/// Asserts that a condition is true. If the condition is false the method throws
		/// an <see cref="EnsuranceException"/>.
		/// </summary>
		/// <param name="condition">The evaluated condition</param>
		/// <param name="message">The message to display if the condition is false</param>
		static public void IsTrue(bool condition, string message) 
		{
			IsTrue(condition, message, null);
		}

		/// <summary>
		/// Asserts that a condition is true. If the condition is false the method throws
		/// an <see cref="EnsuranceException"/>.
		/// </summary>
		/// <param name="condition">The evaluated condition</param>
		static public void IsTrue(bool condition) 
		{
			IsTrue(condition, null, null);
		}

		#endregion

		#region IsFalse

		/// <summary>
		/// Asserts that a condition is false. If the condition is true the method throws
		/// an <see cref="EnsuranceException"/>.
		/// </summary>
		/// <param name="condition">The evaluated condition</param>
		/// <param name="message">The message to display if the condition is true</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void IsFalse(bool condition, string message, params object[] args) 
		{
            That(condition, Is.False, message, args);
		}
		
		/// <summary>
		/// Asserts that a condition is false. If the condition is true the method throws
		/// an <see cref="EnsuranceException"/>.
		/// </summary>
		/// <param name="condition">The evaluated condition</param>
		/// <param name="message">The message to display if the condition is true</param>
		static public void IsFalse(bool condition, string message) 
		{
			IsFalse( condition, message, null );
		}
		
		/// <summary>
		/// Asserts that a condition is false. If the condition is true the method throws
		/// an <see cref="EnsuranceException"/>.
		/// </summary>
		/// <param name="condition">The evaluated condition</param>
		static public void IsFalse(bool condition) 
		{
			IsFalse(condition, string.Empty, null);
		}

		#endregion

		#region IsNotNull

		/// <summary>
		/// Verifies that the object that is passed in is not equal to <code>null</code>
		/// If the object is <code>null</code> then an <see cref="EnsuranceException"/>
		/// is thrown.
		/// </summary>
		/// <param name="actual">The object that is to be tested</param>
		/// <param name="message">The message to be displayed when the object is null</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void IsNotNull(object actual, string message, params object[] args) 
		{
            That(actual, Is.Not.Null, message, args);
		}

		/// <summary>
		/// Verifies that the object that is passed in is not equal to <code>null</code>
		/// If the object is <code>null</code> then an <see cref="EnsuranceException"/>
		/// is thrown.
		/// </summary>
		/// <param name="actual">The object that is to be tested</param>
		/// <param name="message">The message to be displayed when the object is null</param>
		static public void IsNotNull(object actual, string message) 
		{
			IsNotNull(actual, message, null);
		}
    
		/// <summary>
		/// Verifies that the object that is passed in is not equal to <code>null</code>
		/// If the object is <code>null</code> then an <see cref="EnsuranceException"/>
		/// is thrown.
		/// </summary>
		/// <param name="actual">The object that is to be tested</param>
		static public void IsNotNull(object actual) 
		{
			IsNotNull(actual, string.Empty, null);
		}
    
		#endregion
		    
		#region IsNull

		/// <summary>
		/// Verifies that the object that is passed in is equal to <code>null</code>
		/// If the object is not <code>null</code> then an <see cref="EnsuranceException"/>
		/// is thrown.
		/// </summary>
		/// <param name="actual">The object that is to be tested</param>
		/// <param name="message">The message to be displayed when the object is not null</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void IsNull(object actual, string message, params object[] args) 
		{
			That( actual, Is.Null, message, args );
		}

		/// <summary>
		/// Verifies that the object that is passed in is equal to <code>null</code>
		/// If the object is not <code>null</code> then an <see cref="EnsuranceException"/>
		/// is thrown.
		/// </summary>
		/// <param name="actual">The object that is to be tested</param>
		/// <param name="message">The message to be displayed when the object is not null</param>
		static public void IsNull(object actual, string message) 
		{
			IsNull(actual, message, null);
		}
    
		/// <summary>
		/// Verifies that the object that is passed in is equal to <code>null</code>
		/// If the object is not null <code>null</code> then an <see cref="EnsuranceException"/>
		/// is thrown.
		/// </summary>
		/// <param name="actual">The object that is to be tested</param>
		static public void IsNull(object actual) 
		{
			IsNull(actual, string.Empty, null);
		}
    
		#endregion

		#region IsNaN

		/// <summary>
		/// Verifies that the double is passed is an <code>NaN</code> value.
		/// If the object is not <code>NaN</code> then an <see cref="EnsuranceException"/>
		/// is thrown.
		/// </summary>
		/// <param name="aDouble">The value that is to be tested</param>
		/// <param name="message">The message to be displayed when the object is not null</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void IsNaN(double aDouble, string message, params object[] args) 
		{
            That(aDouble, Is.NaN, message, args);
		}

		/// <summary>
		/// Verifies that the double is passed is an <code>NaN</code> value.
		/// If the object is not <code>NaN</code> then an <see cref="EnsuranceException"/>
		/// is thrown.
		/// </summary>
		/// <param name="aDouble">The object that is to be tested</param>
		/// <param name="message">The message to be displayed when the object is not null</param>
		static public void IsNaN(double aDouble, string message) 
		{
			IsNaN(aDouble, message, null);
		}
    
		/// <summary>
		/// Verifies that the double is passed is an <code>NaN</code> value.
		/// If the object is not <code>NaN</code> then an <see cref="EnsuranceException"/>
		/// is thrown.
		/// </summary>
		/// <param name="aDouble">The object that is to be tested</param>
		static public void IsNaN(double aDouble) 
		{
			IsNaN(aDouble, string.Empty, null);
		}
    
		#endregion

		#region IsEmpty

		/// <summary>
		/// Assert that a string is empty - that is equal to string.Empty
		/// </summary>
		/// <param name="actual">The string to be tested</param>
		/// <param name="message">The message to be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		public static void IsEmpty( string actual, string message, params object[] args )
		{
            That(actual, Is.Empty, message, args);
		}

		/// <summary>
		/// Assert that a string is empty - that is equal to string.Emtpy
		/// </summary>
		/// <param name="actual">The string to be tested</param>
		/// <param name="message">The message to be displayed on failure</param>
		public static void IsEmpty( string actual, string message )
		{
			IsEmpty( actual, message, null );
		}

		/// <summary>
		/// Assert that a string is empty - that is equal to string.Emtpy
		/// </summary>
		/// <param name="actual">The string to be tested</param>
		public static void IsEmpty( string actual )
		{
			IsEmpty( actual, string.Empty, null );
		}

		/// <summary>
		/// Assert that an array, list or other collection is empty
		/// </summary>
		/// <param name="collection">An array, list or other collection implementing ICollection</param>
		/// <param name="message">The message to be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		public static void IsEmpty( ICollection collection, string message, params object[] args )
		{
            That(collection, Is.Empty, message, args);
		}

		/// <summary>
		/// Assert that an array, list or other collection is empty
		/// </summary>
		/// <param name="collection">An array, list or other collection implementing ICollection</param>
		/// <param name="message">The message to be displayed on failure</param>
		public static void IsEmpty( ICollection collection, string message )
		{
			IsEmpty( collection, message, null );
		}

		/// <summary>
		/// Assert that an array,list or other collection is empty
		/// </summary>
		/// <param name="collection">An array, list or other collection implementing ICollection</param>
		public static void IsEmpty( ICollection collection )
		{
			IsEmpty( collection, string.Empty, null );
		}
		#endregion

		#region IsNotEmpty
		/// <summary>
		/// Assert that a string is not empty - that is not equal to string.Empty
		/// </summary>
		/// <param name="actual">The string to be tested</param>
		/// <param name="message">The message to be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		public static void IsNotEmpty( string actual, string message, params object[] args )
		{
            That(actual, Is.Not.Empty, message, args);
		}

		/// <summary>
		/// Assert that a string is empty - that is equal to string.Emtpy
		/// </summary>
		/// <param name="actual">The string to be tested</param>
		/// <param name="message">The message to be displayed on failure</param>
		public static void IsNotEmpty( string actual, string message )
		{
			IsNotEmpty( actual, message, null );
		}

		/// <summary>
		/// Assert that a string is empty - that is equal to string.Emtpy
		/// </summary>
		/// <param name="actual">The string to be tested</param>
		public static void IsNotEmpty( string actual )
		{
			IsNotEmpty( actual, string.Empty, null );
		}

		/// <summary>
		/// Assert that an array, list or other collection is empty
		/// </summary>
		/// <param name="collection">An array, list or other collection implementing ICollection</param>
		/// <param name="message">The message to be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		public static void IsNotEmpty( ICollection collection, string message, params object[] args )
		{
            That(collection, Is.Not.Empty, message, args);
		}

		/// <summary>
		/// Assert that an array, list or other collection is empty
		/// </summary>
		/// <param name="collection">An array, list or other collection implementing ICollection</param>
		/// <param name="message">The message to be displayed on failure</param>
		public static void IsNotEmpty( ICollection collection, string message )
		{
			IsNotEmpty( collection, message, null );
		}

		/// <summary>
		/// Assert that an array,list or other collection is empty
		/// </summary>
		/// <param name="collection">An array, list or other collection implementing ICollection</param>
		public static void IsNotEmpty( ICollection collection )
		{
			IsNotEmpty( collection, string.Empty, null );
		}
		#endregion

		#region IsAssignableFrom
		/// <summary>
		/// Asserts that an object may be assigned a  value of a given Type.
		/// </summary>
		/// <param name="expected">The expected Type.</param>
		/// <param name="actual">The object under examination</param>
		static public void IsAssignableFrom( Type expected, object actual )
		{
			IsAssignableFrom(expected, actual, "");
		}

		/// <summary>
		/// Asserts that an object may be assigned a  value of a given Type.
		/// </summary>
		/// <param name="expected">The expected Type.</param>
		/// <param name="actual">The object under examination</param>
		/// <param name="message">The messge to display in case of failure</param>
		static public void IsAssignableFrom( Type expected, object actual, string message )
		{
			IsAssignableFrom(expected, actual, message, null);
		}
		
		/// <summary>
		/// Asserts that an object may be assigned a  value of a given Type.
		/// </summary>
		/// <param name="expected">The expected Type.</param>
		/// <param name="actual">The object under examination</param>
		/// <param name="message">The message to display in case of failure</param>
		/// <param name="args">Array of objects to be used in formatting the message</param>
		static public void IsAssignableFrom( Type expected, object actual, string message, params object[] args )
		{
            That(actual, Is.AssignableFrom(expected), message, args);
		}
		#endregion
		
		#region IsNotAssignableFrom
		/// <summary>
		/// Asserts that an object may not be assigned a  value of a given Type.
		/// </summary>
		/// <param name="expected">The expected Type.</param>
		/// <param name="actual">The object under examination</param>
		static public void IsNotAssignableFrom( Type expected, object actual )
		{
			IsNotAssignableFrom(expected, actual, "");
		}
		
		/// <summary>
		/// Asserts that an object may not be assigned a  value of a given Type.
		/// </summary>
		/// <param name="expected">The expected Type.</param>
		/// <param name="actual">The object under examination</param>
		/// <param name="message">The messge to display in case of failure</param>
		static public void IsNotAssignableFrom( Type expected, object actual, string message )
		{
			IsNotAssignableFrom(expected, actual, message, null);
		}
		
		/// <summary>
		/// Asserts that an object may not be assigned a  value of a given Type.
		/// </summary>
		/// <param name="expected">The expected Type.</param>
		/// <param name="actual">The object under examination</param>
		/// <param name="message">The message to display in case of failure</param>
		/// <param name="args">Array of objects to be used in formatting the message</param>
		static public void IsNotAssignableFrom( Type expected, object actual, string message, params object[] args )
		{
            That(actual, Is.Not.AssignableFrom(expected), message, args);
		}
		#endregion
		
		#region IsInstanceOfType
		/// <summary>
		/// Asserts that an object is an instance of a given type.
		/// </summary>
		/// <param name="expected">The expected Type</param>
		/// <param name="actual">The object being examined</param>
		public static void IsInstanceOfType( Type expected, object actual )
		{
			IsInstanceOfType( expected, actual, string.Empty, null );
		}

		/// <summary>
		/// Asserts that an object is an instance of a given type.
		/// </summary>
		/// <param name="expected">The expected Type</param>
		/// <param name="actual">The object being examined</param>
		/// <param name="message">A message to display in case of failure</param>
		public static void IsInstanceOfType( Type expected, object actual, string message )
		{
			IsInstanceOfType( expected, actual, message, null );
		}

		/// <summary>
		/// Asserts that an object is an instance of a given type.
		/// </summary>
		/// <param name="expected">The expected Type</param>
		/// <param name="actual">The object being examined</param>
		/// <param name="message">A message to display in case of failure</param>
		/// <param name="args">An array of objects to be used in formatting the message</param>
		public static void IsInstanceOfType( Type expected, object actual, string message, params object[] args )
		{
            That(actual, Is.InstanceOfType(expected), message, args);
		}
		#endregion

		#region IsNotInstanceOfType
		/// <summary>
		/// Asserts that an object is not an instance of a given type.
		/// </summary>
		/// <param name="expected">The expected Type</param>
		/// <param name="actual">The object being examined</param>
		public static void IsNotInstanceOfType( Type expected, object actual )
		{
			IsNotInstanceOfType( expected, actual, string.Empty, null );
		}

		/// <summary>
		/// Asserts that an object is not an instance of a given type.
		/// </summary>
		/// <param name="expected">The expected Type</param>
		/// <param name="actual">The object being examined</param>
		/// <param name="message">A message to display in case of failure</param>
		public static void IsNotInstanceOfType( Type expected, object actual, string message )
		{
			IsNotInstanceOfType( expected, actual, message, null );
		}

		/// <summary>
		/// Asserts that an object is not an instance of a given type.
		/// </summary>
		/// <param name="expected">The expected Type</param>
		/// <param name="actual">The object being examined</param>
		/// <param name="message">A message to display in case of failure</param>
		/// <param name="args">An array of objects to be used in formatting the message</param>
		public static void IsNotInstanceOfType( Type expected, object actual, string message, params object[] args )
		{
            That(actual, Is.Not.InstanceOfType(expected), message, args);
		}
		#endregion

		#region AreEqual

        #region Ints

        /// <summary>
        /// Verifies that two ints are equal. If they are not, then an 
        /// <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value</param>
		/// <param name="message">The message that will be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void AreEqual(int expected,
            int actual, string message, params object[] args)
        {
            That(actual, Is.EqualTo(expected), message, args);
        }

        /// <summary>
        /// Verifies that two ints are equal. If they are not, then an 
        /// <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value</param>
        /// <param name="message">The message that will be displayed on failure</param>
        static public void AreEqual(int expected, int actual, string message)
        {
            AreEqual(expected, actual, message, null);
        }

        /// <summary>
        /// Verifies that two ints are equal. If they are not, then an 
        /// <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value</param>
        static public void AreEqual(int expected, int actual)
        {
            AreEqual(expected, actual, string.Empty, null);
        }

        #endregion

        #region Longs

        /// <summary>
        /// Verifies that two longs are equal. If they are not, then an 
        /// <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value</param>
        /// <param name="message">The message that will be displayed on failure</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        static public void AreEqual(long expected,
            long actual, string message, params object[] args)
        {
            That(actual, Is.EqualTo(expected), message, args);
        }

        /// <summary>
        /// Verifies that two longs are equal. If they are not, then an 
        /// <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value</param>
        /// <param name="message">The message that will be displayed on failure</param>
        static public void AreEqual(long expected, long actual, string message)
        {
            AreEqual(expected, actual, message, null);
        }

        /// <summary>
        /// Verifies that two longs are equal. If they are not, then an 
        /// <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value</param>
        static public void AreEqual(long expected, long actual)
        {
            AreEqual(expected, actual, string.Empty, null);
        }

        #endregion

        #region UInts

        /// <summary>
        /// Verifies that two uints are equal. If they are not, then an 
        /// <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value</param>
        /// <param name="message">The message that will be displayed on failure</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
		[CLSCompliant(false)]
		static public void AreEqual(uint expected,
            uint actual, string message, params object[] args)
        {
            That(actual, Is.EqualTo(expected), message, args);
        }

        /// <summary>
        /// Verifies that two uints are equal. If they are not, then an 
        /// <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value</param>
        /// <param name="message">The message that will be displayed on failure</param>
		[CLSCompliant(false)]
		static public void AreEqual(uint expected, uint actual, string message)
        {
            AreEqual(expected, actual, message, null);
        }

        /// <summary>
        /// Verifies that two uints are equal. If they are not, then an 
        /// <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value</param>
		[CLSCompliant(false)]
		static public void AreEqual(uint expected, uint actual)
        {
            AreEqual(expected, actual, string.Empty, null);
        }

        #endregion

        #region Ulongs

        /// <summary>
        /// Verifies that two ulongs are equal. If they are not, then an 
        /// <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value</param>
        /// <param name="message">The message that will be displayed on failure</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
		[CLSCompliant(false)]
		static public void AreEqual(ulong expected,
            ulong actual, string message, params object[] args)
        {
            That(actual, Is.EqualTo(expected), message, args);
        }

        /// <summary>
        /// Verifies that two ulongs are equal. If they are not, then an 
        /// <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value</param>
        /// <param name="message">The message that will be displayed on failure</param>
		[CLSCompliant(false)]
		static public void AreEqual(ulong expected, ulong actual, string message)
        {
            AreEqual(expected, actual, message, null);
        }

        /// <summary>
        /// Verifies that two ulongs are equal. If they are not, then an 
        /// <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The actual value</param>
		[CLSCompliant(false)]
		static public void AreEqual(ulong expected, ulong actual)
        {
            AreEqual(expected, actual, string.Empty, null);
        }

        #endregion

        #region Decimals

		/// <summary>
		/// Verifies that two decimals are equal. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="expected">The expected value</param>
		/// <param name="actual">The actual value</param>
		/// <param name="message">The message that will be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void AreEqual(decimal expected, 
			decimal actual, string message, params object[] args) 
		{
            That(actual, Is.EqualTo(expected), message, args);
        }

		/// <summary>
		/// Verifies that two decimal are equal. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="expected">The expected value</param>
		/// <param name="actual">The actual value</param>
		/// <param name="message">The message that will be displayed on failure</param>
		static public void AreEqual(decimal expected, decimal actual, string message) 
		{
			AreEqual( expected, actual, message, null );
		}

		/// <summary>
		/// Verifies that two decimals are equal. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="expected">The expected value</param>
		/// <param name="actual">The actual value</param>
		static public void AreEqual(decimal expected, decimal actual ) 
		{
			AreEqual( expected, actual, string.Empty, null );
		}

		#endregion

		#region Doubles

		/// <summary>
		/// Verifies that two doubles are equal considering a delta. If the
		/// expected value is infinity then the delta value is ignored. If 
		/// they are not equals then an <see cref="EnsuranceException"/> is
		/// thrown.
		/// </summary>
		/// <param name="expected">The expected value</param>
		/// <param name="actual">The actual value</param>
		/// <param name="delta">The maximum acceptable difference between the
		/// the expected and the actual</param>
		/// <param name="message">The message that will be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void AreEqual(double expected, 
			double actual, double delta, string message, params object[] args) 
		{
			if ( double.IsNaN(expected) || double.IsInfinity(expected) )
				That(actual, Is.EqualTo( expected ), message, args);
			else
				That(actual, Is.EqualTo(expected).Within(delta), message, args);
        }

		/// <summary>
		/// Verifies that two doubles are equal considering a delta. If the
		/// expected value is infinity then the delta value is ignored. If 
		/// they are not equals then an <see cref="EnsuranceException"/> is
		/// thrown.
		/// </summary>
		/// <param name="expected">The expected value</param>
		/// <param name="actual">The actual value</param>
		/// <param name="delta">The maximum acceptable difference between the
		/// the expected and the actual</param>
		/// <param name="message">The message that will be displayed on failure</param>
		static public void AreEqual(double expected, 
			double actual, double delta, string message) 
		{
			AreEqual( expected, actual, delta, message, null );
		}

		/// <summary>
		/// Verifies that two doubles are equal considering a delta. If the
		/// expected value is infinity then the delta value is ignored. If 
		/// they are not equals then an <see cref="EnsuranceException"/> is
		/// thrown.
		/// </summary>
		/// <param name="expected">The expected value</param>
		/// <param name="actual">The actual value</param>
		/// <param name="delta">The maximum acceptable difference between the
		/// the expected and the actual</param>
		static public void AreEqual(double expected, double actual, double delta) 
		{
			AreEqual(expected, actual, delta, string.Empty, null);
		}

		#endregion

		#region Floats

		/// <summary>
		/// Verifies that two floats are equal considering a delta. If the
		/// expected value is infinity then the delta value is ignored. If 
		/// they are not equals then an <see cref="EnsuranceException"/> is
		/// thrown.
		/// </summary>
		/// <param name="expected">The expected value</param>
		/// <param name="actual">The actual value</param>
		/// <param name="delta">The maximum acceptable difference between the
		/// the expected and the actual</param>
		/// <param name="message">The message displayed upon failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void AreEqual(float expected, 
			float actual, float delta, string message, params object[] args) 
		{
			if (float.IsNaN(expected) || float.IsInfinity(expected))
				That(actual, Is.EqualTo( expected), message, args );
			else
				That(actual, Is.EqualTo(expected).Within(delta), message, args);
		}

		/// <summary>
		/// Verifies that two floats are equal considering a delta. If the
		/// expected value is infinity then the delta value is ignored. If 
		/// they are not equals then an <see cref="EnsuranceException"/> is
		/// thrown.
		/// </summary>
		/// <param name="expected">The expected value</param>
		/// <param name="actual">The actual value</param>
		/// <param name="delta">The maximum acceptable difference between the
		/// the expected and the actual</param>
		/// <param name="message">The message displayed upon failure</param>
		static public void AreEqual(float expected, float actual, float delta, string message) 
		{
			AreEqual(expected, actual, delta, message, null);
		}

		/// <summary>
		/// Verifies that two floats are equal considering a delta. If the
		/// expected value is infinity then the delta value is ignored. If 
		/// they are not equals then an <see cref="EnsuranceException"/> is
		/// thrown.
		/// </summary>
		/// <param name="expected">The expected value</param>
		/// <param name="actual">The actual value</param>
		/// <param name="delta">The maximum acceptable difference between the
		/// the expected and the actual</param>
		static public void AreEqual(float expected, float actual, float delta) 
		{
			AreEqual(expected, actual, delta, string.Empty, null);
		}

		#endregion

		#region Objects
		
		/// <summary>
		/// Verifies that two objects are equal.  Two objects are considered
		/// equal if both are null, or if both have the same value.  All
		/// non-numeric types are compared by using the <c>Equals</c> method.
		/// Arrays are compared by comparing each element using the same rules.
		/// If they are not equal an <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="expected">The value that is expected</param>
		/// <param name="actual">The actual value</param>
		/// <param name="message">The message to display if objects are not equal</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void AreEqual(Object expected, Object actual, string message, params object[] args)
		{
            That(actual, Is.EqualTo(expected), message, args);
        }

		/// <summary>
		/// Verifies that two objects are equal.  Two objects are considered
		/// equal if both are null, or if both have the same value.  All
		/// non-numeric types are compared by using the <c>Equals</c> method.
		/// If they are not equal an <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="expected">The value that is expected</param>
		/// <param name="actual">The actual value</param>
		/// <param name="message">The message to display if objects are not equal</param>
		static public void AreEqual(Object expected, Object actual, string message) 
		{
			AreEqual(expected, actual, message, null);
		}

		/// <summary>
		/// Verifies that two objects are equal.  Two objects are considered
		/// equal if both are null, or if both have the same value.  All
		/// non-numeric types are compared by using the <c>Equals</c> method.
		/// If they are not equal an <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="expected">The value that is expected</param>
		/// <param name="actual">The actual value</param>
		static public void AreEqual(Object expected, Object actual) 
		{
			AreEqual(expected, actual, string.Empty, null);
		}

		#endregion

		#endregion

		#region AreNotEqual

		#region Objects
		/// <summary>
		/// Asserts that two objects are not equal. If they are equal
		/// an <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="expected">The expected object</param>
		/// <param name="actual">The actual object</param>
		/// <param name="message">The message to be displayed when the two objects are the same object.</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void AreNotEqual( Object expected, Object actual, string message, params object[] args)
		{
            That(actual, Is.Not.EqualTo(expected), message, args);
        }

		/// <summary>
		/// Asserts that two objects are not equal. If they are equal
		/// an <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="expected">The expected object</param>
		/// <param name="actual">The actual object</param>
		/// <param name="message">The message to be displayed when the objects are the same</param>
		static public void AreNotEqual(Object expected, Object actual, string message) 
		{
			AreNotEqual(expected, actual, message, null);
		}
   
		/// <summary>
		/// Asserts that two objects are not equal. If they are equal
		/// an <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="expected">The expected object</param>
		/// <param name="actual">The actual object</param>
		static public void AreNotEqual(Object expected, Object actual) 
		{
			AreNotEqual(expected, actual, string.Empty, null);
		}
   
		#endregion

        #region Ints
        /// <summary>
        /// Asserts that two ints are not equal. If they are equal
        /// an <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">The expected object</param>
        /// <param name="actual">The actual object</param>
        /// <param name="message">The message to be displayed when the two objects are the same object.</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        static public void AreNotEqual(int expected, int actual, string message, params object[] args)
        {
            That(actual, Is.Not.EqualTo(expected), message, args);
        }

        /// <summary>
        /// Asserts that two ints are not equal. If they are equal
        /// an <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">The expected object</param>
        /// <param name="actual">The actual object</param>
        /// <param name="message">The message to be displayed when the objects are the same</param>
        static public void AreNotEqual(int expected, int actual, string message)
        {
            AreNotEqual(expected, actual, message, null);
        }

        /// <summary>
        /// Asserts that two ints are not equal. If they are equal
        /// an <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">The expected object</param>
        /// <param name="actual">The actual object</param>
        static public void AreNotEqual(int expected, int actual)
        {
            AreNotEqual(expected, actual, string.Empty, null);
        }
        #endregion

        #region Longs
        /// <summary>
        /// Asserts that two longss are not equal. If they are equal
        /// an <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">The expected object</param>
        /// <param name="actual">The actual object</param>
        /// <param name="message">The message to be displayed when the two objects are the same object.</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        static public void AreNotEqual(long expected, long actual, string message, params object[] args)
        {
            That(actual, Is.Not.EqualTo(expected), message, args);
        }

        /// <summary>
        /// Asserts that two longs are not equal. If they are equal
        /// an <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">The expected object</param>
        /// <param name="actual">The actual object</param>
        /// <param name="message">The message to be displayed when the objects are the same</param>
        static public void AreNotEqual(long expected, long actual, string message)
        {
            AreNotEqual(expected, actual, message, null);
        }

        /// <summary>
        /// Asserts that two longs are not equal. If they are equal
        /// an <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">The expected object</param>
        /// <param name="actual">The actual object</param>
        static public void AreNotEqual(long expected, long actual)
        {
            AreNotEqual(expected, actual, string.Empty, null);
        }
        #endregion

        #region UInts
        /// <summary>
        /// Asserts that two uints are not equal. If they are equal
        /// an <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">The expected object</param>
        /// <param name="actual">The actual object</param>
        /// <param name="message">The message to be displayed when the two objects are the same object.</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
		[CLSCompliant(false)]
		static public void AreNotEqual(uint expected, uint actual, string message, params object[] args)
        {
            That(actual, Is.Not.EqualTo(expected), message, args);
        }

        /// <summary>
        /// Asserts that two uints are not equal. If they are equal
        /// an <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">The expected object</param>
        /// <param name="actual">The actual object</param>
        /// <param name="message">The message to be displayed when the objects are the same</param>
		[CLSCompliant(false)]
		static public void AreNotEqual(uint expected, uint actual, string message)
        {
            AreNotEqual(expected, actual, message, null);
        }

        /// <summary>
        /// Asserts that two uints are not equal. If they are equal
        /// an <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">The expected object</param>
        /// <param name="actual">The actual object</param>
		[CLSCompliant(false)]
		static public void AreNotEqual(uint expected, uint actual)
        {
            AreNotEqual(expected, actual, string.Empty, null);
        }
        #endregion

        #region Ulongs
        /// <summary>
        /// Asserts that two ulongs are not equal. If they are equal
        /// an <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">The expected object</param>
        /// <param name="actual">The actual object</param>
        /// <param name="message">The message to be displayed when the two objects are the same object.</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
		[CLSCompliant(false)]
		static public void AreNotEqual(ulong expected, ulong actual, string message, params object[] args)
        {
            That(actual, Is.Not.EqualTo(expected), message, args);
        }

        /// <summary>
        /// Asserts that two ulongs are not equal. If they are equal
        /// an <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">The expected object</param>
        /// <param name="actual">The actual object</param>
        /// <param name="message">The message to be displayed when the objects are the same</param>
		[CLSCompliant(false)]
		static public void AreNotEqual(ulong expected, ulong actual, string message)
        {
            AreNotEqual(expected, actual, message, null);
        }

        /// <summary>
        /// Asserts that two ulong are not equal. If they are equal
        /// an <see cref="EnsuranceException"/> is thrown.
        /// </summary>
        /// <param name="expected">The expected object</param>
        /// <param name="actual">The actual object</param>
		[CLSCompliant(false)]
		static public void AreNotEqual(ulong expected, ulong actual)
        {
            AreNotEqual(expected, actual, string.Empty, null);
        }
        #endregion

        #region Decimals
		/// <summary>
		/// Asserts that two decimals are not equal. If they are equal
		/// an <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="expected">The expected object</param>
		/// <param name="actual">The actual object</param>
		/// <param name="message">The message to be displayed when the two objects are the same object.</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void AreNotEqual( decimal expected, decimal actual, string message, params object[] args)
		{
            That(actual, Is.Not.EqualTo(expected), message, args);
        }

		/// <summary>
		/// Asserts that two decimals are not equal. If they are equal
		/// an <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="expected">The expected object</param>
		/// <param name="actual">The actual object</param>
		/// <param name="message">The message to be displayed when the objects are the same</param>
		static public void AreNotEqual(decimal expected, decimal actual, string message) 
		{
			AreNotEqual(expected, actual, message, null);
		}
   
		/// <summary>
		/// Asserts that two decimals are not equal. If they are equal
		/// an <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="expected">The expected object</param>
		/// <param name="actual">The actual object</param>
		static public void AreNotEqual(decimal expected, decimal actual) 
		{
			AreNotEqual(expected, actual, string.Empty, null);
		}
		#endregion

		#region Floats
		/// <summary>
		/// Asserts that two floats are not equal. If they are equal
		/// an <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="expected">The expected object</param>
		/// <param name="actual">The actual object</param>
		/// <param name="message">The message to be displayed when the two objects are the same object.</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void AreNotEqual( float expected, float actual, string message, params object[] args)
		{
            That(actual, Is.Not.EqualTo(expected), message, args);
		}

		/// <summary>
		/// Asserts that two floats are not equal. If they are equal
		/// an <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="expected">The expected object</param>
		/// <param name="actual">The actual object</param>
		/// <param name="message">The message to be displayed when the objects are the same</param>
		static public void AreNotEqual(float expected, float actual, string message) 
		{
			AreNotEqual(expected, actual, message, null);
		}
   
		/// <summary>
		/// Asserts that two floats are not equal. If they are equal
		/// an <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="expected">The expected object</param>
		/// <param name="actual">The actual object</param>
		static public void AreNotEqual(float expected, float actual) 
		{
			AreNotEqual(expected, actual, string.Empty, null);
		}
		#endregion

		#region Doubles
		/// <summary>
		/// Asserts that two doubles are not equal. If they are equal
		/// an <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="expected">The expected object</param>
		/// <param name="actual">The actual object</param>
		/// <param name="message">The message to be displayed when the two objects are the same object.</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void AreNotEqual( double expected, double actual, string message, params object[] args)
		{
            That(actual, Is.Not.EqualTo(expected), message, args);
		}

		/// <summary>
		/// Asserts that two doubles are not equal. If they are equal
		/// an <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="expected">The expected object</param>
		/// <param name="actual">The actual object</param>
		/// <param name="message">The message to be displayed when the objects are the same</param>
		static public void AreNotEqual(double expected, double actual, string message) 
		{
			AreNotEqual(expected, actual, message, null);
		}
   
		/// <summary>
		/// Asserts that two doubles are not equal. If they are equal
		/// an <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="expected">The expected object</param>
		/// <param name="actual">The actual object</param>
		static public void AreNotEqual(double expected, double actual) 
		{
			AreNotEqual(expected, actual, string.Empty, null);
		}
		#endregion

		#endregion

		#region AreSame

		/// <summary>
		/// Asserts that two objects refer to the same object. If they
		/// are not the same an <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="expected">The expected object</param>
		/// <param name="actual">The actual object</param>
		/// <param name="message">The message to be displayed when the two objects are not the same object.</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void AreSame(Object expected, Object actual, string message, params object[] args)
		{
            That(actual, Is.SameAs(expected), message, args);
		}

		/// <summary>
		/// Asserts that two objects refer to the same object. If they
		/// are not the same an <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="expected">The expected object</param>
		/// <param name="actual">The actual object</param>
		/// <param name="message">The message to be displayed when the object is null</param>
		static public void AreSame(Object expected, Object actual, string message) 
		{
			AreSame(expected, actual, message, null);
		}
   
		/// <summary>
		/// Asserts that two objects refer to the same object. If they
		/// are not the same an <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="expected">The expected object</param>
		/// <param name="actual">The actual object</param>
		static public void AreSame(Object expected, Object actual) 
		{
			AreSame(expected, actual, string.Empty, null);
		}
   
		#endregion

		#region AreNotSame

		/// <summary>
		/// Asserts that two objects do not refer to the same object. If they
		/// are the same an <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="expected">The expected object</param>
		/// <param name="actual">The actual object</param>
		/// <param name="message">The message to be displayed when the two objects are the same object.</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void AreNotSame(Object expected, Object actual, string message, params object[] args)
		{
            That(actual, Is.Not.SameAs(expected), message, args);
		}

		/// <summary>
		/// Asserts that two objects do not refer to the same object. If they
		/// are the same an <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="expected">The expected object</param>
		/// <param name="actual">The actual object</param>
		/// <param name="message">The message to be displayed when the objects are the same</param>
		static public void AreNotSame(Object expected, Object actual, string message) 
		{
			AreNotSame(expected, actual, message, null);
		}
   
		/// <summary>
		/// Asserts that two objects do not refer to the same object. If they
		/// are the same an <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="expected">The expected object</param>
		/// <param name="actual">The actual object</param>
		static public void AreNotSame(Object expected, Object actual) 
		{
			AreNotSame(expected, actual, string.Empty, null);
		}
   
		#endregion

		#region Greater

		#region Ints

		/// <summary>
		/// Verifies that the first value is greater than the second
		/// value. If they are not, then an
		/// <see cref="EnsuranceException"/> is thrown. 
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		/// <param name="message">The message that will be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void Greater(int arg1, 
			int arg2, string message, params object[] args) 
		{
            That(arg1, Is.GreaterThan(arg2), message, args);
		}

		/// <summary>
		/// Verifies that the first value is greater than the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		/// <param name="message">The message that will be displayed on failure</param>
		static public void Greater(int arg1, int arg2, string message) 
		{
			Greater( arg1, arg2, message, null );
		}

		/// <summary>
		/// Verifies that the first value is greater than the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		static public void Greater(int arg1, int arg2 ) 
		{
			Greater( arg1, arg2, string.Empty, null );
		}

		#endregion

		#region UInts

		/// <summary>
		/// Verifies that the first value is greater than the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		/// <param name="message">The message that will be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		[CLSCompliant(false)]
		static public void Greater(uint arg1, 
			uint arg2, string message, params object[] args) 
		{
			That(arg1, Is.GreaterThan(arg2), message, args);
		}

		/// <summary>
		/// Verifies that the first value is greater than the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		/// <param name="message">The message that will be displayed on failure</param>
		[CLSCompliant(false)]
		static public void Greater(uint arg1, uint arg2, string message) 
		{
			Greater( arg1, arg2, message, null );
		}

		/// <summary>
		/// Verifies that the first value is greater than the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		[CLSCompliant(false)]
		static public void Greater(uint arg1, uint arg2 ) 
		{
			Greater( arg1, arg2, string.Empty, null );
		}

		#endregion

		#region Longs

		/// <summary>
		/// Verifies that the first value is greater than the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		/// <param name="message">The message that will be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void Greater(long arg1, 
			long arg2, string message, params object[] args) 
		{
			That(arg1, Is.GreaterThan(arg2), message, args);
		}

		/// <summary>
		/// Verifies that the first value is greater than the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		/// <param name="message">The message that will be displayed on failure</param>
		static public void Greater(long arg1, long arg2, string message) 
		{
			Greater( arg1, arg2, message, null );
		}

		/// <summary>
		/// Verifies that the first value is greater than the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		static public void Greater(long arg1, long arg2 ) 
		{
			Greater( arg1, arg2, string.Empty, null );
		}

		#endregion

		#region ULongs

		/// <summary>
		/// Verifies that the first value is greater than the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		/// <param name="message">The message that will be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		[CLSCompliant(false)]
		static public void Greater(ulong arg1, 
			ulong arg2, string message, params object[] args) 
		{
			That(arg1, Is.GreaterThan(arg2), message, args);
		}

		/// <summary>
		/// Verifies that the first value is greater than the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		/// <param name="message">The message that will be displayed on failure</param>
		[CLSCompliant(false)]
		static public void Greater(ulong arg1, ulong arg2, string message) 
		{
			Greater( arg1, arg2, message, null );
		}

		/// <summary>
		/// Verifies that the first value is greater than the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		[CLSCompliant(false)]
		static public void Greater(ulong arg1, ulong arg2 ) 
		{
			Greater( arg1, arg2, string.Empty, null );
		}

		#endregion

		#region Decimals

		/// <summary>
		/// Verifies that the first value is greater than the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		/// <param name="message">The message that will be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void Greater(decimal arg1, 
			decimal arg2, string message, params object[] args) 
		{
            That(arg1, Is.GreaterThan(arg2), message, args);
        }

		/// <summary>
		/// Verifies that the first value is greater than the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		/// <param name="message">The message that will be displayed on failure</param>
		static public void Greater(decimal arg1, decimal arg2, string message) 
		{
			Greater( arg1, arg2, message, null );
		}

		/// <summary>
		/// Verifies that the first value is greater than the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		static public void Greater(decimal arg1, decimal arg2 ) 
		{
			Greater( arg1, arg2, string.Empty, null );
		}

		#endregion

		#region Doubles

		/// <summary>
		/// Verifies that the first value is greater than the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		/// <param name="message">The message that will be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void Greater(double arg1, 
			double arg2, string message, params object[] args) 
		{
            That(arg1, Is.GreaterThan(arg2), message, args);
        }

		/// <summary>
		/// Verifies that the first value is greater than the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		/// <param name="message">The message that will be displayed on failure</param>
		static public void Greater(double arg1, 
			double arg2, string message) 
		{
			Greater( arg1, arg2, message, null );
		}

		/// <summary>
		/// Verifies that the first value is greater than the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		static public void Greater(double arg1, double arg2) 
		{
			Greater(arg1, arg2, string.Empty, null);
		}

		#endregion

		#region Floats

		/// <summary>
		/// Verifies that the first value is greater than the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		/// <param name="message">The message that will be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void Greater(float arg1, 
			float arg2, string message, params object[] args) 
		{
            That(arg1, Is.GreaterThan(arg2), message, args);
        }

		/// <summary>
		/// Verifies that the first value is greater than the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		/// <param name="message">The message that will be displayed on failure</param>
		static public void Greater(float arg1, float arg2, string message) 
		{
			Greater(arg1, arg2, message, null);
		}

		/// <summary>
		/// Verifies that the first value is greater than the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		static public void Greater(float arg1, float arg2) 
		{
			Greater(arg1, arg2, string.Empty, null);
		}

		#endregion

		#region IComparables

		/// <summary>
		/// Verifies that the first value is greater than the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		/// <param name="message">The message that will be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void Greater(IComparable arg1, 
			IComparable arg2, string message, params object[] args) 
		{
            That(arg1, Is.GreaterThan(arg2), message, args);
        }

		/// <summary>
		/// Verifies that the first value is greater than the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		/// <param name="message">The message that will be displayed on failure</param>
		static public void Greater(IComparable arg1, IComparable arg2, string message) 
		{
			Greater(arg1, arg2, message, null);
		}

		/// <summary>
		/// Verifies that the first value is greater than the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		static public void Greater(IComparable arg1, IComparable arg2) 
		{
			Greater(arg1, arg2, string.Empty, null);
		}

		#endregion

		#endregion

		#region Less

		#region Ints

		/// <summary>
		/// Verifies that the first value is less than the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		/// <param name="message">The message that will be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void Less(int arg1, int arg2, string message, params object[] args) 
		{
            That(arg1, Is.LessThan(arg2), message, args);
        }

		/// <summary>
		/// Verifies that the first value is less than the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		/// <param name="message">The message that will be displayed on failure</param>
		static public void Less(int arg1, int arg2, string message) 
		{
			Less(arg1, arg2, message, null);
		}

		/// <summary>
		/// Verifies that the first value is less than the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		static public void Less(int arg1, int arg2) 
		{
			Less( arg1, arg2, string.Empty, null);
		}

		#endregion

		#region UInts

		/// <summary>
		/// Verifies that the first value is less than the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		/// <param name="message">The message that will be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		[CLSCompliant(false)]
		static public void Less(uint arg1, uint arg2, string message, params object[] args) 
		{
			That(arg1, Is.LessThan(arg2), message, args);
		}

		/// <summary>
		/// Verifies that the first value is less than the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		/// <param name="message">The message that will be displayed on failure</param>
		[CLSCompliant(false)]
		static public void Less(uint arg1, uint arg2, string message) 
		{
			Less(arg1, arg2, message, null);
		}

		/// <summary>
		/// Verifies that the first value is less than the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		[CLSCompliant(false)]
		static public void Less(uint arg1, uint arg2) 
		{
			Less( arg1, arg2, string.Empty, null);
		}

		#endregion

		#region Longs

		/// <summary>
		/// Verifies that the first value is less than the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		/// <param name="message">The message that will be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void Less(long arg1, long arg2, string message, params object[] args) 
		{
			That(arg1, Is.LessThan(arg2), message, args);
		}

		/// <summary>
		/// Verifies that the first value is less than the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		/// <param name="message">The message that will be displayed on failure</param>
		static public void Less(long arg1, long arg2, string message) 
		{
			Less(arg1, arg2, message, null);
		}

		/// <summary>
		/// Verifies that the first value is less than the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		static public void Less(long arg1, long arg2) 
		{
			Less( arg1, arg2, string.Empty, null);
		}

		#endregion

		#region ULongs

		/// <summary>
		/// Verifies that the first value is less than the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		/// <param name="message">The message that will be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		[CLSCompliant(false)]
		static public void Less(ulong arg1, ulong arg2, string message, params object[] args) 
		{
			That(arg1, Is.LessThan(arg2), message, args);
		}

		/// <summary>
		/// Verifies that the first value is less than the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		/// <param name="message">The message that will be displayed on failure</param>
		[CLSCompliant(false)]
		static public void Less(ulong arg1, ulong arg2, string message) 
		{
			Less(arg1, arg2, message, null);
		}

		/// <summary>
		/// Verifies that the first value is less than the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		[CLSCompliant(false)]
		static public void Less(ulong arg1, ulong arg2) 
		{
			Less( arg1, arg2, string.Empty, null);
		}

		#endregion

		#region Decimals

		/// <summary>
		/// Verifies that the first value is less than the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		/// <param name="message">The message that will be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void Less(decimal arg1, decimal arg2, string message, params object[] args) 
		{
            That(arg1, Is.LessThan(arg2), message, args);
        }

		/// <summary>
		/// Verifies that the first value is less than the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		/// <param name="message">The message that will be displayed on failure</param>
		static public void Less(decimal arg1, decimal arg2, string message) 
		{
			Less(arg1, arg2, message, null);
		}

		/// <summary>
		/// Verifies that the first value is less than the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		static public void Less(decimal arg1, decimal arg2) 
		{
			Less(arg1, arg2, string.Empty, null);
		}

		#endregion

		#region Doubles

		/// <summary>
		/// Verifies that the first value is less than the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		/// <param name="message">The message that will be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void Less(double arg1, double arg2, string message, params object[] args) 
		{
            That(arg1, Is.LessThan(arg2), message, args);
        }

		/// <summary>
		/// Verifies that the first value is less than the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		/// <param name="message">The message that will be displayed on failure</param>
		static public void Less(double arg1, double arg2, string message) 
		{
			Less(arg1, arg2, message, null);
		}

		/// <summary>
		/// Verifies that the first value is less than the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		static public void Less(double arg1, double arg2) 
		{
			Less(arg1, arg2, string.Empty, null);
		}

		#endregion

		#region Floats

		/// <summary>
		/// Verifies that the first value is less than the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		/// <param name="message">The message that will be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void Less(float arg1, float arg2, string message, params object[] args) 
		{
            That(arg1, Is.LessThan(arg2), message, args);
        }

		/// <summary>
		/// Verifies that the first value is less than the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		/// <param name="message">The message that will be displayed on failure</param>
		static public void Less(float arg1, float arg2, string message) 
		{
			Less(arg1, arg2, message, null);
		}

		/// <summary>
		/// Verifies that the first value is less than the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		static public void Less(float arg1, float arg2) 
		{
			Less(arg1, arg2, string.Empty, null);
		}

		#endregion

		#region IComparables

		/// <summary>
		/// Verifies that the first value is less than the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		/// <param name="message">The message that will be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void Less(IComparable arg1, IComparable arg2, string message, params object[] args) 
		{
            That(arg1, Is.LessThan(arg2), message, args);
        }

		/// <summary>
		/// Verifies that the first value is less than the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		/// <param name="message">The message that will be displayed on failure</param>
		static public void Less(IComparable arg1, IComparable arg2, string message) 
		{
			Less(arg1, arg2, message, null);
		}

		/// <summary>
		/// Verifies that the first value is less than the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		static public void Less(IComparable arg1, IComparable arg2) 
		{
			Less(arg1, arg2, string.Empty, null);
		}

		#endregion

		#endregion

		#region Collection Containment

		/// <summary>
		/// Asserts that an object is contained in a list.
		/// </summary>
		/// <param name="expected">The expected object</param>
		/// <param name="actual">The list to be examined</param>
		/// <param name="message">The message to display in case of failure</param>
		/// <param name="args">Arguments used in formatting the message</param>
		static public void Contains( object expected, ICollection actual, string message, params object[] args )
		{
            That(actual, new CollectionContainsConstraint(expected), message, args);
		}

		/// <summary>
		/// Asserts that an object is contained in a list.
		/// </summary>
		/// <param name="expected">The expected object</param>
		/// <param name="actual">The list to be examined</param>
		/// <param name="message">The message to display in case of failure</param>
		static public void Contains( object expected, ICollection actual, string message )
		{
			Contains( expected, actual, message, null );
		}

		/// <summary>
		/// Asserts that an object is contained in a list.
		/// </summary>
		/// <param name="expected">The expected object</param>
		/// <param name="actual">The list to be examined</param>
		static public void Contains( object expected, ICollection actual )
		{
			Contains( expected, actual, string.Empty, null );
		}

		#endregion

        #region Fail

        /// <summary>
        /// Throws an <see cref="EnsuranceException"/> with the message and arguments 
        /// that are passed in. This is used by the other Assert functions. 
        /// </summary>
        /// <param name="message">The message to initialize the <see cref="EnsuranceException"/> with.</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        static public void Fail(string message, params object[] args)
        {
            if (message == null) message = string.Empty;
            else if (args != null && args.Length > 0)
                message = string.Format(message, args);

            throw new EnsuranceException(message);
        }

        /// <summary>
        /// Throws an <see cref="EnsuranceException"/> with the message that is 
        /// passed in. This is used by the other Assert functions. 
        /// </summary>
        /// <param name="message">The message to initialize the <see cref="EnsuranceException"/> with.</param>
        static public void Fail(string message)
        {
            Fail(message, null);
        }

        /// <summary>
        /// Throws an <see cref="EnsuranceException"/>. 
        /// This is used by the other Assert functions. 
        /// </summary>
        static public void Fail()
        {
            Fail(string.Empty, null);
        }

        #endregion 

		#region That
        /// <summary>
        /// Apply a constraint to an actual value, succeeding if the constraint
        /// is satisfied and throwing an assertion exception on failure.
        /// </summary>
        /// <param name="constraint">A Constraint to be applied</param>
        /// <param name="actual">The actual value to test</param>
        public static void That( object actual, Constraint constraint )
        {
            That( actual, constraint, null, null );
        }

        /// <summary>
        /// Apply a constraint to an actual value, succeedingt if the constraint
        /// is satisfied and throwing an assertion exception on failure.
        /// </summary>
        /// <param name="constraint">A Constraint to be applied</param>
        /// <param name="actual">The actual value to test</param>
        /// <param name="message">The message that will be displayed on failure</param>
        public static void That( object actual, Constraint constraint, string message )
        {
            That( actual, constraint, message, null );
        }

        /// <summary>
        /// Apply a constraint to an actual value, succeedingt if the constraint
        /// is satisfied and throwing an assertion exception on failure.
        /// </summary>
        /// <param name="constraint">A Constraint to be applied</param>
        /// <param name="actual">The actual value to test</param>
        /// <param name="message">The message that will be displayed on failure</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void That(object actual, Constraint constraint, string message, params object[] args)
        {
            if(constraint == null)
            {
                throw new ArgumentNullException( "constraint", "The constraint must not be null");
            }

            if (!constraint.Matches( actual ))
            {
                try
                {
                    handleMethodInfo.Invoke(null, new object[] { constraint, message, args });
                }
                catch (TargetInvocationException ex)
                {
                    if (ex.InnerException != null)
                    {
                        throw ex.InnerException;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Asserts that a condition is true. If the condition is false the method throws
        /// an <see cref="EnsuranceException"/>.
        /// </summary> 
        /// <param name="condition">The evaluated condition</param>
        /// <param name="message">The message to display if the condition is false</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void That( bool condition, string message, params object[] args )
        {
            That( condition, Is.True, message, args );
        }

        /// <summary>
        /// Asserts that a condition is true. If the condition is false the method throws
        /// an <see cref="EnsuranceException"/>.
        /// </summary>
        /// <param name="condition">The evaluated condition</param>
        /// <param name="message">The message to display if the condition is false</param>
        public static void That( bool condition, string message )
        {
            That( condition, Is.True, message, null );
        }

        /// <summary>
        /// Asserts that a condition is true. If the condition is false the method throws
        /// an <see cref="EnsuranceException"/>.
        /// </summary>
        /// <param name="condition">The evaluated condition</param>
        public static void That( bool condition )
        {
            That( condition, Is.True, null, null );
        }
        #endregion

		#region GreaterOrEqual

		#region Ints

		/// <summary>
		/// Verifies that the first value is greater than or equal to the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown. 
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		/// <param name="message">The message that will be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void GreaterOrEqual(int arg1,
		    int arg2, string message, params object[] args)
		{
            That(arg1, Is.GreaterThanOrEqualTo(arg2), message, args);
        }

		/// <summary>
		/// Verifies that the first value is greater than or equal to the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		/// <param name="message">The message that will be displayed on failure</param>
		static public void GreaterOrEqual(int arg1, int arg2, string message)
		{
		    GreaterOrEqual(arg1, arg2, message, null);
		}

		/// <summary>
		/// Verifies that the first value is greater than or equal to the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		static public void GreaterOrEqual(int arg1, int arg2)
		{
		    GreaterOrEqual(arg1, arg2, string.Empty, null);
		}

		#endregion

		#region UInts

		/// <summary>
		/// Verifies that the first value is greater than or equal to the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		/// <param name="message">The message that will be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		[CLSCompliant(false)]
		static public void GreaterOrEqual(uint arg1,
			uint arg2, string message, params object[] args)
		{
			That(arg1, Is.GreaterThanOrEqualTo(arg2), message, args);
		}

		/// <summary>
		/// Verifies that the first value is greater than or equal to the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		/// <param name="message">The message that will be displayed on failure</param>
		[CLSCompliant(false)]
		static public void GreaterOrEqual(uint arg1, uint arg2, string message)
		{
			GreaterOrEqual(arg1, arg2, message, null);
		}

		/// <summary>
		/// Verifies that the first value is greater or equal to than the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		[CLSCompliant(false)]
		static public void GreaterOrEqual(uint arg1, uint arg2)
		{
			GreaterOrEqual(arg1, arg2, string.Empty, null);
		}

		#endregion

		#region Longs

		/// <summary>
		/// Verifies that the first value is greater than or equal to the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		/// <param name="message">The message that will be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void GreaterOrEqual(long arg1,
			long arg2, string message, params object[] args)
		{
			That(arg1, Is.GreaterThanOrEqualTo(arg2), message, args);
		}

		/// <summary>
		/// Verifies that the first value is greater than or equal to the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		/// <param name="message">The message that will be displayed on failure</param>
		static public void GreaterOrEqual(long arg1, long arg2, string message)
		{
			GreaterOrEqual(arg1, arg2, message, null);
		}

		/// <summary>
		/// Verifies that the first value is greater or equal to than the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		static public void GreaterOrEqual(long arg1, long arg2)
		{
			GreaterOrEqual(arg1, arg2, string.Empty, null);
		}

		#endregion

		#region ULongs

		/// <summary>
		/// Verifies that the first value is greater than or equal to the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		/// <param name="message">The message that will be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		[CLSCompliant(false)]
		static public void GreaterOrEqual(ulong arg1,
			ulong arg2, string message, params object[] args)
		{
			That(arg1, Is.GreaterThanOrEqualTo(arg2), message, args);
		}

		/// <summary>
		/// Verifies that the first value is greater than or equal to the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		/// <param name="message">The message that will be displayed on failure</param>
		[CLSCompliant(false)]
		static public void GreaterOrEqual(ulong arg1, ulong arg2, string message)
		{
			GreaterOrEqual(arg1, arg2, message, null);
		}

		/// <summary>
		/// Verifies that the first value is greater or equal to than the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		[CLSCompliant(false)]
		static public void GreaterOrEqual(ulong arg1, ulong arg2)
		{
			GreaterOrEqual(arg1, arg2, string.Empty, null);
		}

		#endregion

		#region Decimals

		/// <summary>
		/// Verifies that the first value is greater than or equal to the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		/// <param name="message">The message that will be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void GreaterOrEqual(decimal arg1,
		    decimal arg2, string message, params object[] args)
		{
            That(arg1, Is.GreaterThanOrEqualTo(arg2), message, args);
        }

		/// <summary>
		/// Verifies that the first value is greater than or equal to the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		/// <param name="message">The message that will be displayed on failure</param>
		static public void GreaterOrEqual(decimal arg1, decimal arg2, string message)
		{
		    GreaterOrEqual(arg1, arg2, message, null);
		}

		/// <summary>
		/// Verifies that the first value is greater than or equal to the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		static public void GreaterOrEqual(decimal arg1, decimal arg2)
		{
		    GreaterOrEqual(arg1, arg2, string.Empty, null);
		}

		#endregion

		#region Doubles

		/// <summary>
		/// Verifies that the first value is greater than or equal to the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		/// <param name="message">The message that will be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void GreaterOrEqual(double arg1,
		    double arg2, string message, params object[] args)
		{
            That(arg1, Is.GreaterThanOrEqualTo(arg2), message, args);
        }

		/// <summary>
		/// Verifies that the first value is greater than or equal to the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		/// <param name="message">The message that will be displayed on failure</param>
		static public void GreaterOrEqual(double arg1,
		    double arg2, string message)
		{
		    GreaterOrEqual(arg1, arg2, message, null);
		}

		/// <summary>
		/// Verifies that the first value is greater than or equal to the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		static public void GreaterOrEqual(double arg1, double arg2)
		{
		    GreaterOrEqual(arg1, arg2, string.Empty, null);
		}

		#endregion

		#region Floats

		/// <summary>
		/// Verifies that the first value is greater than or equal to the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		/// <param name="message">The message that will be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void GreaterOrEqual(float arg1,
		    float arg2, string message, params object[] args)
		{
            That(arg1, Is.GreaterThanOrEqualTo(arg2), message, args);
        }

		/// <summary>
		/// Verifies that the first value is greater than or equal to the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		/// <param name="message">The message that will be displayed on failure</param>
		static public void GreaterOrEqual(float arg1, float arg2, string message)
		{
		    GreaterOrEqual(arg1, arg2, message, null);
		}

		/// <summary>
		/// Verifies that the first value is greater than or equal to the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		static public void GreaterOrEqual(float arg1, float arg2)
		{
		    GreaterOrEqual(arg1, arg2, string.Empty, null);
		}

		#endregion

		#region IComparables

		/// <summary>
		/// Verifies that the first value is greater than the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		/// <param name="message">The message that will be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void GreaterOrEqual(IComparable arg1,
		    IComparable arg2, string message, params object[] args)
		{
            That(arg1, Is.GreaterThanOrEqualTo(arg2), message, args);
        }

		/// <summary>
		/// Verifies that the first value is greater than the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		/// <param name="message">The message that will be displayed on failure</param>
		static public void GreaterOrEqual(IComparable arg1, IComparable arg2, string message)
		{
		    GreaterOrEqual(arg1, arg2, message, null);
		}

		/// <summary>
		/// Verifies that the first value is greater than the second
		/// value. If they are not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be greater</param>
		/// <param name="arg2">The second value, expected to be less</param>
		static public void GreaterOrEqual(IComparable arg1, IComparable arg2)
		{
		    GreaterOrEqual(arg1, arg2, string.Empty, null);
		}

		#endregion

		#endregion

		#region LessOrEqual

		#region Ints

		/// <summary>
		/// Verifies that the first value is less than or equal to the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		/// <param name="message">The message that will be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void LessOrEqual(int arg1, int arg2, string message, params object[] args)
		{
            That(arg1, Is.LessThanOrEqualTo(arg2), message, args);
        }

		/// <summary>
		/// Verifies that the first value is less than or equal to the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		/// <param name="message">The message that will be displayed on failure</param>
		static public void LessOrEqual(int arg1, int arg2, string message)
		{
		    LessOrEqual(arg1, arg2, message, null);
		}

		/// <summary>
		/// Verifies that the first value is less than or equal to the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		static public void LessOrEqual(int arg1, int arg2)
		{
		    LessOrEqual(arg1, arg2, string.Empty, null);
		}

		#endregion

		#region UInts

		/// <summary>
		/// Verifies that the first value is less than or equal to the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		/// <param name="message">The message that will be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		[CLSCompliant(false)]
		static public void LessOrEqual(uint arg1, uint arg2, string message, params object[] args)
		{
			That(arg1, Is.LessThanOrEqualTo(arg2), message, args);
		}

		/// <summary>
		/// Verifies that the first value is less than or equal to the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		/// <param name="message">The message that will be displayed on failure</param>
		[CLSCompliant(false)]
		static public void LessOrEqual(uint arg1, uint arg2, string message)
		{
			LessOrEqual(arg1, arg2, message, null);
		}

		/// <summary>
		/// Verifies that the first value is less than or equal to the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		[CLSCompliant(false)]
		static public void LessOrEqual(uint arg1, uint arg2)
		{
			LessOrEqual(arg1, arg2, string.Empty, null);
		}

		#endregion

		#region Longs

		/// <summary>
		/// Verifies that the first value is less than or equal to the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		/// <param name="message">The message that will be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void LessOrEqual(long arg1, long arg2, string message, params object[] args)
		{
			That(arg1, Is.LessThanOrEqualTo(arg2), message, args);
		}

		/// <summary>
		/// Verifies that the first value is less than or equal to the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		/// <param name="message">The message that will be displayed on failure</param>
		static public void LessOrEqual(long arg1, long arg2, string message)
		{
			LessOrEqual(arg1, arg2, message, null);
		}

		/// <summary>
		/// Verifies that the first value is less than or equal to the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		static public void LessOrEqual(long arg1, long arg2)
		{
			LessOrEqual(arg1, arg2, string.Empty, null);
		}

		#endregion

		#region ULongs

		/// <summary>
		/// Verifies that the first value is less than or equal to the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		/// <param name="message">The message that will be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		[CLSCompliant(false)]
		static public void LessOrEqual(ulong arg1, ulong arg2, string message, params object[] args)
		{
			That(arg1, Is.LessThanOrEqualTo(arg2), message, args);
		}

		/// <summary>
		/// Verifies that the first value is less than or equal to the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		/// <param name="message">The message that will be displayed on failure</param>
		[CLSCompliant(false)]
		static public void LessOrEqual(ulong arg1, ulong arg2, string message)
		{
			LessOrEqual(arg1, arg2, message, null);
		}

		/// <summary>
		/// Verifies that the first value is less than or equal to the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		[CLSCompliant(false)]
		static public void LessOrEqual(ulong arg1, ulong arg2)
		{
			LessOrEqual(arg1, arg2, string.Empty, null);
		}

		#endregion

		#region Decimals

		/// <summary>
		/// Verifies that the first value is less than or equal to the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		/// <param name="message">The message that will be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void LessOrEqual(decimal arg1, decimal arg2, string message, params object[] args)
		{
            That(arg1, Is.LessThanOrEqualTo(arg2), message, args);
        }

		/// <summary>
		/// Verifies that the first value is less than or equal to the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		/// <param name="message">The message that will be displayed on failure</param>
		static public void LessOrEqual(decimal arg1, decimal arg2, string message)
		{
		    LessOrEqual(arg1, arg2, message, null);
		}

		/// <summary>
		/// Verifies that the first value is less than or equal to the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		static public void LessOrEqual(decimal arg1, decimal arg2)
		{
		    LessOrEqual(arg1, arg2, string.Empty, null);
		}

		#endregion

		#region Doubles

		/// <summary>
		/// Verifies that the first value is less than or equal to the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		/// <param name="message">The message that will be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void LessOrEqual(double arg1, double arg2, string message, params object[] args)
		{
            That(arg1, Is.LessThanOrEqualTo(arg2), message, args);
        }

		/// <summary>
		/// Verifies that the first value is less than or equal to the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		/// <param name="message">The message that will be displayed on failure</param>
		static public void LessOrEqual(double arg1, double arg2, string message)
		{
		    LessOrEqual(arg1, arg2, message, null);
		}

		/// <summary>
		/// Verifies that the first value is less than or equal to the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		static public void LessOrEqual(double arg1, double arg2)
		{
		    LessOrEqual(arg1, arg2, string.Empty, null);
		}

		#endregion

		#region Floats

		/// <summary>
		/// Verifies that the first value is less than or equal to the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		/// <param name="message">The message that will be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void LessOrEqual(float arg1, float arg2, string message, params object[] args)
		{
            That(arg1, Is.LessThanOrEqualTo(arg2), message, args);
        }

		/// <summary>
		/// Verifies that the first value is less than or equal to the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		/// <param name="message">The message that will be displayed on failure</param>
		static public void LessOrEqual(float arg1, float arg2, string message)
		{
		    LessOrEqual(arg1, arg2, message, null);
		}

		/// <summary>
		/// Verifies that the first value is less than or equal to the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		static public void LessOrEqual(float arg1, float arg2)
		{
		    LessOrEqual(arg1, arg2, string.Empty, null);
		}

		#endregion

		#region IComparables

		/// <summary>
		/// Verifies that the first value is less than or equal to the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		/// <param name="message">The message that will be displayed on failure</param>
		/// <param name="args">Arguments to be used in formatting the message</param>
		static public void LessOrEqual(IComparable arg1, IComparable arg2, string message, params object[] args)
		{
            That(arg1, Is.LessThanOrEqualTo(arg2), message, args);
        }

		/// <summary>
		/// Verifies that the first value is less than or equal to the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		/// <param name="message">The message that will be displayed on failure</param>
		static public void LessOrEqual(IComparable arg1, IComparable arg2, string message)
		{
		    LessOrEqual(arg1, arg2, message, null);
		}

		/// <summary>
		/// Verifies that the first value is less than or equal to the second
		/// value. If it is not, then an 
		/// <see cref="EnsuranceException"/> is thrown.
		/// </summary>
		/// <param name="arg1">The first value, expected to be less</param>
		/// <param name="arg2">The second value, expected to be greater</param>
		static public void LessOrEqual(IComparable arg1, IComparable arg2)
		{
		    LessOrEqual(arg1, arg2, string.Empty, null);
		}

		#endregion

        #endregion

        #region FileEnsure
        
        public class Files
        {
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
            public static void AreEqual(Stream expected, Stream actual, string message, params object[] args)
            {
                That(actual, new EqualConstraint(expected), message, args);
            }

            /// <summary>
            /// Verifies that two Streams are equal.  Two Streams are considered
            /// equal if both are null, or if both have the same value byte for byte.
            /// If they are not equal an <see cref="EnsuranceException"/> is thrown.
            /// </summary>
            /// <param name="expected">The expected Stream</param>
            /// <param name="actual">The actual Stream</param>
            /// <param name="message">The message to display if objects are not equal</param>
            public static void AreEqual(Stream expected, Stream actual, string message)
            {
                AreEqual(expected, actual, message, null);
            }

            /// <summary>
            /// Verifies that two Streams are equal.  Two Streams are considered
            /// equal if both are null, or if both have the same value byte for byte.
            /// If they are not equal an <see cref="EnsuranceException"/> is thrown.
            /// </summary>
            /// <param name="expected">The expected Stream</param>
            /// <param name="actual">The actual Stream</param>
            public static void AreEqual(Stream expected, Stream actual)
            {
                AreEqual(expected, actual, string.Empty, null);
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
            public static void AreEqual(FileInfo expected, FileInfo actual, string message, params object[] args)
            {
                using (FileStream exStream = expected.OpenRead())
                {
                    using (FileStream acStream = actual.OpenRead())
                    {
                        AreEqual(exStream, acStream, message, args);
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
            public static void AreEqual(FileInfo expected, FileInfo actual, string message)
            {
                AreEqual(expected, actual, message, null);
            }

            /// <summary>
            /// Verifies that two files are equal.  Two files are considered
            /// equal if both are null, or if both have the same value byte for byte.
            /// If they are not equal an <see cref="EnsuranceException"/> is thrown.
            /// </summary>
            /// <param name="expected">A file containing the value that is expected</param>
            /// <param name="actual">A file containing the actual value</param>
            public static void AreEqual(FileInfo expected, FileInfo actual)
            {
                AreEqual(expected, actual, string.Empty, null);
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
            public static void AreEqual(string expected, string actual, string message, params object[] args)
            {
                using (FileStream exStream = File.OpenRead(expected))
                {
                    using (FileStream acStream = File.OpenRead(actual))
                    {
                        AreEqual(exStream, acStream, message, args);
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
            public static void AreEqual(string expected, string actual, string message)
            {
                AreEqual(expected, actual, message, null);
            }

            /// <summary>
            /// Verifies that two files are equal.  Two files are considered
            /// equal if both are null, or if both have the same value byte for byte.
            /// If they are not equal an <see cref="EnsuranceException"/> is thrown.
            /// </summary>
            /// <param name="expected">The path to a file containing the value that is expected</param>
            /// <param name="actual">The path to a file containing the actual value</param>
            public static void AreEqual(string expected, string actual)
            {
                AreEqual(expected, actual, string.Empty, null);
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
            public static void AreNotEqual(Stream expected, Stream actual, string message, params object[] args)
            {
                That(actual, new NotConstraint(new EqualConstraint(expected)), message, args);
            }

            /// <summary>
            /// Asserts that two Streams are not equal. If they are equal
            /// an <see cref="EnsuranceException"/> is thrown.
            /// </summary>
            /// <param name="expected">The expected Stream</param>
            /// <param name="actual">The actual Stream</param>
            /// <param name="message">The message to be displayed when the Streams are the same.</param>
            public static void AreNotEqual(Stream expected, Stream actual, string message)
            {
                AreNotEqual(expected, actual, message, null);
            }

            /// <summary>
            /// Asserts that two Streams are not equal. If they are equal
            /// an <see cref="EnsuranceException"/> is thrown.
            /// </summary>
            /// <param name="expected">The expected Stream</param>
            /// <param name="actual">The actual Stream</param>
            public static void AreNotEqual(Stream expected, Stream actual)
            {
                AreNotEqual(expected, actual, string.Empty, null);
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
            public static void AreNotEqual(FileInfo expected, FileInfo actual, string message, params object[] args)
            {
                using (FileStream exStream = expected.OpenRead())
                {
                    using (FileStream acStream = actual.OpenRead())
                    {
                        AreNotEqual(exStream, acStream, message, args);
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
            public static void AreNotEqual(FileInfo expected, FileInfo actual, string message)
            {
                AreNotEqual(expected, actual, message, null);
            }

            /// <summary>
            /// Asserts that two files are not equal. If they are equal
            /// an <see cref="EnsuranceException"/> is thrown.
            /// </summary>
            /// <param name="expected">A file containing the value that is expected</param>
            /// <param name="actual">A file containing the actual value</param>
            public static void AreNotEqual(FileInfo expected, FileInfo actual)
            {
                AreNotEqual(expected, actual, string.Empty, null);
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
            public static void AreNotEqual(string expected, string actual, string message, params object[] args)
            {
                using (FileStream exStream = File.OpenRead(expected))
                {
                    using (FileStream acStream = File.OpenRead(actual))
                    {
                        AreNotEqual(exStream, acStream, message, args);
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
            public static void AreNotEqual(string expected, string actual, string message)
            {
                AreNotEqual(expected, actual, message, null);
            }

            /// <summary>
            /// Asserts that two files are not equal. If they are equal
            /// an <see cref="EnsuranceException"/> is thrown.
            /// </summary>
            /// <param name="expected">The path to a file containing the value that is expected</param>
            /// <param name="actual">The path to a file containing the actual value</param>
            public static void AreNotEqual(string expected, string actual)
            {
                AreNotEqual(expected, actual, string.Empty, null);
            }

            #endregion

            #endregion
        }

        #endregion

        #region StringEnsure

        public class Strings
        {
            #region Contains

            /// <summary>
            /// Asserts that a string is found within another string.
            /// </summary>
            /// <param name="expected">The expected string</param>
            /// <param name="actual">The string to be examined</param>
            /// <param name="message">The message to display in case of failure</param>
            /// <param name="args">Arguments used in formatting the message</param>
            public static void Contains(string expected, string actual, string message, params object[] args)
            {
                That(actual, new SubstringConstraint(expected), message, args);
            }

            /// <summary>
            /// Asserts that a string is found within another string.
            /// </summary>
            /// <param name="expected">The expected string</param>
            /// <param name="actual">The string to be examined</param>
            /// <param name="message">The message to display in case of failure</param>
            public static void Contains(string expected, string actual, string message)
            {
                Contains(expected, actual, message, null);
            }

            /// <summary>
            /// Asserts that a string is found within another string.
            /// </summary>
            /// <param name="expected">The expected string</param>
            /// <param name="actual">The string to be examined</param>
            public static void Contains(string expected, string actual)
            {
                Contains(expected, actual, string.Empty, null);
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
            public static void StartsWith(string expected, string actual, string message, params object[] args)
            {
                That(actual, new StartsWithConstraint(expected), message, args);
            }

            /// <summary>
            /// Asserts that a string starts with another string.
            /// </summary>
            /// <param name="expected">The expected string</param>
            /// <param name="actual">The string to be examined</param>
            /// <param name="message">The message to display in case of failure</param>
            public static void StartsWith(string expected, string actual, string message)
            {
                StartsWith(expected, actual, message, null);
            }

            /// <summary>
            /// Asserts that a string starts with another string.
            /// </summary>
            /// <param name="expected">The expected string</param>
            /// <param name="actual">The string to be examined</param>
            public static void StartsWith(string expected, string actual)
            {
                StartsWith(expected, actual, string.Empty, null);
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
            public static void EndsWith(string expected, string actual, string message, params object[] args)
            {
                That(actual, new EndsWithConstraint(expected), message, args);
            }

            /// <summary>
            /// Asserts that a string ends with another string.
            /// </summary>
            /// <param name="expected">The expected string</param>
            /// <param name="actual">The string to be examined</param>
            /// <param name="message">The message to display in case of failure</param>
            public static void EndsWith(string expected, string actual, string message)
            {
                EndsWith(expected, actual, message, null);
            }

            /// <summary>
            /// Asserts that a string ends with another string.
            /// </summary>
            /// <param name="expected">The expected string</param>
            /// <param name="actual">The string to be examined</param>
            public static void EndsWith(string expected, string actual)
            {
                EndsWith(expected, actual, string.Empty, null);
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
            public static void AreEqualIgnoringCase(string expected, string actual, string message, params object[] args)
            {
                That(actual, new EqualConstraint(expected).IgnoreCase, message, args);
            }

            /// <summary>
            /// Asserts that two strings are equal, without regard to case.
            /// </summary>
            /// <param name="expected">The expected string</param>
            /// <param name="actual">The actual string</param>
            /// <param name="message">The message to display in case of failure</param>
            public static void AreEqualIgnoringCase(string expected, string actual, string message)
            {
                AreEqualIgnoringCase(expected, actual, message, null);
            }

            /// <summary>
            /// Asserts that two strings are equal, without regard to case.
            /// </summary>
            /// <param name="expected">The expected string</param>
            /// <param name="actual">The actual string</param>
            public static void AreEqualIgnoringCase(string expected, string actual)
            {
                AreEqualIgnoringCase(expected, actual, string.Empty, null);
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
            public static void IsMatch(string expected, string actual, string message, params object[] args)
            {
                That(actual, new RegexConstraint(expected), message, args);
            }

            /// <summary>
            /// Asserts that a string matches an expected regular expression pattern.
            /// </summary>
            /// <param name="expected">The expected expression</param>
            /// <param name="actual">The actual string</param>
            /// <param name="message">The message to display in case of failure</param>
            public static void IsMatch(string expected, string actual, string message)
            {
                IsMatch(expected, actual, message, null);
            }

            /// <summary>
            /// Asserts that a string matches an expected regular expression pattern.
            /// </summary>
            /// <param name="expected">The expected expression</param>
            /// <param name="actual">The actual string</param>
            public static void IsMatch(string expected, string actual)
            {
                IsMatch(expected, actual, string.Empty, null);
            }

            #endregion
        }
        
        #endregion

        #region CollectionEnsure

        #region AllItemsAreInstancesOfType

        /// <summary>
        /// Asserts that all items contained in collection are of the type specified by expectedType.
        /// </summary>
        /// <param name="collection">ICollection of objects to be considered</param>
        /// <param name="expectedType">System.Type that all objects in collection must be instances of</param>
        public static void AllItemsAreInstancesOfType(ICollection collection, Type expectedType)
        {
            AllItemsAreInstancesOfType(collection, expectedType, string.Empty, null);
        }

        /// <summary>
        /// Asserts that all items contained in collection are of the type specified by expectedType.
        /// </summary>
        /// <param name="collection">ICollection of objects to be considered</param>
        /// <param name="expectedType">System.Type that all objects in collection must be instances of</param>
        /// <param name="message">The message that will be displayed on failure</param>
        public static void AllItemsAreInstancesOfType(ICollection collection, Type expectedType, string message)
        {
            AllItemsAreInstancesOfType(collection, expectedType, message, null);
        }

        /// <summary>
        /// Asserts that all items contained in collection are of the type specified by expectedType.
        /// </summary>
        /// <param name="collection">ICollection of objects to be considered</param>
        /// <param name="expectedType">System.Type that all objects in collection must be instances of</param>
        /// <param name="message">The message that will be displayed on failure</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void AllItemsAreInstancesOfType(ICollection collection, Type expectedType, string message, params object[] args)
        {
            That(collection, new AllItemsConstraint(new InstanceOfTypeConstraint(expectedType)), message, args);
        }

        #endregion

        #region AllItemsAreNotNull

        /// <summary>
        /// Asserts that all items contained in collection are not equal to null.
        /// </summary>
        /// <param name="collection">ICollection of objects to be considered</param>
        public static void AllItemsAreNotNull(ICollection collection)
        {
            AllItemsAreNotNull(collection, string.Empty, null);
        }

        /// <summary>
        /// Asserts that all items contained in collection are not equal to null.
        /// </summary>
        /// <param name="collection">ICollection of objects to be considered</param>
        /// <param name="message">The message that will be displayed on failure</param>
        public static void AllItemsAreNotNull(ICollection collection, string message)
        {
            AllItemsAreNotNull(collection, message, null);
        }

        /// <summary>
        /// Asserts that all items contained in collection are not equal to null.
        /// </summary>
        /// <param name="collection">ICollection of objects to be considered</param>
        /// <param name="message">The message that will be displayed on failure</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void AllItemsAreNotNull(ICollection collection, string message, params object[] args)
        {
            That(collection, new AllItemsConstraint(new NotConstraint(new EqualConstraint(null))), message, args);
        }

        #endregion

        #region AllItemsAreUnique

        /// <summary>
        /// Ensures that every object contained in collection exists within the collection
        /// once and only once.
        /// </summary>
        /// <param name="collection">ICollection of objects to be considered</param>
        public static void AllItemsAreUnique(ICollection collection)
        {
            AllItemsAreUnique(collection, string.Empty, null);
        }

        /// <summary>
        /// Ensures that every object contained in collection exists within the collection
        /// once and only once.
        /// </summary>
        /// <param name="collection">ICollection of objects to be considered</param>
        /// <param name="message">The message that will be displayed on failure</param>
        public static void AllItemsAreUnique(ICollection collection, string message)
        {
            AllItemsAreUnique(collection, message, null);
        }

        /// <summary>
        /// Ensures that every object contained in collection exists within the collection
        /// once and only once.
        /// </summary>
        /// <param name="collection">ICollection of objects to be considered</param>
        /// <param name="message">The message that will be displayed on failure</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void AllItemsAreUnique(ICollection collection, string message, params object[] args)
        {
            That(collection, new UniqueItemsConstraint(), message, args);
        }

        #endregion

        #region AreEqual

        /// <summary>
        /// Asserts that expected and actual are exactly equal.  The collections must have the same count, 
        /// and contain the exact same objects in the same order.
        /// </summary>
        /// <param name="expected">The first ICollection of objects to be considered</param>
        /// <param name="actual">The second ICollection of objects to be considered</param>
        public static void AreEqual(ICollection expected, ICollection actual)
        {
            //AreEqual(expected, actual, null, string.Empty, null);
            That(actual, new EqualConstraint(expected));
        }

        /// <summary>
        /// Asserts that expected and actual are exactly equal.  The collections must have the same count, 
        /// and contain the exact same objects in the same order.
        /// If comparer is not null then it will be used to compare the objects.
        /// </summary>
        /// <param name="expected">The first ICollection of objects to be considered</param>
        /// <param name="actual">The second ICollection of objects to be considered</param>
        /// <param name="comparer">The IComparer to use in comparing objects from each ICollection</param>
        public static void AreEqual(ICollection expected, ICollection actual, IComparer comparer)
        {
            AreEqual(expected, actual, comparer, string.Empty, null);
        }

        /// <summary>
        /// Asserts that expected and actual are exactly equal.  The collections must have the same count, 
        /// and contain the exact same objects in the same order.
        /// </summary>
        /// <param name="expected">The first ICollection of objects to be considered</param>
        /// <param name="actual">The second ICollection of objects to be considered</param>
        /// <param name="message">The message that will be displayed on failure</param>
        public static void AreEqual(ICollection expected, ICollection actual, string message)
        {
            //AreEqual(expected, actual, null, message, null);
            That(actual, new EqualConstraint(expected), message);
        }

        /// <summary>
        /// Asserts that expected and actual are exactly equal.  The collections must have the same count, 
        /// and contain the exact same objects in the same order.
        /// If comparer is not null then it will be used to compare the objects.
        /// </summary>
        /// <param name="expected">The first ICollection of objects to be considered</param>
        /// <param name="actual">The second ICollection of objects to be considered</param>
        /// <param name="comparer">The IComparer to use in comparing objects from each ICollection</param>
        /// <param name="message">The message that will be displayed on failure</param>
        public static void AreEqual(ICollection expected, ICollection actual, IComparer comparer, string message)
        {
            AreEqual(expected, actual, comparer, message, null);
        }

        /// <summary>
        /// Asserts that expected and actual are exactly equal.  The collections must have the same count, 
        /// and contain the exact same objects in the same order.
        /// </summary>
        /// <param name="expected">The first ICollection of objects to be considered</param>
        /// <param name="actual">The second ICollection of objects to be considered</param>
        /// <param name="message">The message that will be displayed on failure</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void AreEqual(ICollection expected, ICollection actual, string message, params object[] args)
        {
            //AreEqual(expected, actual, null, message, args);
            That(actual, new EqualConstraint(expected), message, args);
        }

        /// <summary>
        /// Asserts that expected and actual are exactly equal.  The collections must have the same count, 
        /// and contain the exact same objects in the same order.
        /// If comparer is not null then it will be used to compare the objects.
        /// </summary>
        /// <param name="expected">The first ICollection of objects to be considered</param>
        /// <param name="actual">The second ICollection of objects to be considered</param>
        /// <param name="comparer">The IComparer to use in comparing objects from each ICollection</param>
        /// <param name="message">The message that will be displayed on failure</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void AreEqual(ICollection expected, ICollection actual, IComparer comparer, string message, params object[] args)
        {
            That(actual, new EqualConstraint(expected).Comparer(comparer), message, args);
        }

        #endregion

        #region AreEquivalent

        /// <summary>
        /// Asserts that expected and actual are equivalent, containing the same objects but the match may be in any order.
        /// </summary>
        /// <param name="expected">The first ICollection of objects to be considered</param>
        /// <param name="actual">The second ICollection of objects to be considered</param>
        public static void AreEquivalent(ICollection expected, ICollection actual)
        {
            AreEquivalent(expected, actual, string.Empty, null);
        }

        /// <summary>
        /// Asserts that expected and actual are equivalent, containing the same objects but the match may be in any order.
        /// </summary>
        /// <param name="expected">The first ICollection of objects to be considered</param>
        /// <param name="actual">The second ICollection of objects to be considered</param>
        /// <param name="message">The message that will be displayed on failure</param>
        public static void AreEquivalent(ICollection expected, ICollection actual, string message)
        {
            AreEquivalent(expected, actual, message, null);
        }

        /// <summary>
        /// Asserts that expected and actual are equivalent, containing the same objects but the match may be in any order.
        /// </summary>
        /// <param name="expected">The first ICollection of objects to be considered</param>
        /// <param name="actual">The second ICollection of objects to be considered</param>
        /// <param name="message">The message that will be displayed on failure</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void AreEquivalent(ICollection expected, ICollection actual, string message, params object[] args)
        {
            That(actual, new CollectionEquivalentConstraint(expected), message, args);
        }

        #endregion

        #region AreNotEqual

        /// <summary>
        /// Asserts that expected and actual are not exactly equal.
        /// </summary>
        /// <param name="expected">The first ICollection of objects to be considered</param>
        /// <param name="actual">The second ICollection of objects to be considered</param>
        public static void AreNotEqual(ICollection expected, ICollection actual)
        {
            That(actual, new NotConstraint(new EqualConstraint(expected)));
        }

        /// <summary>
        /// Asserts that expected and actual are not exactly equal.
        /// If comparer is not null then it will be used to compare the objects.
        /// </summary>
        /// <param name="expected">The first ICollection of objects to be considered</param>
        /// <param name="actual">The second ICollection of objects to be considered</param>
        /// <param name="comparer">The IComparer to use in comparing objects from each ICollection</param>
        public static void AreNotEqual(ICollection expected, ICollection actual, IComparer comparer)
        {
            AreNotEqual(expected, actual, comparer, string.Empty, null);
        }

        /// <summary>
        /// Asserts that expected and actual are not exactly equal.
        /// </summary>
        /// <param name="expected">The first ICollection of objects to be considered</param>
        /// <param name="actual">The second ICollection of objects to be considered</param>
        /// <param name="message">The message that will be displayed on failure</param>
        public static void AreNotEqual(ICollection expected, ICollection actual, string message)
        {
            //AreNotEqual(expected, actual, null, message, null);
            //EnsureBase<T>.AreNotEqual( expected, actual, message );
            That(actual, new NotConstraint(new EqualConstraint(expected)), message);
        }

        /// <summary>
        /// Asserts that expected and actual are not exactly equal.
        /// If comparer is not null then it will be used to compare the objects.
        /// </summary>
        /// <param name="expected">The first ICollection of objects to be considered</param>
        /// <param name="actual">The second ICollection of objects to be considered</param>
        /// <param name="comparer">The IComparer to use in comparing objects from each ICollection</param>
        /// <param name="message">The message that will be displayed on failure</param>
        public static void AreNotEqual(ICollection expected, ICollection actual, IComparer comparer, string message)
        {
            AreNotEqual(expected, actual, comparer, message, null);
        }

        /// <summary>
        /// Asserts that expected and actual are not exactly equal.
        /// </summary>
        /// <param name="expected">The first ICollection of objects to be considered</param>
        /// <param name="actual">The second ICollection of objects to be considered</param>
        /// <param name="message">The message that will be displayed on failure</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void AreNotEqual(ICollection expected, ICollection actual, string message, params object[] args)
        {
            //AreNotEqual(expected, actual, null, message, args);
            //EnsureBase<T>.AreNotEqual( expected, actual, message, args );
            That(actual, new NotConstraint(new EqualConstraint(expected)), message, args);
        }

        /// <summary>
        /// Asserts that expected and actual are not exactly equal.
        /// If comparer is not null then it will be used to compare the objects.
        /// </summary>
        /// <param name="expected">The first ICollection of objects to be considered</param>
        /// <param name="actual">The second ICollection of objects to be considered</param>
        /// <param name="comparer">The IComparer to use in comparing objects from each ICollection</param>
        /// <param name="message">The message that will be displayed on failure</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void AreNotEqual(ICollection expected, ICollection actual, IComparer comparer, string message, params object[] args)
        {
            That(actual, new NotConstraint(new EqualConstraint(expected).Comparer(comparer)), message, args);
        }

        #endregion

        #region AreNotEquivalent

        /// <summary>
        /// Asserts that expected and actual are not equivalent.
        /// </summary>
        /// <param name="expected">The first ICollection of objects to be considered</param>
        /// <param name="actual">The second ICollection of objects to be considered</param>
        public static void AreNotEquivalent(ICollection expected, ICollection actual)
        {
            AreNotEquivalent(expected, actual, string.Empty, null);
        }

        /// <summary>
        /// Asserts that expected and actual are not equivalent.
        /// </summary>
        /// <param name="expected">The first ICollection of objects to be considered</param>
        /// <param name="actual">The second ICollection of objects to be considered</param>
        /// <param name="message">The message that will be displayed on failure</param>
        public static void AreNotEquivalent(ICollection expected, ICollection actual, string message)
        {
            AreNotEquivalent(expected, actual, message, null);
        }

        /// <summary>
        /// Asserts that expected and actual are not equivalent.
        /// </summary>
        /// <param name="expected">The first ICollection of objects to be considered</param>
        /// <param name="actual">The second ICollection of objects to be considered</param>
        /// <param name="message">The message that will be displayed on failure</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void AreNotEquivalent(ICollection expected, ICollection actual, string message, params object[] args)
        {
            That(actual, new NotConstraint(new CollectionEquivalentConstraint(expected)), message, args);
        }

        #endregion

        #region DoesNotContain

        /// <summary>
        /// Asserts that collection does not contain actual as an item.
        /// </summary>
        /// <param name="collection">ICollection of objects to be considered</param>
        /// <param name="actual">Object that cannot exist within collection</param>
        public static void DoesNotContain(ICollection collection, Object actual)
        {
            DoesNotContain(collection, actual, string.Empty, null);
        }

        /// <summary>
        /// Asserts that collection does not contain actual as an item.
        /// </summary>
        /// <param name="collection">ICollection of objects to be considered</param>
        /// <param name="actual">Object that cannot exist within collection</param>
        /// <param name="message">The message that will be displayed on failure</param>
        public static void DoesNotContain(ICollection collection, Object actual, string message)
        {
            DoesNotContain(collection, actual, message, null);
        }

        /// <summary>
        /// Asserts that collection does not contain actual as an item.
        /// </summary>
        /// <param name="collection">ICollection of objects to be considered</param>
        /// <param name="actual">Object that cannot exist within collection</param>
        /// <param name="message">The message that will be displayed on failure</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void DoesNotContain(ICollection collection, Object actual, string message, params object[] args)
        {
            That(collection, new NotConstraint(new CollectionContainsConstraint(actual)), message, args);
        }

        #endregion

        #region IsNotSubsetOf

        /// <summary>
        /// Asserts that superset is not a subject of subset.
        /// </summary>
        /// <param name="subset">The ICollection superset to be considered</param>
        /// <param name="superset">The ICollection subset to be considered</param>
        public static void IsNotSubsetOf(ICollection subset, ICollection superset)
        {
            IsNotSubsetOf(subset, superset, string.Empty, null);
        }

        /// <summary>
        /// Asserts that superset is not a subject of subset.
        /// </summary>
        /// <param name="subset">The ICollection superset to be considered</param>
        /// <param name="superset">The ICollection subset to be considered</param>
        /// <param name="message">The message that will be displayed on failure</param>
        public static void IsNotSubsetOf(ICollection subset, ICollection superset, string message)
        {
            IsNotSubsetOf(subset, superset, message, null);
        }

        /// <summary>
        /// Asserts that superset is not a subject of subset.
        /// </summary>
        /// <param name="subset">The ICollection superset to be considered</param>
        /// <param name="superset">The ICollection subset to be considered</param>
        /// <param name="message">The message that will be displayed on failure</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void IsNotSubsetOf(ICollection subset, ICollection superset, string message, params object[] args)
        {
            That(subset, new NotConstraint(new CollectionSubsetConstraint(superset)), message, args);
        }

        #endregion

        #region IsSubsetOf

        /// <summary>
        /// Asserts that superset is a subset of subset.
        /// </summary>
        /// <param name="subset">The ICollection superset to be considered</param>
        /// <param name="superset">The ICollection subset to be considered</param>
        public static void IsSubsetOf(ICollection subset, ICollection superset)
        {
            IsSubsetOf(subset, superset, string.Empty, null);
        }

        /// <summary>
        /// Asserts that superset is a subset of subset.
        /// </summary>
        /// <param name="subset">The ICollection superset to be considered</param>
        /// <param name="superset">The ICollection subset to be considered</param>
        /// <param name="message">The message that will be displayed on failure</param>
        public static void IsSubsetOf(ICollection subset, ICollection superset, string message)
        {
            IsSubsetOf(subset, superset, message, null);
        }

        /// <summary>
        /// Asserts that superset is a subset of subset.
        /// </summary>
        /// <param name="subset">The ICollection superset to be considered</param>
        /// <param name="superset">The ICollection subset to be considered</param>
        /// <param name="message">The message that will be displayed on failure</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void IsSubsetOf(ICollection subset, ICollection superset, string message, params object[] args)
        {
            That(subset, new CollectionSubsetConstraint(superset), message, args);
        }

        #endregion

        #endregion

        #region Predicates

        #region IsTrue

        /// <summary>
        /// Asserts that a predicate is true. If the predicate is false the method throws
        /// an <see cref="EnsuranceException"/>.
        /// </summary>
        /// <param name="predicate">The condition to be evaluated.</param>
        /// <param name="input">The input for the predicate.</param>
        /// <param name="message">The message to display if the predicate is false</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        static public void IsTrue<TInput>( Predicate<TInput> predicate, TInput input, string message, params object[] args)
        {
            That(predicate, input, message, args);
        }

        /// <summary>
        /// Asserts that a condition is true. If the condition is false the method throws
        /// an <see cref="EnsuranceException"/>.
        /// </summary>
        /// <param name="predicate">The condition to be evaluated.</param>
        /// <param name="input">The input for the predicate.</param>
        /// <param name="message">The message to display if the condition is false</param>
        static public void IsTrue<TInput>( Predicate<TInput> predicate, TInput input, string message)
        {
            That(predicate, input, message, null);
        }

        /// <summary>
        /// Asserts that a condition is true. If the condition is false the method throws
        /// an <see cref="EnsuranceException"/>.
        /// </summary>
        /// <param name="predicate">The condition to be evaluated.</param>
        /// <param name="input">The input for the predicate.</param>
        static public void IsTrue<TInput>( Predicate<TInput> predicate, TInput input )
        {
            That(predicate, input, string.Empty, null);
        }

        #endregion

        #region IsFalse

        /// <summary>
        /// Asserts that a condition is false. If the condition is true the method throws
        /// an <see cref="EnsuranceException"/>.
        /// </summary>
        /// <param name="predicate">The condition to be evaluated.</param>
        /// <param name="input">The input for the predicate.</param>
        /// <param name="message">The message to display if the condition is true</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        static public void IsFalse<TInput>( Predicate<TInput> predicate, TInput input, string message, params object[] args)
        {
            if ( predicate == null )
            {
                throw new ArgumentNullException( "predicate", "The predicate must not be null" );
            }
            That( predicate( input ), Is.False, message, args );
        }

        /// <summary>
        /// Asserts that a condition is false. If the condition is true the method throws
        /// an <see cref="EnsuranceException"/>.
        /// </summary>
        /// <param name="predicate">The condition to be evaluated.</param>
        /// <param name="input">The input for the predicate.</param>
        /// <param name="message">The message to display if the condition is true</param>
        static public void IsFalse<TInput>( Predicate<TInput> predicate, TInput input, string message)
        {
            IsFalse( predicate, input, message, null );
        }

        /// <summary>
        /// Asserts that a condition is false. If the condition is true the method throws
        /// an <see cref="EnsuranceException"/>.
        /// </summary>
        /// <param name="predicate">The condition to be evaluated.</param>
        /// <param name="input">The input for the predicate.</param>
        static public void IsFalse<TInput>( Predicate<TInput> predicate, TInput input )
        {
            IsFalse( predicate, input, string.Empty, null );
        }

        #endregion

        #region That

        /// <summary>
        /// Asserts that a condition is true. If the condition is false the method throws
        /// an <see cref="EnsuranceException"/>.
        /// </summary> 
        /// <param name="predicate">The condition to be evaluated.</param>
        /// <param name="input">The input for the predicate.</param>
        /// <param name="message">The message to display if the condition is false</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void That<TInput>( Predicate<TInput> predicate, TInput input, string message, params object[] args)
        {
            if ( predicate == null )
            {
                throw new ArgumentNullException( "predicate", "The predicate must not be null" );
            }
            That(predicate(input), message, args);
        }

        /// <summary>
        /// Asserts that a condition is true. If the condition is false the method throws
        /// an <see cref="EnsuranceException"/>.
        /// </summary>
        /// <param name="predicate">The condition to be evaluated.</param>
        /// <param name="message">The message to display if the condition is false</param>
        /// <param name="input">The input for the predicate.</param>
        public static void That<TInput>( Predicate<TInput> predicate, TInput input, string message)
        {
            That(predicate, input, message, null);
        }

        /// <summary>
        /// Asserts that a condition is true. If the condition is false the method throws
        /// an <see cref="EnsuranceException"/>.
        /// </summary>
        /// <param name="predicate">The condition to be evaluated.</param>
        /// <param name="input">The input for the predicate.</param>
        public static void That<TInput>( Predicate<TInput> predicate, TInput input )
        {
            That(predicate, input, string.Empty, null);
        }

        #endregion

        #endregion
    }
}