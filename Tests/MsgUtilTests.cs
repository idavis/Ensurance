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

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ensurance.Tests
{
    /// <summary>
    /// Summary description for MsgUtilTests.
    /// </summary>
    [TestClass]
    public class MsgUtilTests
    {
        [TestMethod]
        public void TestConvertWhitespace()
        {
            Ensure.AreEqual( "\\n", MsgUtils.ConvertWhitespace( "\n" ) );
            Ensure.AreEqual( "\\n\\n", MsgUtils.ConvertWhitespace( "\n\n" ) );
            Ensure.AreEqual( "\\n\\n\\n", MsgUtils.ConvertWhitespace( "\n\n\n" ) );

            Ensure.AreEqual( "\\r", MsgUtils.ConvertWhitespace( "\r" ) );
            Ensure.AreEqual( "\\r\\r", MsgUtils.ConvertWhitespace( "\r\r" ) );
            Ensure.AreEqual( "\\r\\r\\r", MsgUtils.ConvertWhitespace( "\r\r\r" ) );

            Ensure.AreEqual( "\\r\\n", MsgUtils.ConvertWhitespace( "\r\n" ) );
            Ensure.AreEqual( "\\n\\r", MsgUtils.ConvertWhitespace( "\n\r" ) );
            Ensure.AreEqual( "This is a\\rtest message", MsgUtils.ConvertWhitespace( "This is a\rtest message" ) );

            Ensure.AreEqual( "", MsgUtils.ConvertWhitespace( "" ) );
            Ensure.AreEqual( null, MsgUtils.ConvertWhitespace( null ) );

            Ensure.AreEqual( "\\t", MsgUtils.ConvertWhitespace( "\t" ) );
            Ensure.AreEqual( "\\t\\n", MsgUtils.ConvertWhitespace( "\t\n" ) );

            Ensure.AreEqual( "\\\\r\\\\n", MsgUtils.ConvertWhitespace( "\\r\\n" ) );
        }
    }
}