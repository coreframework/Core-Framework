// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Text.RegularExpressions;
using Framework.Core.Helpers;

namespace Framework.Core.Extensions
{
    /// <summary>
    /// Extends <see cref="string"/> class functionality.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Capitalizes a word.
        /// </summary>
        /// <param name="word">The word to be capitalized.</param>
        /// <returns>Capitalized <paramref name="word"/>.</returns>
        public static String Capitalize(this String word)
        {
            return word.Substring(0, 1).ToUpperInvariant() + word.Substring(1).ToLower();
        }

        /// <summary>
        /// Return the plural of a word.
        /// </summary>
        /// <param name="word">The singular form.</param>
        /// <returns>The plural form of <paramref name="word"/>.</returns>
        public static String Pluralize(this String word)
        {
            return Inflector.Pluralize(word);
        }

        /// <summary>
        /// Return the singular of a word.
        /// </summary>
        /// <param name="word">The plural form.</param>
        /// <returns>The singular form of <paramref name="word"/>.</returns>
        public static String Singularize(this String word)
        {
            return Inflector.Singularize(word);
        }

        /// <summary>
        /// Translate camelCase or PascalCase property name to human readable name (splits it to words and capitilize first word).
        /// </summary>
        /// <param name="propertyName">Property name in camelCase or PascalCase.</param>
        /// <returns>Human readable property name.</returns>
        public static String Humanize(this String propertyName)
        {
            return Regex.Replace(propertyName, "(\\p{Lu})", " $1").Trim().Capitalize();
        }
    }
}