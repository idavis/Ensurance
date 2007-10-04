// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org/?p=license&r=2.4
// ****************************************************************

using System.Collections;
using Ensurance.Constraints;
using Ensurance.SyntaxHelpers;

namespace Ensurance
{
    /// <summary>
    /// EnsuranceHelper is an optional base class for user tests,
    /// allowing the use of shorter names for constraints and
    /// asserts and avoiding conflict with the definition of 
    /// <see cref="Is"/>, from which it inherits much of its
    /// behavior, in certain mock object frameworks.
    /// </summary>
    public class EnsuranceHelper<T> : ConstraintBuilder where T : IEnsuranceHandler
    {
        #region Expect

        /// <summary>
        /// Apply a constraint to an actual value, succeeding if the constraint
        /// is satisfied and throwing an assertion exception on failure. Works
        /// identically to <see cref="Ensurance.EnsureBase<T>.That(object, Constraint)"/>
        /// </summary>
        /// <param name="constraint">A Constraint to be applied</param>
        /// <param name="actual">The actual value to test</param>
        public static void Expect( object actual, Constraint constraint )
        {
            EnsureBase<T>.That( actual, constraint, null, null );
        }

        /// <summary>
        /// Apply a constraint to an actual value, succeeding if the constraint
        /// is satisfied and throwing an assertion exception on failure. Works
        /// identically to <see cref="Ensurance.EnsureBase<T>.That(object, Constraint, string)"/>
        /// </summary>
        /// <param name="constraint">A Constraint to be applied</param>
        /// <param name="actual">The actual value to test</param>
        /// <param name="message">The message that will be displayed on failure</param>
        public static void Expect( object actual, Constraint constraint, string message )
        {
            EnsureBase<T>.That( actual, constraint, message, null );
        }

        /// <summary>
        /// Apply a constraint to an actual value, succeeding if the constraint
        /// is satisfied and throwing an assertion exception on failure. Works
        /// identically to <see cref="Ensurance.EnsureBase<T>.That(object, Constraint, string, object[])"/>
        /// </summary>
        /// <param name="constraint">A Constraint to be applied</param>
        /// <param name="actual">The actual value to test</param>
        /// <param name="message">The message that will be displayed on failure</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void Expect( object actual, Constraint constraint, string message, params object[] args )
        {
            EnsureBase<T>.That( actual, constraint, message, args );
        }

        /// <summary>
        /// Asserts that a condition is true. If the condition is false the method throws
        /// an <see cref="EnsuranceException"/>. Works Identically to 
        /// <see cref="EnsureBase<T>.That(bool, string, object[])"/>.
        /// </summary> 
        /// <param name="condition">The evaluated condition</param>
        /// <param name="message">The message to display if the condition is false</param>
        /// <param name="args">Arguments to be used in formatting the message</param>
        public static void Expect( bool condition, string message, params object[] args )
        {
            EnsureBase<T>.That( condition, Is.True, message, args );
        }

        /// <summary>
        /// Asserts that a condition is true. If the condition is false the method throws
        /// an <see cref="EnsuranceException"/>. Works Identically to 
        /// <see cref="EnsureBase<T>.That(bool, string)"/>.
        /// </summary>
        /// <param name="condition">The evaluated condition</param>
        /// <param name="message">The message to display if the condition is false</param>
        public static void Expect( bool condition, string message )
        {
            EnsureBase<T>.That( condition, Is.True, message, null );
        }

        /// <summary>
        /// Asserts that a condition is true. If the condition is false the method throws
        /// an <see cref="EnsuranceException"/>. Works Identically to <see cref="EnsureBase<T>.That(bool)"/>.
        /// </summary>
        /// <param name="condition">The evaluated condition</param>
        public static void Expect( bool condition )
        {
            EnsureBase<T>.That( condition, Is.True, null, null );
        }

        #endregion

        #region Map

        /// <summary>
        /// Returns a ListMapper based on a collection.
        /// </summary>
        /// <param name="original">The original collection</param>
        /// <returns></returns>
        public ListMapper Map( ICollection original )
        {
            return new ListMapper( original );
        }

        #endregion
    }
}