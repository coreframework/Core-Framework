// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TransformationRule.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Text.RegularExpressions;

namespace Framework.Core.Helpers
{
    /// <summary>
    /// Specified word transformation rule (regular expression pattern + replacment rule).
    /// </summary>
    /// <example>
    /// Transformation rules can be used for word plurilizing, f.e. rule:
    /// <code>new TransformationRule("sis$", "ses");</code>
    /// specified the rule that replaces end of word ends with "sis" to "ses".
    /// </example>
    public class TransformationRule
    {
        private readonly Regex regex;

        private readonly String replacement;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransformationRule"/> class.
        /// </summary>
        /// <param name="pattern">The pattern.</param>
        /// <param name="replacement">The replacement.</param>
        public TransformationRule(String pattern, String replacement)
        {
            regex = new Regex(pattern, RegexOptions.IgnoreCase);
            this.replacement = replacement;
        }

        /// <summary>
        /// Applies rule to word.
        /// </summary>
        /// <param name="word">The word to transform.</param>
        /// <returns>Transformed rule if rule applied to word specified or null otherwise.</returns>
        public String Apply(String word)
        {
            String result = null;

            if (regex.IsMatch(word))
            {
            result = regex.Replace(word, replacement);
            }

            return result;
        }
    }
}