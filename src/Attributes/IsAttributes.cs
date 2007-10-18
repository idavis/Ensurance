using System;

namespace Ensurance.Attributes
{
    public class Iz : Is
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public partial class Is
    {
        #region Nested type: Empty

        /// <summary>
        /// 
        /// </summary>
        public class Empty : ConstraintAttribute
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Empty"/> class.
            /// </summary>
            public Empty()
            {
                _constraint = SyntaxHelpers.Is.Empty;
            }
        }

        #endregion

        #region Nested type: False

        /// <summary>
        /// 
        /// </summary>
        public class False : ConstraintAttribute
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="False"/> class.
            /// </summary>
            public False()
            {
                _constraint = SyntaxHelpers.Is.False;
            }
        }

        #endregion

        #region Nested type: NaN

        /// <summary>
        /// 
        /// </summary>
        public class NaN : ConstraintAttribute
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="NaN"/> class.
            /// </summary>
            public NaN()
            {
                _constraint = SyntaxHelpers.Is.NaN;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="NaN"/> class.
            /// </summary>
            /// <param name="tolerance">The tolerance.</param>
            public NaN( object tolerance )
                : this()
            {
                _constraint.Tolerance = tolerance;
            }
        }

        #endregion

        #region Nested type: Null

        /// <summary>
        /// 
        /// </summary>
        public class Null : ConstraintAttribute
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Null"/> class.
            /// </summary>
            public Null()
            {
                _constraint = SyntaxHelpers.Is.Null;
            }
        }

        #endregion

        #region Nested type: True

        /// <summary>
        /// 
        /// </summary>
        public class True : ConstraintAttribute
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="True"/> class.
            /// </summary>
            public True()
            {
                _constraint = SyntaxHelpers.Is.True;
            }
        }

        #endregion

        #region Nested type: Unique

        /// <summary>
        /// 
        /// </summary>
        public class Unique : ConstraintAttribute
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Unique"/> class.
            /// </summary>
            public Unique()
            {
                _constraint = SyntaxHelpers.Is.Unique;
            }
        }

        #region Comparison Constraints

        #region Nested type: AtLeast

        /// <summary>
        /// 
        /// </summary>
        public class AtLeast : GreaterThanOrEqualTo
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="AtLeast"/> class.
            /// </summary>
            /// <param name="expected">The expected.</param>
            public AtLeast( IComparable expected ) : base( expected )
            {
            }
        }

        #endregion

        #region Nested type: AtMost

        /// <summary>
        /// 
        /// </summary>
        public class AtMost : LessThanOrEqualTo
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="AtMost"/> class.
            /// </summary>
            /// <param name="expected">The expected.</param>
            public AtMost( IComparable expected ) : base( expected )
            {
            }
        }

        #endregion

        #region Nested type: GreaterThan

        /// <summary>
        /// 
        /// </summary>
        public class GreaterThan : ConstraintAttribute
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="GreaterThan"/> class.
            /// </summary>
            /// <param name="expected">The expected.</param>
            public GreaterThan( IComparable expected )
            {
                _constraint = SyntaxHelpers.Is.GreaterThan( expected );
            }
        }

        #endregion

        #region Nested type: GreaterThanOrEqualTo

        /// <summary>
        /// 
        /// </summary>
        public class GreaterThanOrEqualTo : ConstraintAttribute
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="GreaterThanOrEqualTo"/> class.
            /// </summary>
            /// <param name="expected">The expected.</param>
            public GreaterThanOrEqualTo( IComparable expected )
            {
                _constraint = SyntaxHelpers.Is.GreaterThanOrEqualTo( expected );
            }
        }

        #endregion

        #region Nested type: LessThan

        /// <summary>
        /// 
        /// </summary>
        public class LessThan : ConstraintAttribute
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="LessThan"/> class.
            /// </summary>
            /// <param name="expected">The expected.</param>
            public LessThan( IComparable expected )
            {
                _constraint = SyntaxHelpers.Is.LessThan( expected );
            }
        }

        #endregion

        #region Nested type: LessThanOrEqualTo

        /// <summary>
        /// 
        /// </summary>
        public class LessThanOrEqualTo : ConstraintAttribute
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="LessThanOrEqualTo"/> class.
            /// </summary>
            /// <param name="expected">The expected.</param>
            public LessThanOrEqualTo( IComparable expected )
            {
                _constraint = SyntaxHelpers.Is.LessThanOrEqualTo( expected );
            }
        }

        #endregion

        #endregion Comparison Constraints

        #region Type Constraints

        #region Nested type: AssignableFrom

        /// <summary>
        /// 
        /// </summary>
        public class AssignableFrom : ConstraintAttribute
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="AssignableFrom"/> class.
            /// </summary>
            /// <param name="expected">The expected.</param>
            public AssignableFrom( Type expected )
            {
                _constraint = SyntaxHelpers.Is.AssignableFrom( expected );
            }
        }

        #endregion

        #region Nested type: InstanceOfType

        /// <summary>
        /// 
        /// </summary>
        public class InstanceOfType : ConstraintAttribute
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="InstanceOfType"/> class.
            /// </summary>
            /// <param name="expected">The expected.</param>
            public InstanceOfType( Type expected )
            {
                _constraint = SyntaxHelpers.Is.InstanceOfType( expected );
            }
        }

        #endregion

        #region Nested type: TypeOf

        /// <summary>
        /// 
        /// </summary>
        public class TypeOf : ConstraintAttribute
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="TypeOf"/> class.
            /// </summary>
            /// <param name="expected">The expected.</param>
            public TypeOf( Type expected )
            {
                _constraint = SyntaxHelpers.Is.TypeOf( expected );
            }
        }

        #endregion

        #endregion

        #endregion
    }
}