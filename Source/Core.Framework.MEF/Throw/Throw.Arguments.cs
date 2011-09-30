using System;
using System.Diagnostics;
using System.Globalization;

namespace Core.Framework.MEF.Throw
{
    public static partial class Throw
    {
        #region Methods
        /// <summary>
        /// Throws an <see cref="ArgumentNullException" /> if the specified argument is null.
        /// </summary>
        /// <param name="argument">The argument to check.</param>
        /// <param name="argumentName">The name of the argument.</param>
        /// <param name="modifier">A modifier delegate used to modify the exception before being thrown.</param>
        [DebuggerStepThrough]
        public static void IfArgumentNull(object argument, String argumentName, Func<Exception, Exception> modifier = null)
        {
            if (argument == null)
                ThrowInternal(
                    new ArgumentNullException(argumentName),
                    modifier);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException" /> if the specified argument is null or equal to <see cref="String.Empty" />.
        /// </summary>
        /// <param name="argument">The argument to check.</param>
        /// <param name="argumentName">The name of the argument.</param>
        /// <param name="modifier">A modifier delegate used to modify the exception before being thrown.</param>
        [DebuggerStepThrough]
        public static void IfArgumentNullOrEmpty(String argument, String argumentName, Func<Exception, Exception> modifier = null)
        {
            if (String.IsNullOrEmpty(argument))
                ThrowInternal(
                    new ArgumentException(
                        String.Format(CultureInfo.CurrentUICulture, Resources.Throw.ArgumentNullOrEmpty, argumentName)),
                    modifier);
        }
        #endregion
    }
}