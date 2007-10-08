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

using System.Reflection;
using System.Runtime.InteropServices;
using log4net.Config;
// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly : AssemblyTitle( "TestBed" )]
[assembly : AssemblyDescription( "" )]
[assembly : AssemblyConfiguration( "" )]
[assembly : AssemblyCompany( "Organization" )]
[assembly : AssemblyProduct( "TestBed" )]
[assembly : AssemblyCopyright( "Copyright © Organization 2007" )]
[assembly : AssemblyTrademark( "" )]
[assembly : AssemblyCulture( "" )]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly : ComVisible( false )]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly : Guid( "d7263ad0-5e59-472f-8eda-4efd501eb3bf" )]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
[assembly : AssemblyVersion( "1.0.0.0" )]
[assembly : AssemblyFileVersion( "1.0.0.0" )]
[assembly : XmlConfigurator( ConfigFileExtension = "config" )]