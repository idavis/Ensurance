using System.Diagnostics;
using Ensurance.Constraints;
using Ensurance.MessageWriters;

namespace Ensurance
{
    /// <summary>
    /// 
    /// </summary>
    [DebuggerNonUserCode]
    public class EnsureWithThrow : EnsureBase<EnsureWithThrow>, IEnsuranceHandler
    {
        private static readonly IEnsuranceHandler _handler = new ExceptionEnsuranceHandler();

        /// <summary>
        /// Initializes a new instance of the <see cref="EnsureWithThrow"/> class.
        /// </summary>
        protected EnsureWithThrow()
        {
        }

        #region IEnsuranceHandler Members

        /// <summary>
        /// Handles an Ensurance failure for the given constraint. Implementors
        /// should always call 
        /// <code>
        /// if( successor != null)
        /// {
        ///     successor.Handle( constraint, message, args );
        /// }
        /// </code>
        /// So that the downstream handler can have a chance to process the failure.
        /// </summary>
        /// <param name="constraint">The constraint.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The args.</param>
        void IEnsuranceHandler.Handle( Constraint constraint, string message, params object[] args )
        {
            Handle( constraint, message, args );
        }

        #endregion

        /// <summary>
        /// Handles an Ensurance failure for the given constraint. Implementors
        /// should always call
        /// <code>
        /// if( successor != null)
        /// {
        /// successor.Handle( constraint, message, args );
        /// }
        /// </code>
        /// So that the downstream handler can have a chance to process the failure.
        /// </summary>
        /// <param name="constraint">The constraint.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The args.</param>
        private static void Handle( Constraint constraint, string message, params object[] args )
        {
            _handler.Handle( constraint, message, args );
        }
    }
}