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
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using Ensurance.Tests.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ensurance.Tests
{
    public class TestFile : IDisposable
    {
        private bool _disposedValue = false;
        private string _resourceName;

        #region Nested TestFile Utility Class

        //private string _fileName;

        public TestFile( string resourceName, Bitmap resource )
        {
            _resourceName = Path.Combine( AppDomain.CurrentDomain.BaseDirectory, resourceName );
            Stream resourceStream = new MemoryStream();
            resource.Save( resourceStream, ImageFormat.Jpeg );
            if ( resourceStream == null )
            {
                throw new Exception( "Manifest Resource Stream " + _resourceName + " was not found." );
            }

            SaveResourceToDisk( resourceStream );
        }

        public TestFile( string resourceName, string resource )
        {
            _resourceName = Path.Combine( AppDomain.CurrentDomain.BaseDirectory, resourceName );
            Stream resourceStream = new MemoryStream( Encoding.UTF8.GetBytes( resource ) );
            if ( resourceStream == null )
            {
                throw new Exception( "Manifest Resource Stream " + _resourceName + " was not found." );
            }

            SaveResourceToDisk( resourceStream );
        }

        #region IDisposable Members

        public void Dispose()
        {
            // Do not change this code.  Put cleanup code in Dispose(bool disposing) above.
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        #endregion

        private void SaveResourceToDisk( Stream resourceStream )
        {
            resourceStream.Seek( 0, SeekOrigin.Begin );
            using (Stream s = resourceStream)
            {
                byte[] buffer = new byte[1024];
                using (FileStream fs = File.Create( _resourceName ))
                {
                    while ( true )
                    {
                        int count = s.Read( buffer, 0, buffer.Length );
                        if ( count == 0 )
                        {
                            break;
                        }
                        fs.Write( buffer, 0, count );
                    }
                }
            }
        }

        protected virtual void Dispose( bool disposing )
        {
            if ( !_disposedValue )
            {
                if ( disposing )
                {
                    if ( File.Exists( _resourceName ) )
                    {
                        File.Delete( _resourceName );
                    }
                }
            }
            _disposedValue = true;
        }
    }

    #endregion

    /// <summary>
    /// Summary description for FileAssertTests.
    /// </summary>
    [TestClass]
    public class FileAssertTests : MessageChecker
    {
        #region AreEqual

        #region Success Tests

        [TestMethod]
        public void AreEqualPassesWhenBothAreNull()
        {
            FileStream expected = null;
            FileStream actual = null;
            Ensure.Files.AreEqual( expected, actual );
        }

        [TestMethod]
        public void AreEqualPassesWithStreams()
        {
            using (TestFile tf1 = new TestFile( "TestImage1.jpg", Resources.TestImage1 ))
            {
                using (TestFile tf2 = new TestFile( "TestImage2.jpg", Resources.TestImage1 ))
                {
                    using (FileStream expected = File.OpenRead( "TestImage1.jpg" ))
                    {
                        using (FileStream actual = File.OpenRead( "TestImage2.jpg" ))
                        {
                            Ensure.Files.AreEqual( expected, actual );
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void AreEqualPassesWithFiles()
        {
            using (TestFile tf1 = new TestFile( "TestImage1.jpg", Resources.TestImage1 ))
            {
                using (TestFile tf2 = new TestFile( "TestImage2.jpg", Resources.TestImage1 ))
                {
                    Ensure.Files.AreEqual( "TestImage1.jpg", "TestImage2.jpg", "Failed using file names" );
                }
            }
        }

        [TestMethod]
        public void AreEqualPassesUsingSameFileTwice()
        {
            using (TestFile tf1 = new TestFile( "TestImage1.jpg", Resources.TestImage1 ))
            {
                Ensure.Files.AreEqual( "TestImage1.jpg", "TestImage1.jpg" );
            }
        }

        [TestMethod]
        public void AreEqualPassesWithFileInfos()
        {
            using (TestFile tf1 = new TestFile( "TestImage1.jpg", Resources.TestImage1 ))
            {
                using (TestFile tf2 = new TestFile( "TestImage2.jpg", Resources.TestImage1 ))
                {
                    FileInfo expected = new FileInfo( "TestImage1.jpg" );
                    FileInfo actual = new FileInfo( "TestImage2.jpg" );
                    Ensure.Files.AreEqual( expected, actual );
                    Ensure.Files.AreEqual( expected, actual );
                }
            }
        }

        [TestMethod]
        public void AreEqualPassesWithTextFiles()
        {
            using (TestFile tf1 = new TestFile( "TestText1.txt", Resources.TestText1 ))
            {
                using (TestFile tf2 = new TestFile( "TestText2.txt", Resources.TestText1 ))
                {
                    Ensure.Files.AreEqual( "TestText1.txt", "TestText2.txt" );
                }
            }
        }

        #endregion

        #region Failure Tests

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void AreEqualFailsWhenOneIsNull()
        {
            using (TestFile tf1 = new TestFile( "TestImage1.jpg", Resources.TestImage1 ))
            {
                using (FileStream expected = File.OpenRead( "TestImage1.jpg" ))
                {
                    expectedMessage =
                        "  Expected: <System.IO.FileStream>" + Environment.NewLine +
                        "  But was:  null" + Environment.NewLine;
                    Ensure.Files.AreEqual( expected, null );
                }
            }
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void AreEqualFailsWithStreams()
        {
            string expectedFile = "TestImage1.jpg";
            string actualFile = "TestImage2.jpg";
            using (TestFile tf1 = new TestFile( expectedFile, Resources.TestImage1 ))
            {
                using (TestFile tf2 = new TestFile( actualFile, Resources.TestImage2 ))
                {
                    using (FileStream expected = File.OpenRead( expectedFile ))
                    {
                        using (FileStream actual = File.OpenRead( actualFile ))
                        {
                            expectedMessage =
                                string.Format( "  Expected Stream length {0} but was {1}." + Environment.NewLine,
                                               new FileInfo( expectedFile ).Length, new FileInfo( actualFile ).Length );
                            Ensure.Files.AreEqual( expected, actual );
                        }
                    }
                }
            }
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void AreEqualFailsWithFileInfos()
        {
            using (TestFile tf1 = new TestFile( "TestImage1.jpg", Resources.TestImage1 ))
            {
                using (TestFile tf2 = new TestFile( "TestImage2.jpg", Resources.TestImage2 ))
                {
                    FileInfo expected = new FileInfo( "TestImage1.jpg" );
                    FileInfo actual = new FileInfo( "TestImage2.jpg" );
                    expectedMessage =
                        string.Format( "  Expected Stream length {0} but was {1}." + Environment.NewLine,
                                       expected.Length, actual.Length );
                    Ensure.Files.AreEqual( expected, actual );
                }
            }
        }


        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void AreEqualFailsWithFiles()
        {
            string expected = "TestImage1.jpg";
            string actual = "TestImage2.jpg";
            using (TestFile tf1 = new TestFile( expected, Resources.TestImage1 ))
            {
                using (TestFile tf2 = new TestFile( actual, Resources.TestImage2 ))
                {
                    expectedMessage =
                        string.Format( "  Expected Stream length {0} but was {1}." + Environment.NewLine,
                                       new FileInfo( expected ).Length, new FileInfo( actual ).Length );
                    Ensure.Files.AreEqual( expected, actual );
                }
            }
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void AreEqualFailsWithTextFilesAfterReadingBothFiles()
        {
            using (TestFile tf1 = new TestFile( "TestText1.txt", Resources.TestText1 ))
            {
                using (TestFile tf2 = new TestFile( "TestText2.txt", Resources.TestText2 ))
                {
                    expectedMessage =
                        "  Stream lengths are both 65600. Streams differ at offset 65597." + Environment.NewLine;
                    Ensure.Files.AreEqual( "TestText1.txt", "TestText2.txt" );
                }
            }
        }

        #endregion

        #endregion

        #region AreNotEqual

        #region Success Tests

        [TestMethod]
        public void AreNotEqualPassesIfOneIsNull()
        {
            using (TestFile tf1 = new TestFile( "TestImage1.jpg", Resources.TestImage1 ))
            {
                using (FileStream expected = File.OpenRead( "TestImage1.jpg" ))
                {
                    Ensure.Files.AreNotEqual( expected, null );
                }
            }
        }

        [TestMethod]
        public void AreNotEqualPassesWithStreams()
        {
            using (TestFile tf1 = new TestFile( "TestImage1.jpg", Resources.TestImage1 ))
            {
                using (TestFile tf2 = new TestFile( "TestImage2.jpg", Resources.TestImage2 ))
                {
                    using (FileStream expected = File.OpenRead( "TestImage1.jpg" ))
                    {
                        using (FileStream actual = File.OpenRead( "TestImage2.jpg" ))
                        {
                            Ensure.Files.AreNotEqual( expected, actual );
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void AreNotEqualPassesWithFiles()
        {
            using (TestFile tf1 = new TestFile( "TestImage1.jpg", Resources.TestImage1 ))
            {
                using (TestFile tf2 = new TestFile( "TestImage2.jpg", Resources.TestImage2 ))
                {
                    Ensure.Files.AreNotEqual( "TestImage1.jpg", "TestImage2.jpg" );
                }
            }
        }

        [TestMethod]
        public void AreNotEqualPassesWithFileInfos()
        {
            using (TestFile tf1 = new TestFile( "TestImage1.jpg", Resources.TestImage1 ))
            {
                using (TestFile tf2 = new TestFile( "TestImage2.jpg", Resources.TestImage2 ))
                {
                    FileInfo expected = new FileInfo( "TestImage1.jpg" );
                    FileInfo actual = new FileInfo( "TestImage2.jpg" );
                    Ensure.Files.AreNotEqual( expected, actual );
                }
            }
        }

        [TestMethod]
        public void AreNotEqualIteratesOverTheEntireFile()
        {
            using (TestFile tf1 = new TestFile( "TestText1.txt", Resources.TestText1 ))
            {
                using (TestFile tf2 = new TestFile( "TestText2.txt", Resources.TestText2 ))
                {
                    Ensure.Files.AreNotEqual( "TestText1.txt", "TestText2.txt" );
                }
            }
        }

        #endregion

        #region Failure Tests

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void AreNotEqualFailsWhenBothAreNull()
        {
            FileStream expected = null;
            FileStream actual = null;
            expectedMessage =
                "  Expected: not null" + Environment.NewLine +
                "  But was:  null" + Environment.NewLine;
            Ensure.Files.AreNotEqual( expected, actual );
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void AreNotEqualFailsWithStreams()
        {
            using (TestFile tf1 = new TestFile( "TestImage1.jpg", Resources.TestImage1 ))
            {
                using (TestFile tf2 = new TestFile( "TestImage2.jpg", Resources.TestImage1 ))
                {
                    using (FileStream expected = File.OpenRead( "TestImage1.jpg" ))
                    {
                        using (FileStream actual = File.OpenRead( "TestImage2.jpg" ))
                        {
                            Ensure.Files.AreNotEqual( expected, actual );
                        }
                    }
                }
            }
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void AreNotEqualFailsWithFileInfos()
        {
            using (TestFile tf1 = new TestFile( "TestImage1.jpg", Resources.TestImage1 ))
            {
                using (TestFile tf2 = new TestFile( "TestImage2.jpg", Resources.TestImage1 ))
                {
                    FileInfo expected = new FileInfo( "TestImage1.jpg" );
                    FileInfo actual = new FileInfo( "TestImage2.jpg" );
                    expectedMessage =
                        "  Expected: not <System.IO.FileStream>" + Environment.NewLine +
                        "  But was:  <System.IO.FileStream>" + Environment.NewLine;
                    Ensure.Files.AreNotEqual( expected, actual );
                }
            }
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void AreNotEqualFailsWithFiles()
        {
            using (TestFile tf1 = new TestFile( "TestImage1.jpg", Resources.TestImage1 ))
            {
                expectedMessage =
                    "  Expected: not <System.IO.FileStream>" + Environment.NewLine +
                    "  But was:  <System.IO.FileStream>" + Environment.NewLine;
                Ensure.Files.AreNotEqual( "TestImage1.jpg", "TestImage1.jpg" );
            }
        }

        [TestMethod, ExpectedException( typeof (EnsuranceException) )]
        public void AreNotEqualIteratesOverTheEntireFileAndFails()
        {
            using (TestFile tf1 = new TestFile( "TestText1.txt", Resources.TestText1 ))
            {
                using (TestFile tf2 = new TestFile( "TestText2.txt", Resources.TestText1 ))
                {
                    expectedMessage =
                        "  Expected: not <System.IO.FileStream>" + Environment.NewLine +
                        "  But was:  <System.IO.FileStream>" + Environment.NewLine;
                    Ensure.Files.AreNotEqual( "TestText1.txt", "TestText2.txt" );
                }
            }
        }

        #endregion

        #endregion
    }
}