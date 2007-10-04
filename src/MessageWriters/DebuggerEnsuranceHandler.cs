using System.Diagnostics;
using Ensurance.Constraints;

namespace Ensurance.MessageWriters
{
    [DebuggerNonUserCode]
    public class DebuggerEnsuranceHandler : IEnsuranceResponsibilityChainLink
    {
        private IEnsuranceResponsibilityChainLink _successor;

        #region IEnsuranceResponsibilityChainLink Members

        /// <summary>
        /// Gets or sets the successor.
        /// </summary>
        /// <value>The successor.</value>
        public IEnsuranceResponsibilityChainLink Successor
        {
            get { return _successor; }
            set { _successor = value; }
        }

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
        public void Handle( Constraint constraint, string message, params object[] args )
        {
            try
            {
                if ( Debugger.IsAttached )
                {
                    Debugger.Break();
                }
            }
            finally
            {
                IEnsuranceResponsibilityChainLink handler = _successor;
                if ( handler != null )
                {
                    handler.Handle( constraint, message, args );
                }
            }
        }

        #endregion
    }
}