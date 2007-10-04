using System.Globalization;
using System.IO;
using Ensurance.Constraints;

namespace Ensurance.MessageWriters
{
    /// <summary>
    /// 
    /// </summary>
    public class ExceptionEnsuranceHandler : TextMessageWriter
    {
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
        public override void Handle( Constraint constraint, string message, params object[] args )
        {
            try
            {
                MessageWriter messageWriter = new TextMessageWriter( new StringWriter( CultureInfo.CurrentCulture ) );
                constraint.WriteMessageTo( messageWriter );
                throw new EnsuranceException( messageWriter.ToString() );
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
    }
}