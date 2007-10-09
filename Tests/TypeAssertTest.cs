// ****************************************************************
// Copyright 2007, Charlie Poole
// This is free software licensed under the NUnit license. You may
// obtain a copy of the license at http://nunit.org/?p=license&r=2.4
// ****************************************************************

using System;
using Ensurance.SyntaxHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using NUnit.Framework.Syntax;

namespace Ensurance.Tests
{
    [TestClass]
    public class TypeAssertTests : MessageChecker
    {
        [TestMethod]
        public void ExactType()
        {
            EnsuranceHelper.Expect( "Hello", Is.TypeOf( typeof (String) ) );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void ExactTypeFails()
        {
            expectedMessage =
                "  Expected: <System.Int32>" + Environment.NewLine +
                "  But was:  <System.String>" + Environment.NewLine;
            EnsuranceHelper.Expect( "Hello", Is.TypeOf( typeof (Int32) ) );
        }

        [TestMethod]
        public void IsInstanceOfType()
        {
            Ensure.IsInstanceOfType( typeof (Exception), new ApplicationException() );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void IsInstanceOfTypeFails()
        {
            expectedMessage =
                "  Expected: instance of <System.Int32>" + Environment.NewLine +
                "  But was:  <System.String>" + Environment.NewLine;
            EnsuranceHelper.Expect( "abc123", Is.InstanceOfType( typeof (Int32) ) );
        }

        [TestMethod]
        public void IsNotInstanceOfType()
        {
            Ensure.IsNotInstanceOfType( typeof (Int32), "abc123" );
            EnsuranceHelper.Expect( "abc123", Is.Not.InstanceOfType( typeof (Int32) ) );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void IsNotInstanceOfTypeFails()
        {
            expectedMessage =
                "  Expected: not instance of <System.Exception>" + Environment.NewLine +
                "  But was:  <System.ApplicationException>" + Environment.NewLine;
            Ensure.IsNotInstanceOfType( typeof (Exception), new ApplicationException() );
        }

        [TestMethod]
        public void IsAssignableFrom()
        {
            int[] array10 = new int[10];
            int[] array2 = new int[2];

            Ensure.IsAssignableFrom( array2.GetType(), array10 );
            Ensure.IsAssignableFrom( array2.GetType(), array10, "Type Failure Message" );
            Ensure.IsAssignableFrom( array2.GetType(), array10, "Type Failure Message", null );
            EnsuranceHelper.Expect( array10, Is.AssignableFrom( array2.GetType() ) );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void IsAssignableFromFails()
        {
            int[] array10 = new int[10];
            int[,] array2 = new int[2,2];

            expectedMessage =
                "  Expected: Type assignable from <System.Int32[,]>" + Environment.NewLine +
                "  But was:  <System.Int32[]>" + Environment.NewLine;
            EnsuranceHelper.Expect( array10, Is.AssignableFrom( array2.GetType() ) );
        }

        [TestMethod]
        public void IsNotAssignableFrom()
        {
            int[] array10 = new int[10];
            int[,] array2 = new int[2,2];

            Ensure.IsNotAssignableFrom( array2.GetType(), array10 );
            Ensure.IsNotAssignableFrom( array2.GetType(), array10, "Type Failure Message" );
            Ensure.IsNotAssignableFrom( array2.GetType(), array10, "Type Failure Message", null );
            EnsuranceHelper.Expect( array10, Is.Not.AssignableFrom( array2.GetType() ) );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void IsNotAssignableFromFails()
        {
            int[] array10 = new int[10];
            int[] array2 = new int[2];

            expectedMessage =
                "  Expected: not Type assignable from <System.Int32[]>" + Environment.NewLine +
                "  But was:  <System.Int32[]>" + Environment.NewLine;
            EnsuranceHelper.Expect( array10, Is.Not.AssignableFrom( array2.GetType() ) );
        }
    }
}