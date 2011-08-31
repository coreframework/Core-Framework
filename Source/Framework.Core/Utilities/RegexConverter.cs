using System;

namespace Framework.Core.Utilities
{
    /// <summary>
    /// Regex converter.
    /// </summary>
    public static class RegexConverter
    {
        #region Fields

        private const String AsterixFilterExpression = "*";

        private const String AsterixRegexExpression = " \\.*";

        private const String StartEndRegexExpression = "^{0}$";

        #endregion

        #region Methods

        /// <summary>
        /// Converts filter expression (i.e. *.txt) to regex expression.
        /// </summary>
        /// <param name="filterExpression">The filter expression.</param>
        /// <returns>The converted expression.</returns>
        public static String FilterToRegex(String filterExpression)
        {
            filterExpression.Replace(AsterixFilterExpression, AsterixRegexExpression);

            //add string start&end expression
            filterExpression = String.Format(StartEndRegexExpression, filterExpression);

            return filterExpression;
        }

        #endregion
    }
}
