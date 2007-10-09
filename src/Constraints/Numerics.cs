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
using System.Globalization;

namespace Ensurance.Constraints
{
    /// <summary>
    /// The Numerics class contains common operations on numeric values.
    /// </summary>
    public static class Numerics
    {
        #region Numeric Type Recognition

        /// <summary>
        /// Checks the type of the object, returning true if
        /// the object is a numeric type.
        /// </summary>
        /// <param name="obj">The object to check</param>
        /// <returns>true if the object is a numeric type</returns>
        public static bool IsNumericType( Object obj )
        {
            return IsFloatingPointNumeric( obj ) || IsFixedPointNumeric( obj );
        }

        /// <summary>
        /// Checks the type of the object, returning true if
        /// the object is a floating point numeric type.
        /// </summary>
        /// <param name="obj">The object to check</param>
        /// <returns>true if the object is a floating point numeric type</returns>
        public static bool IsFloatingPointNumeric( Object obj )
        {
            if ( null != obj )
            {
                if ( obj is double )
                {
                    return true;
                }
                if ( obj is float )
                {
                    return true;
                }

                if ( obj is Double )
                {
                    return true;
                }
                if ( obj is Single )
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks the type of the object, returning true if
        /// the object is a fixed point numeric type.
        /// </summary>
        /// <param name="obj">The object to check</param>
        /// <returns>true if the object is a fixed point numeric type</returns>
        public static bool IsFixedPointNumeric( Object obj )
        {
            if ( null != obj )
            {
                if ( obj is byte )
                {
                    return true;
                }
                if ( obj is sbyte )
                {
                    return true;
                }
                if ( obj is decimal )
                {
                    return true;
                }
                if ( obj is int )
                {
                    return true;
                }
                if ( obj is uint )
                {
                    return true;
                }
                if ( obj is long )
                {
                    return true;
                }
                if ( obj is short )
                {
                    return true;
                }
                if ( obj is ushort )
                {
                    return true;
                }

                if ( obj is Byte )
                {
                    return true;
                }
                if ( obj is SByte )
                {
                    return true;
                }
                if ( obj is Decimal )
                {
                    return true;
                }
                if ( obj is Int32 )
                {
                    return true;
                }
                if ( obj is UInt32 )
                {
                    return true;
                }
                if ( obj is Int64 )
                {
                    return true;
                }
                if ( obj is UInt64 )
                {
                    return true;
                }
                if ( obj is Int16 )
                {
                    return true;
                }
                if ( obj is UInt16 )
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Numeric Equality

        public static bool AreEqual( object expected, object actual, object tolerance )
        {
            CultureInfo currenCulture = CultureInfo.CurrentCulture;
            if ( IsFloatingPointNumeric( expected ) || IsFloatingPointNumeric( actual ) )
            {
                return AreEqual( Convert.ToDouble( expected, currenCulture ), Convert.ToDouble( actual, currenCulture ), Convert.ToDouble( tolerance, currenCulture ) );
            }

            if ( expected is decimal || actual is decimal )
            {
                return AreEqual( Convert.ToDecimal( expected, currenCulture ), Convert.ToDecimal( actual, currenCulture ), Convert.ToDecimal( tolerance, currenCulture ) );
            }

            if ( expected is ulong || actual is ulong )
            {
                return AreEqual( Convert.ToUInt64( expected, currenCulture ), Convert.ToUInt64( actual, currenCulture ), Convert.ToUInt64( tolerance, currenCulture ) );
            }

            if ( expected is long || actual is long )
            {
                return AreEqual( Convert.ToInt64( expected, currenCulture ), Convert.ToInt64( actual, currenCulture ), Convert.ToInt64( tolerance, currenCulture ) );
            }

            if ( expected is uint || actual is uint )
            {
                return AreEqual( Convert.ToUInt32( expected, currenCulture ), Convert.ToUInt32( actual, currenCulture ), Convert.ToUInt32( tolerance, currenCulture ) );
            }

            return AreEqual( Convert.ToInt32( expected, currenCulture ), Convert.ToInt32( actual, currenCulture ), Convert.ToInt32( tolerance, currenCulture ) );
        }

        private static bool AreEqual( double expected, double actual, double tolerance )
        {
            if ( double.IsNaN( expected ) && double.IsNaN( actual ) )
            {
                return true;
            }
            // handle infinity specially since subtracting two infinite values gives 
            // NaN and the following test fails. mono also needs NaN to be handled
            // specially although ms.net could use either method.
            if ( double.IsInfinity( expected ) || double.IsNaN( expected ) || double.IsNaN( actual ) )
            {
                return expected.Equals( actual );
            }

            if ( tolerance > 0.0d )
            {
                return Math.Abs( expected - actual ) <= tolerance;
            }

            return expected.Equals( actual );
        }

        private static bool AreEqual( decimal expected, decimal actual, decimal tolerance )
        {
            if ( tolerance > 0m )
            {
                return Math.Abs( expected - actual ) <= tolerance;
            }

            return expected.Equals( actual );
        }

        private static bool AreEqual( ulong expected, ulong actual, ulong tolerance )
        {
            if ( tolerance > 0ul )
            {
                ulong diff = expected >= actual ? expected - actual : actual - expected;
                return diff <= tolerance;
            }

            return expected.Equals( actual );
        }

        private static bool AreEqual( long expected, long actual, long tolerance )
        {
            if ( tolerance > 0L )
            {
                return Math.Abs( expected - actual ) <= tolerance;
            }

            return expected.Equals( actual );
        }

        private static bool AreEqual( uint expected, uint actual, uint tolerance )
        {
            if ( tolerance > 0 )
            {
                uint diff = expected >= actual ? expected - actual : actual - expected;
                return diff <= tolerance;
            }

            return expected.Equals( actual );
        }

        private static bool AreEqual( int expected, int actual, int tolerance )
        {
            if ( tolerance > 0 )
            {
                return Math.Abs( expected - actual ) <= tolerance;
            }

            return expected.Equals( actual );
        }

        #endregion

        #region Numeric Comparisons 

        public static int Compare( IComparable expected, object actual )
        {
            if ( expected == null )
            {
                throw new ArgumentException( "Cannot compare using a null reference", "expected" );
            }

            if ( actual == null )
            {
                throw new ArgumentException( "Cannot compare to null reference", "actual" );
            }

            if ( IsNumericType( expected ) && IsNumericType( actual ) )
            {
                CultureInfo currenCulture = CultureInfo.CurrentCulture;
                if ( IsFloatingPointNumeric( expected ) || IsFloatingPointNumeric( actual ) )
                {
                    return Convert.ToDouble( expected, currenCulture ).CompareTo( Convert.ToDouble( actual, currenCulture ) );
                }

                if ( expected is decimal || actual is decimal )
                {
                    return Convert.ToDecimal( expected, currenCulture ).CompareTo( Convert.ToDecimal( actual, currenCulture ) );
                }

                if ( expected is ulong || actual is ulong )
                {
                    return Convert.ToUInt64( expected, currenCulture ).CompareTo( Convert.ToUInt64( actual, currenCulture ) );
                }

                if ( expected is long || actual is long )
                {
                    return Convert.ToInt64( expected, currenCulture ).CompareTo( Convert.ToInt64( actual, currenCulture ) );
                }

                if ( expected is uint || actual is uint )
                {
                    return Convert.ToUInt32( expected, currenCulture ).CompareTo( Convert.ToUInt32( actual, currenCulture ) );
                }

                return Convert.ToInt32( expected, currenCulture ).CompareTo( Convert.ToInt32( actual, currenCulture ) );
            }
            else
            {
                return expected.CompareTo( actual );
            }
        }

        #endregion
    }
}