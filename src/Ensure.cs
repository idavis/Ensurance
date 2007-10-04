using System.Collections.Generic;
using System.Diagnostics;
using Ensurance.Constraints;
using Ensurance.SyntaxHelpers;

namespace Ensurance
{
    [DebuggerNonUserCode]
    public class Ensure : EnsureBase<Ensure>, IEnsuranceHandler
    {
        #region EnsurenceHandler

        private static IEnsuranceResponsibilityChainLink _handler;

        /// <summary>
        /// Gets or sets the handler.
        /// </summary>
        /// <value>The handler.</value>
        public static IEnsuranceHandler Handler
        {
            get { return _handler; }
        }

        /// <summary>
        /// Sets the ensurance handlers.
        /// </summary>
        /// <param name="handlersList">The handlers.</param>
        public static void SetEnsuranceHandlers( IList<IEnsuranceResponsibilityChainLink> handlersList )
        {
            That( handlersList, Is.Not.Null );
            That( handlersList.Count, Is.GreaterThan( 0 ) );
            ProcessEnsuranceHandlers( handlersList );
        }

        /// <summary>
        /// Sets the ensurance handlers.
        /// </summary>
        /// <param name="handlersList">The handlers list.</param>
        public static void SetEnsuranceHandlers( params IEnsuranceResponsibilityChainLink[] handlersList )
        {
            That( handlersList.Length, Is.GreaterThan( 0 ) );
            ProcessEnsuranceHandlers( handlersList );
        }

        /// <summary>
        /// Processes the ensurance handlers.
        /// </summary>
        /// <param name="handlersList">The handlers list.</param>
        public static void ProcessEnsuranceHandlers( IList<IEnsuranceResponsibilityChainLink> handlersList )
        {
            _handler = handlersList[0];
            IEnsuranceResponsibilityChainLink current = _handler;
            for (int i = 1; i < handlersList.Count; i++)
            {
                current.Successor = handlersList[i];
                current = current.Successor;
            }
        }

        #endregion

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