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

using System.Collections;
using Ensurance.Constraints;
using Ensurance.SyntaxHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ensurance.Tests
{
    /// <summary>
    /// This test fixture attempts to exercise all the syntactic
    /// variations of Ensure without getting into failures, errors 
    /// or corner cases. Thus, some of the tests may be duplicated 
    /// in other fixtures.
    /// 
    /// Each test performs the same operations using the classic
    /// syntax (if available) and the new syntax in both the
    /// helper-based and inherited forms.
    /// 
    /// This Fixture will eventually be duplicated in other
    /// supported languages. 
    /// </summary>
    [TestClass]
    public class AssertSyntaxTests : ConstraintBuilder
    {
        #region Simple Constraint Tests

        [TestMethod]
        public void IsNull()
        {
            // Classic syntax
            Ensure.IsNull( null );

            // Helper syntax
            Ensure.That( null, Is.Null );

            // Inherited syntax
            EnsuranceHelper.Expect( null, Null );
        }

        [TestMethod]
        public void IsNotNull()
        {
            // Classic syntax
            Ensure.IsNotNull( 42 );

            // Helper syntax
            Ensure.That( 42, Is.Not.Null );

            // Inherited syntax
            EnsuranceHelper.Expect( 42, Not.Null );
        }

        [TestMethod]
        public void IsTrue()
        {
            // Classic syntax
            Ensure.IsTrue( 2 + 2 == 4 );

            // Helper syntax
            Ensure.That( 2 + 2 == 4, Is.True );
            Ensure.That( 2 + 2 == 4 );

            // Inherited syntax
            EnsuranceHelper.Expect( 2 + 2 == 4, True );
            EnsuranceHelper.Expect( 2 + 2 == 4 );
        }

        [TestMethod]
        public void IsFalse()
        {
            // Classic syntax
            Ensure.IsFalse( 2 + 2 == 5 );

            // Helper syntax
            Ensure.That( 2 + 2 == 5, Is.False );

            // Inherited syntax
            EnsuranceHelper.Expect( 2 + 2 == 5, False );
        }

        [TestMethod]
        public void IsNaN()
        {
            double d = double.NaN;
            float f = float.NaN;

            // Classic syntax
            Ensure.IsNaN( d );
            Ensure.IsNaN( f );

            // Helper syntax
            Ensure.That( d, Is.NaN );
            Ensure.That( f, Is.NaN );

            // Inherited syntax
            EnsuranceHelper.Expect( d, NaN );
            EnsuranceHelper.Expect( f, NaN );
        }

        [TestMethod]
        public void EmptyStringTests()
        {
            // Classic syntax
            Ensure.IsEmpty( "" );
            Ensure.IsNotEmpty( "Hello!" );

            // Helper syntax
            Ensure.That( "", Is.Empty );
            Ensure.That( "Hello!", Is.Not.Empty );

            // Inherited syntax
            EnsuranceHelper.Expect( "", Empty );
            EnsuranceHelper.Expect( "Hello!", Not.Empty );
        }

        [TestMethod]
        public void EmptyCollectionTests()
        {
            // Classic syntax
            Ensure.IsEmpty( new bool[0] );
            Ensure.IsNotEmpty( new int[] {1, 2, 3} );

            // Helper syntax
            Ensure.That( new bool[0], Is.Empty );
            Ensure.That( new int[] {1, 2, 3}, Is.Not.Empty );

            // Inherited syntax
            EnsuranceHelper.Expect( new bool[0], Empty );
            EnsuranceHelper.Expect( new int[] {1, 2, 3}, Not.Empty );
        }

        #endregion

        #region TypeConstraint Tests

        [TestMethod]
        public void ExactTypeTests()
        {
            // Classic syntax workarounds
            Ensure.AreEqual( typeof (string), "Hello".GetType() );
            Ensure.AreEqual( "System.String", "Hello".GetType().FullName );
            Ensure.AreNotEqual( typeof (int), "Hello".GetType() );
            Ensure.AreNotEqual( "System.Int32", "Hello".GetType().FullName );

            // Helper syntax
            Ensure.That( "Hello", Is.TypeOf( typeof (string) ) );
            Ensure.That( "Hello", Is.Not.TypeOf( typeof (int) ) );

            // Inherited syntax
            EnsuranceHelper.Expect( "Hello", TypeOf( typeof (string) ) );
            EnsuranceHelper.Expect( "Hello", Not.TypeOf( typeof (int) ) );
        }

        [TestMethod]
        public void InstanceOfTypeTests()
        {
            // Classic syntax
            Ensure.IsInstanceOfType( typeof (string), "Hello" );
            Ensure.IsNotInstanceOfType( typeof (string), 5 );

            // Helper syntax
            Ensure.That( "Hello", Is.InstanceOfType( typeof (string) ) );
            Ensure.That( 5, Is.Not.InstanceOfType( typeof (string) ) );

            // Inherited syntax
            EnsuranceHelper.Expect( "Hello", InstanceOfType( typeof (string) ) );
            EnsuranceHelper.Expect( 5, Not.InstanceOfType( typeof (string) ) );
        }

        [TestMethod]
        public void AssignableFromTypeTests()
        {
            // Classic syntax
            Ensure.IsAssignableFrom( typeof (string), "Hello" );
            Ensure.IsNotAssignableFrom( typeof (string), 5 );

            // Helper syntax
            Ensure.That( "Hello", Is.AssignableFrom( typeof (string) ) );
            Ensure.That( 5, Is.Not.AssignableFrom( typeof (string) ) );

            // Inherited syntax
            EnsuranceHelper.Expect( "Hello", AssignableFrom( typeof (string) ) );
            EnsuranceHelper.Expect( 5, Not.AssignableFrom( typeof (string) ) );
        }

        #endregion

        #region StringConstraint Tests

        [TestMethod]
        public void SubstringTests()
        {
            string phrase = "Hello World!";
            string[] array = new string[] {"abc", "bad", "dba"};

            // Classic Syntax
            EnsureBase<Ensure>.Strings.Contains( "World", phrase );

            // Helper syntax
            Ensure.That( phrase, Text.Contains( "World" ) );
            // Only available using new syntax
            Ensure.That( phrase, Text.DoesNotContain( "goodbye" ) );
            Ensure.That( phrase, Text.Contains( "WORLD" ).IgnoreCase );
            Ensure.That( phrase, Text.DoesNotContain( "BYE" ).IgnoreCase );
            Ensure.That( array, Text.All.Contains( "b" ) );

            // Inherited syntax
            EnsuranceHelper.Expect( phrase, Contains( "World" ) );
            // Only available using new syntax
            EnsuranceHelper.Expect( phrase, Not.Contains( "goodbye" ) );
            EnsuranceHelper.Expect( phrase, Contains( "WORLD" ).IgnoreCase );
            EnsuranceHelper.Expect( phrase, Not.Contains( "BYE" ).IgnoreCase );
            EnsuranceHelper.Expect( array, All.Contains( "b" ) );
        }

        [TestMethod]
        public void StartsWithTests()
        {
            string phrase = "Hello World!";
            string[] greetings = new string[] {"Hello!", "Hi!", "Hola!"};

            // Classic syntax
            EnsureBase<Ensure>.Strings.StartsWith( "Hello", phrase );

            // Helper syntax
            Ensure.That( phrase, Text.StartsWith( "Hello" ) );
            // Only available using new syntax
            Ensure.That( phrase, Text.DoesNotStartWith( "Hi!" ) );
            Ensure.That( phrase, Text.StartsWith( "HeLLo" ).IgnoreCase );
            Ensure.That( phrase, Text.DoesNotStartWith( "HI" ).IgnoreCase );
            Ensure.That( greetings, Text.All.StartsWith( "h" ).IgnoreCase );

            // Inherited syntax
            EnsuranceHelper.Expect( phrase, StartsWith( "Hello" ) );
            // Only available using new syntax
            EnsuranceHelper.Expect( phrase, Not.StartsWith( "Hi!" ) );
            EnsuranceHelper.Expect( phrase, StartsWith( "HeLLo" ).IgnoreCase );
            EnsuranceHelper.Expect( phrase, Not.StartsWith( "HI" ).IgnoreCase );
            EnsuranceHelper.Expect( greetings, All.StartsWith( "h" ).IgnoreCase );
        }

        [TestMethod]
        public void EndsWithTests()
        {
            string phrase = "Hello World!";
            string[] greetings = new string[] {"Hello!", "Hi!", "Hola!"};

            // Classic Syntax
            EnsureBase<Ensure>.Strings.EndsWith( "!", phrase );

            // Helper syntax
            Ensure.That( phrase, Text.EndsWith( "!" ) );
            // Only available using new syntax
            Ensure.That( phrase, Text.DoesNotEndWith( "?" ) );
            Ensure.That( phrase, Text.EndsWith( "WORLD!" ).IgnoreCase );
            Ensure.That( greetings, Text.All.EndsWith( "!" ) );

            // Inherited syntax
            EnsuranceHelper.Expect( phrase, EndsWith( "!" ) );
            // Only available using new syntax
            EnsuranceHelper.Expect( phrase, Not.EndsWith( "?" ) );
            EnsuranceHelper.Expect( phrase, EndsWith( "WORLD!" ).IgnoreCase );
            EnsuranceHelper.Expect( greetings, All.EndsWith( "!" ) );
        }

        [TestMethod]
        public void EqualIgnoringCaseTests()
        {
            string phrase = "Hello World!";

            // Classic syntax
            EnsureBase<Ensure>.Strings.AreEqualIgnoringCase( "hello world!", phrase );

            // Helper syntax
            Ensure.That( phrase, Is.EqualTo( "hello world!" ).IgnoreCase );
            //Only available using new syntax
            Ensure.That( phrase, Is.Not.EqualTo( "goodbye world!" ).IgnoreCase );
            Ensure.That( new string[] {"Hello", "World"},
                         Is.EqualTo( new object[] {"HELLO", "WORLD"} ).IgnoreCase );
            Ensure.That( new string[] {"HELLO", "Hello", "hello"},
                         Is.All.EqualTo( "hello" ).IgnoreCase );

            // Inherited syntax
            EnsuranceHelper.Expect( phrase, EqualTo( "hello world!" ).IgnoreCase );
            //Only available using new syntax
            EnsuranceHelper.Expect( phrase, Not.EqualTo( "goodbye world!" ).IgnoreCase );
            EnsuranceHelper.Expect( new string[] {"Hello", "World"},
                                    EqualTo( new object[] {"HELLO", "WORLD"} ).IgnoreCase );
            EnsuranceHelper.Expect( new string[] {"HELLO", "Hello", "hello"},
                                    All.EqualTo( "hello" ).IgnoreCase );
        }

        [TestMethod]
        public void RegularExpressionTests()
        {
            string phrase = "Now is the time for all good men to come to the aid of their country.";
            string[] quotes = new string[] {"Never say never", "It's never too late", "Nevermore!"};

            // Classic syntax
            EnsureBase<Ensure>.Strings.IsMatch( "all good men", phrase );
            EnsureBase<Ensure>.Strings.IsMatch( "Now.*come", phrase );

            // Helper syntax
            Ensure.That( phrase, Text.Matches( "all good men" ) );
            Ensure.That( phrase, Text.Matches( "Now.*come" ) );
            // Only available using new syntax
            Ensure.That( phrase, Text.DoesNotMatch( "all.*men.*good" ) );
            Ensure.That( phrase, Text.Matches( "ALL" ).IgnoreCase );
            Ensure.That( quotes, Text.All.Matches( "never" ).IgnoreCase );

            // Inherited syntax
            EnsuranceHelper.Expect( phrase, Matches( "all good men" ) );
            EnsuranceHelper.Expect( phrase, Matches( "Now.*come" ) );
            // Only available using new syntax
            EnsuranceHelper.Expect( phrase, Not.Matches( "all.*men.*good" ) );
            EnsuranceHelper.Expect( phrase, Matches( "ALL" ).IgnoreCase );
            EnsuranceHelper.Expect( quotes, All.Matches( "never" ).IgnoreCase );
        }

        #endregion

        #region Equality Tests

        [TestMethod]
        public void EqualityTests()
        {
            int[] i3 = new int[] {1, 2, 3};
            double[] d3 = new double[] {1.0, 2.0, 3.0};
            int[] iunequal = new int[] {1, 3, 2};

            // Classic Syntax
            Ensure.AreEqual( 4, 2 + 2 );
            Ensure.AreEqual( i3, d3 );
            Ensure.AreNotEqual( 5, 2 + 2 );
            Ensure.AreNotEqual( i3, iunequal );

            // Helper syntax
            Ensure.That( 2 + 2, Is.EqualTo( 4 ) );
            Ensure.That( 2 + 2 == 4 );
            Ensure.That( i3, Is.EqualTo( d3 ) );
            Ensure.That( 2 + 2, Is.Not.EqualTo( 5 ) );
            Ensure.That( i3, Is.Not.EqualTo( iunequal ) );

            // Inherited syntax
            EnsuranceHelper.Expect( 2 + 2, EqualTo( 4 ) );
            EnsuranceHelper.Expect( 2 + 2 == 4 );
            EnsuranceHelper.Expect( i3, EqualTo( d3 ) );
            EnsuranceHelper.Expect( 2 + 2, Not.EqualTo( 5 ) );
            EnsuranceHelper.Expect( i3, Not.EqualTo( iunequal ) );
        }

        [TestMethod]
        public void EqualityTestsWithTolerance()
        {
            // CLassic syntax
            Ensure.AreEqual( 5.0d, 4.99d, 0.05d );
            Ensure.AreEqual( 5.0f, 4.99f, 0.05f );

            // Helper syntax
            Ensure.That( 4.99d, Is.EqualTo( 5.0d ).Within( 0.05d ) );
            Ensure.That( 4.0d, Is.Not.EqualTo( 5.0d ).Within( 0.5d ) );
            Ensure.That( 4.99f, Is.EqualTo( 5.0f ).Within( 0.05f ) );
            Ensure.That( 4.99m, Is.EqualTo( 5.0m ).Within( 0.05m ) );
            Ensure.That( 3999999999u, Is.EqualTo( 4000000000u ).Within( 5u ) );
            Ensure.That( 499, Is.EqualTo( 500 ).Within( 5 ) );
            Ensure.That( 4999999999L, Is.EqualTo( 5000000000L ).Within( 5L ) );
            Ensure.That( 5999999999ul, Is.EqualTo( 6000000000ul ).Within( 5ul ) );

            // Inherited syntax
            EnsuranceHelper.Expect( 4.99d, EqualTo( 5.0d ).Within( 0.05d ) );
            EnsuranceHelper.Expect( 4.0d, Not.EqualTo( 5.0d ).Within( 0.5d ) );
            EnsuranceHelper.Expect( 4.99f, EqualTo( 5.0f ).Within( 0.05f ) );
            EnsuranceHelper.Expect( 4.99m, EqualTo( 5.0m ).Within( 0.05m ) );
            EnsuranceHelper.Expect( 499u, EqualTo( 500u ).Within( 5u ) );
            EnsuranceHelper.Expect( 499, EqualTo( 500 ).Within( 5 ) );
            EnsuranceHelper.Expect( 4999999999L, EqualTo( 5000000000L ).Within( 5L ) );
            EnsuranceHelper.Expect( 5999999999ul, EqualTo( 6000000000ul ).Within( 5ul ) );
        }

        [TestMethod]
        public void EqualityTestsWithTolerance_MixedFloatAndDouble()
        {
            // Bug Fix 1743844
            Ensure.That( 2.20492d, Is.EqualTo( 2.2d ).Within( 0.01f ),
                         "Double actual, Double expected, Single tolerance" );
            Ensure.That( 2.20492d, Is.EqualTo( 2.2f ).Within( 0.01d ),
                         "Double actual, Single expected, Double tolerance" );
            Ensure.That( 2.20492d, Is.EqualTo( 2.2f ).Within( 0.01f ),
                         "Double actual, Single expected, Single tolerance" );
            Ensure.That( 2.20492f, Is.EqualTo( 2.2f ).Within( 0.01d ),
                         "Single actual, Single expected, Double tolerance" );
            Ensure.That( 2.20492f, Is.EqualTo( 2.2d ).Within( 0.01d ),
                         "Single actual, Double expected, Double tolerance" );
            Ensure.That( 2.20492f, Is.EqualTo( 2.2d ).Within( 0.01f ),
                         "Single actual, Double expected, Single tolerance" );
        }

        [TestMethod]
        public void EqualityTestsWithTolerance_MixingTypesGenerally()
        {
            // Extending tolerance to all numeric types
            Ensure.That( 202d, Is.EqualTo( 200d ).Within( 2 ),
                         "Double actual, Double expected, int tolerance" );
            Ensure.That( 4.87m, Is.EqualTo( 5 ).Within( .25 ),
                         "Decimal actual, int expected, Double tolerance" );
            Ensure.That( 4.87m, Is.EqualTo( 5ul ).Within( 1 ),
                         "Decimal actual, ulong expected, int tolerance" );
            Ensure.That( 487, Is.EqualTo( 500 ).Within( 25 ),
                         "int actual, int expected, int tolerance" );
            Ensure.That( 487u, Is.EqualTo( 500 ).Within( 25 ),
                         "uint actual, int expected, int tolerance" );
            Ensure.That( 487L, Is.EqualTo( 500 ).Within( 25 ),
                         "long actual, int expected, int tolerance" );
            Ensure.That( 487ul, Is.EqualTo( 500 ).Within( 25 ),
                         "ulong actual, int expected, int tolerance" );
        }

        #endregion

        #region Comparison Tests

        [TestMethod]
        public void ComparisonTests()
        {
            // Classic Syntax
            Ensure.Greater( 7, 3 );
            Ensure.GreaterOrEqual( 7, 3 );
            Ensure.GreaterOrEqual( 7, 7 );

            // Helper syntax
            Ensure.That( 7, Is.GreaterThan( 3 ) );
            Ensure.That( 7, Is.GreaterThanOrEqualTo( 3 ) );
            Ensure.That( 7, Is.AtLeast( 3 ) );
            Ensure.That( 7, Is.GreaterThanOrEqualTo( 7 ) );
            Ensure.That( 7, Is.AtLeast( 7 ) );

            // Inherited syntax
            EnsuranceHelper.Expect( 7, GreaterThan( 3 ) );
            EnsuranceHelper.Expect( 7, GreaterThanOrEqualTo( 3 ) );
            EnsuranceHelper.Expect( 7, AtLeast( 3 ) );
            EnsuranceHelper.Expect( 7, GreaterThanOrEqualTo( 7 ) );
            EnsuranceHelper.Expect( 7, AtLeast( 7 ) );

            // Classic syntax
            Ensure.Less( 3, 7 );
            Ensure.LessOrEqual( 3, 7 );
            Ensure.LessOrEqual( 3, 3 );

            // Helper syntax
            Ensure.That( 3, Is.LessThan( 7 ) );
            Ensure.That( 3, Is.LessThanOrEqualTo( 7 ) );
            Ensure.That( 3, Is.AtMost( 7 ) );
            Ensure.That( 3, Is.LessThanOrEqualTo( 3 ) );
            Ensure.That( 3, Is.AtMost( 3 ) );

            // Inherited syntax
            EnsuranceHelper.Expect( 3, LessThan( 7 ) );
            EnsuranceHelper.Expect( 3, LessThanOrEqualTo( 7 ) );
            EnsuranceHelper.Expect( 3, AtMost( 7 ) );
            EnsuranceHelper.Expect( 3, LessThanOrEqualTo( 3 ) );
            EnsuranceHelper.Expect( 3, AtMost( 3 ) );
        }

        #endregion

        #region Collection Tests

        [TestMethod]
        public void AllItemsTests()
        {
            object[] ints = new object[] {1, 2, 3, 4};
            object[] doubles = new object[] {0.99, 2.1, 3.0, 4.05};
            object[] strings = new object[] {"abc", "bad", "cab", "bad", "dad"};

            // Classic syntax
            Ensure.AllItemsAreNotNull( ints );
            Ensure.AllItemsAreInstancesOfType( ints, typeof (int) );
            Ensure.AllItemsAreInstancesOfType( strings, typeof (string) );
            Ensure.AllItemsAreUnique( ints );

            // Helper syntax
            Ensure.That( ints, Is.All.Not.Null );
            Ensure.That( ints, Has.None.Null );
            Ensure.That( ints, Is.All.InstanceOfType( typeof (int) ) );
            Ensure.That( ints, Has.All.InstanceOfType( typeof (int) ) );
            Ensure.That( strings, Is.All.InstanceOfType( typeof (string) ) );
            Ensure.That( strings, Has.All.InstanceOfType( typeof (string) ) );
            Ensure.That( ints, Is.Unique );
            // Only available using new syntax
            Ensure.That( strings, Is.Not.Unique );
            Ensure.That( ints, Is.All.GreaterThan( 0 ) );
            Ensure.That( ints, Has.All.GreaterThan( 0 ) );
            Ensure.That( ints, Has.None.LessThanOrEqualTo( 0 ) );
            Ensure.That( strings, Text.All.Contains( "a" ) );
            Ensure.That( strings, Has.All.Contains( "a" ) );
            Ensure.That( strings, Has.Some.StartsWith( "ba" ) );
            Ensure.That( strings, Has.Some.Property( "Length", 3 ) );
            Ensure.That( strings, Has.Some.StartsWith( "BA" ).IgnoreCase );
            Ensure.That( doubles, Has.Some.EqualTo( 1.0 ).Within( .05 ) );

            // Inherited syntax
            EnsuranceHelper.Expect( ints, All.Not.Null );
            EnsuranceHelper.Expect( ints, None.Null );
            EnsuranceHelper.Expect( ints, All.InstanceOfType( typeof (int) ) );
            EnsuranceHelper.Expect( strings, All.InstanceOfType( typeof (string) ) );
            EnsuranceHelper.Expect( ints, Unique );
            // Only available using new syntax
            EnsuranceHelper.Expect( strings, Not.Unique );
            EnsuranceHelper.Expect( ints, All.GreaterThan( 0 ) );
            EnsuranceHelper.Expect( ints, None.LessThanOrEqualTo( 0 ) );
            EnsuranceHelper.Expect( strings, All.Contains( "a" ) );
            EnsuranceHelper.Expect( strings, Some.StartsWith( "ba" ) );
            EnsuranceHelper.Expect( strings, Some.StartsWith( "BA" ).IgnoreCase );
            EnsuranceHelper.Expect( doubles, Some.EqualTo( 1.0 ).Within( .05 ) );
        }

        [TestMethod]
        public void SomeItemTests()
        {
            object[] mixed = new object[] {1, 2, "3", null, "four", 100};
            object[] strings = new object[] {"abc", "bad", "cab", "bad", "dad"};

            // Not available using the classic syntax

            // Helper syntax
            Ensure.That( mixed, Has.Some.Null );
            Ensure.That( mixed, Has.Some.InstanceOfType( typeof (int) ) );
            Ensure.That( mixed, Has.Some.InstanceOfType( typeof (string) ) );
            Ensure.That( strings, Has.Some.StartsWith( "ba" ) );
            Ensure.That( strings, Has.Some.Not.StartsWith( "ba" ) );

            // Inherited syntax
            EnsuranceHelper.Expect( mixed, Some.Null );
            EnsuranceHelper.Expect( mixed, Some.InstanceOfType( typeof (int) ) );
            EnsuranceHelper.Expect( mixed, Some.InstanceOfType( typeof (string) ) );
            EnsuranceHelper.Expect( strings, Some.StartsWith( "ba" ) );
            EnsuranceHelper.Expect( strings, Some.Not.StartsWith( "ba" ) );
        }

        [TestMethod]
        public void NoItemTests()
        {
            object[] ints = new object[] {1, 2, 3, 4, 5};
            object[] strings = new object[] {"abc", "bad", "cab", "bad", "dad"};

            // Not available using the classic syntax

            // Helper syntax
            Ensure.That( ints, Has.None.Null );
            Ensure.That( ints, Has.None.InstanceOfType( typeof (string) ) );
            Ensure.That( ints, Has.None.GreaterThan( 99 ) );
            Ensure.That( strings, Has.None.StartsWith( "qu" ) );

            // Inherited syntax
            EnsuranceHelper.Expect( ints, None.Null );
            EnsuranceHelper.Expect( ints, None.InstanceOfType( typeof (string) ) );
            EnsuranceHelper.Expect( ints, None.GreaterThan( 99 ) );
            EnsuranceHelper.Expect( strings, None.StartsWith( "qu" ) );
        }

        [TestMethod]
        public void CollectionContainsTests()
        {
            int[] iarray = new int[] {1, 2, 3};
            string[] sarray = new string[] {"a", "b", "c"};

            // Classic syntax
            Ensure.Contains( 3, iarray );
            Ensure.Contains( "b", sarray );
            Ensure.Contains( 3, iarray );
            Ensure.Contains( "b", sarray );
            Ensure.DoesNotContain( sarray, "x" );
            // Showing that Contains uses object equality
            Ensure.DoesNotContain( iarray, 1.0d );

            // Helper syntax
            Ensure.That( iarray, Has.Member( 3 ) );
            Ensure.That( sarray, Has.Member( "b" ) );
            Ensure.That( sarray, Has.No.Member( "x" ) );
            // Showing that Contains uses object equality
            Ensure.That( iarray, Has.No.Member( 1.0d ) );

            // Only available using the new syntax
            // Note that EqualTo and SameAs do NOT give identical results to Contains because Contains uses Object.Equals()
            Ensure.That( iarray, Has.Some.EqualTo( 3 ) );
            Ensure.That( iarray, Has.Member( 3 ) );
            Ensure.That( sarray, Has.Some.EqualTo( "b" ) );
            Ensure.That( sarray, Has.None.EqualTo( "x" ) );
            Ensure.That( iarray, Has.None.SameAs( 1.0d ) );
            Ensure.That( iarray, Has.All.LessThan( 10 ) );
            Ensure.That( sarray, Has.All.Length( 1 ) );
            Ensure.That( sarray, Has.None.Property( "Length" ).GreaterThan( 3 ) );

            // Inherited syntax
            EnsuranceHelper.Expect( iarray, Contains( 3 ) );
            EnsuranceHelper.Expect( sarray, Contains( "b" ) );
            EnsuranceHelper.Expect( sarray, Not.Contains( "x" ) );

            // Only available using new syntax
            // Note that EqualTo and SameAs do NOT give identical results to Contains because Contains uses Object.Equals()
            EnsuranceHelper.Expect( iarray, Some.EqualTo( 3 ) );
            EnsuranceHelper.Expect( sarray, Some.EqualTo( "b" ) );
            EnsuranceHelper.Expect( sarray, None.EqualTo( "x" ) );
            EnsuranceHelper.Expect( iarray, All.LessThan( 10 ) );
            EnsuranceHelper.Expect( sarray, All.Length( 1 ) );
            EnsuranceHelper.Expect( sarray, None.Property( "Length" ).GreaterThan( 3 ) );
        }

        [TestMethod]
        public void CollectionEquivalenceTests()
        {
            int[] ints1to5 = new int[] {1, 2, 3, 4, 5};
            int[] twothrees = new int[] {1, 2, 3, 3, 4, 5};
            int[] twofours = new int[] {1, 2, 3, 4, 4, 5};

            // Classic syntax
            Ensure.AreEquivalent( new int[] {2, 1, 4, 3, 5}, ints1to5 );
            Ensure.AreNotEquivalent( new int[] {2, 2, 4, 3, 5}, ints1to5 );
            Ensure.AreNotEquivalent( new int[] {2, 4, 3, 5}, ints1to5 );
            Ensure.AreNotEquivalent( new int[] {2, 2, 1, 1, 4, 3, 5}, ints1to5 );
            Ensure.AreNotEquivalent( twothrees, twofours );

            // Helper syntax
            Ensure.That( new int[] {2, 1, 4, 3, 5}, Is.EquivalentTo( ints1to5 ) );
            Ensure.That( new int[] {2, 2, 4, 3, 5}, Is.Not.EquivalentTo( ints1to5 ) );
            Ensure.That( new int[] {2, 4, 3, 5}, Is.Not.EquivalentTo( ints1to5 ) );
            Ensure.That( new int[] {2, 2, 1, 1, 4, 3, 5}, Is.Not.EquivalentTo( ints1to5 ) );

            // Inherited syntax
            EnsuranceHelper.Expect( new int[] {2, 1, 4, 3, 5}, EquivalentTo( ints1to5 ) );
            EnsuranceHelper.Expect( new int[] {2, 2, 4, 3, 5}, Not.EquivalentTo( ints1to5 ) );
            EnsuranceHelper.Expect( new int[] {2, 4, 3, 5}, Not.EquivalentTo( ints1to5 ) );
            EnsuranceHelper.Expect( new int[] {2, 2, 1, 1, 4, 3, 5}, Not.EquivalentTo( ints1to5 ) );
        }

        [TestMethod]
        public void SubsetTests()
        {
            int[] ints1to5 = new int[] {1, 2, 3, 4, 5};

            // Classic syntax
            Ensure.IsSubsetOf( new int[] {1, 3, 5}, ints1to5 );
            Ensure.IsSubsetOf( new int[] {1, 2, 3, 4, 5}, ints1to5 );
            Ensure.IsNotSubsetOf( new int[] {2, 4, 6}, ints1to5 );
            Ensure.IsNotSubsetOf( new int[] {1, 2, 2, 2, 5}, ints1to5 );

            // Helper syntax
            Ensure.That( new int[] {1, 3, 5}, Is.SubsetOf( ints1to5 ) );
            Ensure.That( new int[] {1, 2, 3, 4, 5}, Is.SubsetOf( ints1to5 ) );
            Ensure.That( new int[] {2, 4, 6}, Is.Not.SubsetOf( ints1to5 ) );

            // Inherited syntax
            EnsuranceHelper.Expect( new int[] {1, 3, 5}, SubsetOf( ints1to5 ) );
            EnsuranceHelper.Expect( new int[] {1, 2, 3, 4, 5}, SubsetOf( ints1to5 ) );
            EnsuranceHelper.Expect( new int[] {2, 4, 6}, Not.SubsetOf( ints1to5 ) );
        }

        #endregion

        #region Property Tests

        [TestMethod]
        public void PropertyTests()
        {
            string[] array = {"abc", "bca", "xyz", "qrs"};
            string[] array2 = {"a", "ab", "abc"};
            ArrayList list = new ArrayList( array );

            // Not available using the classic syntax

            // Helper syntax
            Ensure.That( list, Has.Property( "Count" ) );
            Ensure.That( list, Has.No.Property( "Length" ) );

            Ensure.That( "Hello", Has.Property( "Length", 5 ) );
            Ensure.That( "Hello", Has.Length( 5 ) );
            Ensure.That( "Hello", Has.Property( "Length" ).EqualTo( 5 ) );
            Ensure.That( "Hello", Has.Property( "Length" ).GreaterThan( 3 ) );

            Ensure.That( array, Has.Property( "Length", 4 ) );
            Ensure.That( array, Has.Length( 4 ) );
            Ensure.That( array, Has.Property( "Length" ).LessThan( 10 ) );

            Ensure.That( array, Has.All.Property( "Length", 3 ) );
            Ensure.That( array, Has.All.Length( 3 ) );
            Ensure.That( array, Is.All.Length( 3 ) );
            Ensure.That( array, Has.All.Property( "Length" ).EqualTo( 3 ) );
            Ensure.That( array, Is.All.Property( "Length" ).EqualTo( 3 ) );

            Ensure.That( array2, Has.Some.Property( "Length", 2 ) );
            Ensure.That( array2, Has.Some.Length( 2 ) );
            Ensure.That( array2, Has.Some.Property( "Length" ).GreaterThan( 2 ) );

            Ensure.That( array2, Is.Not.Property( "Length", 4 ) );
            Ensure.That( array2, Is.Not.Length( 4 ) );
            Ensure.That( array2, Has.No.Property( "Length" ).GreaterThan( 3 ) );

            Ensure.That( List.Map( array2 ).Property( "Length" ), Is.EqualTo( new int[] {1, 2, 3} ) );
            Ensure.That( List.Map( array2 ).Property( "Length" ), Is.EquivalentTo( new int[] {3, 2, 1} ) );
            Ensure.That( List.Map( array2 ).Property( "Length" ), Is.SubsetOf( new int[] {1, 2, 3, 4, 5} ) );
            Ensure.That( List.Map( array2 ).Property( "Length" ), Is.Unique );

            Ensure.That( list, Has.Count( 4 ) );

            // Inherited syntax
            EnsuranceHelper.Expect( list, Property( "Count" ) );
            EnsuranceHelper.Expect( list, Not.Property( "Nada" ) );

            EnsuranceHelper.Expect( "Hello", Property( "Length", 5 ) );
            EnsuranceHelper.Expect( "Hello", Length( 5 ) );
            EnsuranceHelper.Expect( "Hello", Property( "Length" ).EqualTo( 5 ) );
            EnsuranceHelper.Expect( "Hello", Property( "Length" ).GreaterThan( 0 ) );

            EnsuranceHelper.Expect( array, Property( "Length", 4 ) );
            EnsuranceHelper.Expect( array, Length( 4 ) );
            EnsuranceHelper.Expect( array, Property( "Length" ).LessThan( 10 ) );

            EnsuranceHelper.Expect( array, All.Property( "Length", 3 ) );
            EnsuranceHelper.Expect( array, All.Length( 3 ) );
            EnsuranceHelper.Expect( array, All.Property( "Length" ).EqualTo( 3 ) );

            EnsuranceHelper.Expect( array2, Some.Property( "Length", 2 ) );
            EnsuranceHelper.Expect( array2, Some.Length( 2 ) );
            EnsuranceHelper.Expect( array2, Some.Property( "Length" ).GreaterThan( 2 ) );

            EnsuranceHelper.Expect( array2, None.Property( "Length", 4 ) );
            EnsuranceHelper.Expect( array2, None.Length( 4 ) );
            EnsuranceHelper.Expect( array2, None.Property( "Length" ).GreaterThan( 3 ) );

            EnsuranceHelper.Expect( new ListMapper( array2 ).Property( "Length" ), EqualTo( new int[] {1, 2, 3} ) );
            EnsuranceHelper.Expect( new ListMapper( array2 ).Property( "Length" ), EquivalentTo( new int[] {3, 2, 1} ) );
            EnsuranceHelper.Expect( new ListMapper( array2 ).Property( "Length" ), SubsetOf( new int[] {1, 2, 3, 4, 5} ) );
            EnsuranceHelper.Expect( new ListMapper( array2 ).Property( "Length" ), Unique );

            EnsuranceHelper.Expect( list, Count( 4 ) );
        }

        #endregion

        #region Not Tests

        [TestMethod]
        public void NotTests()
        {
            // Not available using the classic syntax

            // Helper syntax
            Ensure.That( 42, Is.Not.Null );
            Ensure.That( 42, Is.Not.True );
            Ensure.That( 42, Is.Not.False );
            Ensure.That( 2.5, Is.Not.NaN );
            Ensure.That( 2 + 2, Is.Not.EqualTo( 3 ) );
            Ensure.That( 2 + 2, Is.Not.Not.EqualTo( 4 ) );
            Ensure.That( 2 + 2, Is.Not.Not.Not.EqualTo( 5 ) );

            // Inherited syntax
            EnsuranceHelper.Expect( 42, Not.Null );
            EnsuranceHelper.Expect( 42, Not.True );
            EnsuranceHelper.Expect( 42, Not.False );
            EnsuranceHelper.Expect( 2.5, Not.NaN );
            EnsuranceHelper.Expect( 2 + 2, Not.EqualTo( 3 ) );
            EnsuranceHelper.Expect( 2 + 2, Not.Not.EqualTo( 4 ) );
            EnsuranceHelper.Expect( 2 + 2, Not.Not.Not.EqualTo( 5 ) );
        }

        #endregion

        #region Operator Tests

        [TestMethod]
        public void NotOperator()
        {
            // The ! operator is only available in the new syntax
            Ensure.That( 42, !Is.Null );
            // Inherited syntax
            EnsuranceHelper.Expect( 42, !Null );
        }

        [TestMethod]
        public void AndOperator()
        {
            // The & operator is only available in the new syntax
            Ensure.That( 7, Is.GreaterThan( 5 ) & Is.LessThan( 10 ) );
            // Inherited syntax
            EnsuranceHelper.Expect( 7, GreaterThan( 5 ) & LessThan( 10 ) );
        }

        [TestMethod]
        public void OrOperator()
        {
            // The | operator is only available in the new syntax
            Ensure.That( 3, Is.LessThan( 5 ) | Is.GreaterThan( 10 ) );
            EnsuranceHelper.Expect( 3, LessThan( 5 ) | GreaterThan( 10 ) );
        }

        [TestMethod]
        public void ComplexTests()
        {
            Ensure.That( 7, Is.Not.Null & Is.Not.LessThan( 5 ) & Is.Not.GreaterThan( 10 ) );
            EnsuranceHelper.Expect( 7, Not.Null & Not.LessThan( 5 ) & Not.GreaterThan( 10 ) );

            Ensure.That( 7, !Is.Null & !Is.LessThan( 5 ) & !Is.GreaterThan( 10 ) );
            EnsuranceHelper.Expect( 7, !Null & !LessThan( 5 ) & !GreaterThan( 10 ) );

            // TODO: Remove #if when mono compiler can handle null
#if MONO
            Constraint x = null;
            Ensure.That(7, !x & !Is.LessThan(5) & !Is.GreaterThan(10));
			EnsuranceHelper.Expect(7, !x & !LessThan(5) & !GreaterThan(10));
#else
            Ensure.That( 7, !(Constraint) null & !Is.LessThan( 5 ) & !Is.GreaterThan( 10 ) );
            EnsuranceHelper.Expect( 7, !(Constraint) null & !LessThan( 5 ) & !GreaterThan( 10 ) );
#endif
        }

        #endregion

        #region Invalid Code Tests

        // This method contains assertions that should not compile
        // You can check by uncommenting it.
        //public void WillNotCompile()
        //{
        //    Ensure.That(42, Is.Not);
        //    Ensure.That(42, Is.All);
        //    Ensure.That(42, Is.Null.Not);
        //    Ensure.That(42, Is.Not.Null.GreaterThan(10));
        //    Ensure.That(42, Is.GreaterThan(10).LessThan(99));

        //    object[] c = new object[0];
        //    Ensure.That(c, Is.Null.All);
        //    Ensure.That(c, Is.Not.All);
        //    Ensure.That(c, Is.All.Not);
        //}

        #endregion
    }
}