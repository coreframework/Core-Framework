namespace Core.Framework.MEF.Throw
{
    using System;
    using System.Configuration;
    using System.Diagnostics;

    /// <summary>
    /// Throws handled exceptions.
    /// </summary>
    public static partial class Throw
    {
        #region Methods
        /// <summary>
        /// Throws the specified exception.
        /// </summary>
        /// <param name="exception">The exception to be thrown.</param>
        /// <param name="modifier">A modifier delegate used to modify the exception before being thrown.</param>
        [DebuggerStepThrough]
        private static void ThrowInternal(Exception exception, Func<Exception, Exception> modifier = null)
        {
            if (exception == null)
                return;

            Exception ex = null;
            if (modifier != null)
                ex = modifier(exception);

            /* We should never try and suppress an exception at this point, so make sure the original
             * exception is thrown if the modifier function returns null. */
            if (ex == null)
                ex = exception;

            throw ex;
        }

        /// <summary>
        /// Throws a <see cref="NotSupportedException" /> with the specified message.
        /// </summary>
        /// <param name="message">The message to throw.</param>
        /// <param name="modifier">A modifier delegate used to modify the exception before being thrown.</param>
        public static void NotSupported(String message, Func<Exception, Exception> modifier = null)
        {
            ThrowInternal(new NotSupportedException(message), modifier);
        }

        /// <summary>
        /// Throws a <see cref="InvalidOperationException" /> with the specified message.
        /// </summary>
        /// <param name="message">The message to throw.</param>
        /// <param name="modifier">A modifier delegate used to modify the exception before being thrown.</param>
        public static void InvalidOperation(String message, Func<Exception, Exception> modifier = null)
        {
            ThrowInternal(new InvalidOperationException(message), modifier);
        }

        /// <summary>
        /// Throws a <see cref="ConfigurationErrorsException" /> with the specified message.
        /// </summary>
        /// <param name="message">The message to throw.</param>
        /// <param name="modifier">A modifier delegate used to modify the exception before being thrown.</param>
        public static void ConfigurationErrors(String message, Func<Exception, Exception> modifier = null)
        {
            ThrowInternal(new ConfigurationErrorsException(message), modifier);
        }
        #endregion
    }
}
