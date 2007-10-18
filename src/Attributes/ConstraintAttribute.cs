using System;
using Ensurance.Constraints;

namespace Ensurance.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage( AttributeTargets.Parameter, Inherited = false, AllowMultiple = true )]
    public abstract class ConstraintAttribute : Attribute
    {
        protected Constraint _constraint;

        /// <summary>
        /// Gets the constraint.
        /// </summary>
        /// <value>The constraint.</value>
        public Constraint Constraint
        {
            get { return _constraint; }
        }
    }
}