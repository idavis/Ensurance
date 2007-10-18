using System;
using System.Collections;
using System.IO;
using Ensurance.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ensurance.Tests.Attributes
{
    /// <summary>
    /// Summary description for IsAttributeTests
    /// </summary>
    [TestClass]
    public class IsAttributeTests
    {
        #region Additional test attributes

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion

        #region Tests

        [TestMethod]
        public void AllAttributesPass()
        {
            IsAssignableFrom( new int[] {3, 4} );
            IsEmpty( new ArrayList() );
            IsFalse( false );
            IsInstanceOfType( new Is.Null() );
            IsNaN( double.NaN );
            IsNaN( float.NaN );
            IsNull( null );
            IsTrue( true );
            IsTypeOf( new StringReader( "" ) );
            IsUnique( new int[] {1, 3, 5, 7, 9} );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void IsAssignableFromFails()
        {
            IsAssignableFrom( new double[] {3, 4} );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void IsEmptyFails()
        {
            IsEmpty( new int[] {1, 3, 5, 7, 9} );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void IsFalseFails()
        {
            IsFalse( true );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void IsInstanceOfTypeFails()
        {
            IsInstanceOfType( new object() );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void IsNaNFails()
        {
            IsNaN( -1 );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void IsNullFails()
        {
            IsNull( "NotNull" );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void IsTrueFails()
        {
            IsTrue( false );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void IsTypeOfFails()
        {
            IsTypeOf( "NotTextReader" );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void IsUniqueFails()
        {
            IsUnique( new int[] {1, 3, 5, 7, 9, 1} );
        }

        [TestMethod]
        public void FilledArrayPassesAsNotNullAndNotEmpty()
        {
            IsNotNullAndIsNotEmpty( new int[] {1, 3, 5, 7, 9, 1} );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void FilledArrayFailAsNotNullAndNotEmptyButFailsNotUnique()
        {
            IsNotNullAndIsNotEmptyAndIsUnique( new int[] {1, 3, 5, 7, 9, 1} );
        }

        [TestMethod]
        public void FilledArraysPassIsNotNullAndIsNotEmptyFollowedByIsNotUnique()
        {
            IsNotNullAndIsNotEmptyFollowedByIsNotUnique( new int[] {1, 3, 5, 7, 9, 1}, new int[] {1, 3, 5, 7, 9, 1} );
            IsNotNullAndIsNotEmptyFollowedByIsNotUnique( new int[] {1, 3, 5, 7, 9, 1}, new double[] {1.3, 3.2, 1.3} );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void FilledArrayFailIsNotNullAndIsNotEmptyAndIsUniqueFollowedByIsNotUnique()
        {
            IsNotNullAndIsNotEmptyFollowedByIsNotUnique( new int[] {1, 3, 5, 7, 9, 1}, new int[] {1, 3, 5, 7, 9} );
        }

        #endregion Tests

        #region Method Calls With Attributes

        #region Multiple Attributes

        public static void IsNotNullAndIsNotEmpty( [Is.Not.Null, Is.Not.Empty] ICollection value )
        {
            Ensure.VerifyArguments( value );
        }

        public static void IsNotNullAndIsNotEmptyAndIsUnique( [Is.Not.Null, Is.Not.Empty, Is.Unique] ICollection value )
        {
            Ensure.VerifyArguments( value );
        }

        #endregion

        #region Multiple Parameters

        public static void IsNotNullAndIsNotEmptyFollowedByIsNotUnique( [Is.Not.Null, Is.Not.Empty] ICollection value1, [Is.Not.Unique] ICollection value2 )
        {
            Ensure.VerifyArguments( value1, value2 );
        }

        #endregion

        public static void IsAssignableFrom( [Is.AssignableFrom( typeof (int[]) )] object value )
        {
            Ensure.VerifyArguments( value );
        }

        //public static void IsAtLeast([Is.AtLeast()] int value)
        //{
//           Ensure.VerifyArguments( value );
        //}

        //public static void IsAtMost([Is.AtMost()] int value)
        //{
        //Ensure.VerifyArguments( value );
        //}

        public static void IsEmpty( [Is.Empty] ICollection value )
        {
            Ensure.VerifyArguments( value );
        }

        public static void IsFalse( [Is.False] bool value )
        {
            Ensure.VerifyArguments( value );
        }

        //public static void IsGreaterThan( [Is.GreaterThan( 0 )] int value )
        //{
        //    Ensure.VerifyArguments(value);
        //}

        //public static void IsGreaterThanOrEqualTo( [Is.GreaterThanOrEqualTo( 0 )] int value )
        //{
        //    Ensure.VerifyArguments(value);
        //}

        public static void IsInstanceOfType( [Is.InstanceOfType( typeof (Attribute) )] object value )
        {
            Ensure.VerifyArguments( value );
        }

        //public static void IsLessThan( [Is.LessThan( 0 )] int value )
        //{
        //    Ensure.VerifyArguments(value);
        //}

        //public static void IsLessThanOrEqualTo( [Is.LessThanOrEqualTo( 0 )] int value )
        //{
        //    Ensure.VerifyArguments(value);
        //}

        public static void IsNaN( [Is.NaN] object value )
        {
            Ensure.VerifyArguments( value );
        }

        public static void IsNull( [Is.Null] string value )
        {
            Ensure.VerifyArguments( value );
        }

        public static void IsTrue( [Is.True] bool value )
        {
            Ensure.VerifyArguments( value );
        }

        public static void IsTypeOf( [Is.TypeOf( typeof (StringReader) )] object value )
        {
            Ensure.VerifyArguments( value );
        }

        public static void IsUnique( [Is.Unique] ICollection value )
        {
            Ensure.VerifyArguments( value );
        }

        #endregion Method Calls With Attributes
    }
}