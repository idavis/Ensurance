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
using System.Globalization;
using System.Reflection;

namespace Ensurance.SyntaxHelpers
{
    /// <summary>
    /// ListMapper is used to transform a collection used as an actual argument
    /// producing another collection to be used in the assertion.
    /// </summary>
    public class ListMapper
    {
        private ICollection original;

        /// <summary>
        /// Construct a ListMapper based on a collection
        /// </summary>
        /// <param name="original">The collection to be transformed</param>
        public ListMapper( ICollection original )
        {
            this.original = original;
        }

        /// <summary>
        /// Produces a collection containing all the values of a property
        /// </summary>
        /// <param name="name">The collection of property values</param>
        /// <returns></returns>
        public ICollection Property( string name )
        {
            ArrayList propList = new ArrayList();
            foreach (object item in original)
            {
                PropertyInfo property = item.GetType().GetProperty( name,
                                                                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance );
                if ( property == null )
                {
                    throw new ArgumentException( string.Format( CultureInfo.CurrentCulture,
                                                                "{0} does not have a {1} property", item, name ) );
                }

                propList.Add( property.GetValue( item, null ) );
            }

            return propList;
        }
    }
}