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
using Ensurance.MessageWriters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ensurance.Tests
{
    [TestClass]
    public class StringAssertTests : MessageChecker
    {
        [TestMethod]
        public void Contains()
        {
            EnsureBase<Ensure>.Strings.Contains( "abc", "abc" );
            EnsureBase<Ensure>.Strings.Contains( "abc", "***abc" );
            EnsureBase<Ensure>.Strings.Contains( "abc", "**abc**" );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void ContainsFails()
        {
            expectedMessage =
                TextMessageWriter.Pfx_Expected + "String containing \"abc\"" + Environment.NewLine +
                TextMessageWriter.Pfx_Actual + "\"abxcdxbc\"" + Environment.NewLine;
            EnsureBase<Ensure>.Strings.Contains( "abc", "abxcdxbc" );
        }

        [TestMethod]
        public void StartsWith()
        {
            EnsureBase<Ensure>.Strings.StartsWith( "abc", "abcdef" );
            EnsureBase<Ensure>.Strings.StartsWith( "abc", "abc" );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void StartsWithFails()
        {
            expectedMessage =
                TextMessageWriter.Pfx_Expected + "String starting with \"xyz\"" + Environment.NewLine +
                TextMessageWriter.Pfx_Actual + "\"abcxyz\"" + Environment.NewLine;
            EnsureBase<Ensure>.Strings.StartsWith( "xyz", "abcxyz" );
        }

        [TestMethod]
        public void EndsWith()
        {
            EnsureBase<Ensure>.Strings.EndsWith( "abc", "abc" );
            EnsureBase<Ensure>.Strings.EndsWith( "abc", "123abc" );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void EndsWithFails()
        {
            expectedMessage =
                TextMessageWriter.Pfx_Expected + "String ending with \"xyz\"" + Environment.NewLine +
                TextMessageWriter.Pfx_Actual + "\"abcdef\"" + Environment.NewLine;
            EnsureBase<Ensure>.Strings.EndsWith( "xyz", "abcdef" );
        }

        [TestMethod]
        public void CaseInsensitiveCompare()
        {
            EnsureBase<Ensure>.Strings.AreEqualIgnoringCase( "name", "NAME" );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void CaseInsensitiveCompareFails()
        {
            expectedMessage =
                "  Expected string length 4 but was 5. Strings differ at index 4." + Environment.NewLine
                + TextMessageWriter.Pfx_Expected + "\"Name\", ignoring case" + Environment.NewLine
                + TextMessageWriter.Pfx_Actual + "\"NAMES\"" + Environment.NewLine
                + "  ---------------^" + Environment.NewLine;
            EnsureBase<Ensure>.Strings.AreEqualIgnoringCase( "Name", "NAMES" );
        }

        [TestMethod]
        public void IsMatch()
        {
            EnsureBase<Ensure>.Strings.IsMatch( "a?bc", "12a3bc45" );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void IsMatchFails()
        {
            expectedMessage =
                TextMessageWriter.Pfx_Expected + "String matching \"a?b*c\"" + Environment.NewLine +
                TextMessageWriter.Pfx_Actual + "\"12ab456\"" + Environment.NewLine;
            EnsureBase<Ensure>.Strings.IsMatch( "a?b*c", "12ab456" );
        }
    }
}