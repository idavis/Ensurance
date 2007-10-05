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

namespace Ensurance.Tests
{
    /// <summary>
    /// ICollectionAdapter is used in testing to wrap an array or
    /// ArrayList, ensuring that only methods of the ICollection
    /// interface are accessible.
    /// </summary>
    internal class ICollectionAdapter : ICollection
    {
        private readonly ICollection inner;

        public ICollectionAdapter( ICollection inner )
        {
            this.inner = inner;
        }

        public ICollectionAdapter( params object[] inner )
        {
            this.inner = inner;
        }

        #region ICollection Members

        public void CopyTo( Array array, int index )
        {
            inner.CopyTo( array, index );
        }

        public int Count
        {
            get { return inner.Count; }
        }

        public bool IsSynchronized
        {
            get { return inner.IsSynchronized; }
        }

        public object SyncRoot
        {
            get { return inner.SyncRoot; }
        }

        public IEnumerator GetEnumerator()
        {
            return inner.GetEnumerator();
        }

        #endregion
    }
}