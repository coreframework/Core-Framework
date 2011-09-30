using System;
using System.Globalization;

namespace Core.Framework.MEF.Extensions
{
    /// <summary>
    /// Provides extension methods for the <see cref="String" /> type.
    /// </summary>
    public static class StringExtensions
    {
        #region Methods
        /// <summary>
        /// Formats the given String using the specified culture and arguments.
        /// </summary>
        /// <param name="string">The String to format.</param>
        /// <param name="arguments">The arguments used to format the string.</param>
        /// <exception cref="ArgumentNullException">If the input string, or any arguments are null.</exception>
        /// <exception cref="FormatException">If the input String is invalid, or the index of a format item is less than zero, or greater than or equal to the length of the args array.</exception>
        public static String Format(this String @string, params object[] arguments)
        {
            return Format(@string, CultureInfo.CurrentUICulture, arguments);
        }

        /// <summary>
        /// Formats the given String using the specified culture and arguments.
        /// </summary>
        /// <param name="string">The String to format.</param>
        /// <param name="culture">The culture used to format the string.</param>
        /// <param name="arguments">The arguments used to format the string.</param>
        /// <exception cref="ArgumentNullException">If the input string, or any arguments are null.</exception>
        /// <exception cref="FormatException">If the input String is invalid, or the index of a format item is less than zero, or greater than or equal to the length of the args array.</exception>
        public static String Format(this String @string, CultureInfo culture, params object[] arguments)
        {
            return String.Format(culture, @string, arguments);
        }
        #endregion
    }
}