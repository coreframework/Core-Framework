// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Inflector.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Core.Helpers
{
    /// <summary>
    /// The Inflector class transforms words from one form to another. 
    /// For example, from singular to plural.
    /// </summary>
    public static class Inflector
    {
        #region Fields

        private const String WholeWordTemplate = "({0}){1}$";

        private const String WholeWordReplacement = "$1";

        private static readonly IList<TransformationRule> plurals = new List<TransformationRule>();

        private static readonly IList<TransformationRule> singulars = new List<TransformationRule>();

        private static readonly IList<String> uncountables = new List<String>();

        #endregion

        #region Default Rules

        /// <summary>
        /// Initializes static members of the <see cref="Inflector"/> class. 
        /// </summary>
        static Inflector()
        {
            AddPlural("$", "s");
            AddPlural("s$", "s");
            AddPlural("(ax|test)is$", "$1es");
            AddPlural("(octop|vir)us$", "$1i");
            AddPlural("(alias|status)$", "$1es");
            AddPlural("(bu)s$", "$1ses");
            AddPlural("(buffal|tomat)o$", "$1oes");
            AddPlural("([ti])um$", "$1a");
            AddPlural("sis$", "ses");
            AddPlural("(?:([^f])fe|([lr])f)$", "$1$2ves");
            AddPlural("(hive)$", "$1s");
            AddPlural("([^aeiouy]|qu)y$", "$1ies");
            AddPlural("(x|ch|ss|sh)$", "$1es");
            AddPlural("(matr|vert|ind)ix|ex$", "$1ices");
            AddPlural("([m|l])ouse$", "$1ice");
            AddPlural("^(ox)$", "$1en");
            AddPlural("(quiz)$", "$1zes");

            AddSingular("s$", String.Empty);
            AddSingular("(n)ews$", "$1ews");
            AddSingular("([ti])a$", "$1um");
            AddSingular("((a)naly|(b)a|(d)iagno|(p)arenthe|(p)rogno|(s)ynop|(t)he)ses$", "$1$2sis");
            AddSingular("(^analy)ses$", "$1sis");
            AddSingular("([^f])ves$", "$1fe");
            AddSingular("(hive)s$", "$1");
            AddSingular("(tive)s$", "$1");
            AddSingular("([lr])ves$", "$1f");
            AddSingular("([^aeiouy]|qu)ies$", "$1y");
            AddSingular("(s)eries$", "$1eries");
            AddSingular("(m)ovies$", "$1ovie");
            AddSingular("(x|ch|ss|sh)es$", "$1");
            AddSingular("([m|l])ice$", "$1ouse");
            AddSingular("(bus)es$", "$1");
            AddSingular("(o)es$", "$1");
            AddSingular("(shoe)s$", "$1");
            AddSingular("(cris|ax|test)es$", "$1is");
            AddSingular("(octop|vir)i$", "$1us");
            AddSingular("(alias|status)es$", "$1");
            AddSingular("^(ox)en", "$1");
            AddSingular("(vert|ind)ices$", "$1ex");
            AddSingular("(matr)ices$", "$1ix");
            AddSingular("(quiz)zes$", "$1");

            AddIrregular("person", "people");
            AddIrregular("man", "men");
            AddIrregular("child", "children");
            AddIrregular("sex", "sexes");
            AddIrregular("move", "moves");

            AddUncountable("equipment");
            AddUncountable("information");
            AddUncountable("rice");
            AddUncountable("money");
            AddUncountable("species");
            AddUncountable("series");
            AddUncountable("fish");
            AddUncountable("sheep");
        }

        #endregion

        /// <summary>
        /// Return the plural of a word.
        /// </summary>
        /// <param name="word">The singular form.</param>
        /// <returns>The plural form of <paramref name="word"/>.</returns>
        public static String Pluralize(String word)
        {
            return ApplyRules(plurals, word);
        }

        /// <summary>
        /// Return the singular of a word.
        /// </summary>
        /// <param name="word">The plural form.</param>
        /// <returns>The singular form of <paramref name="word"/>.</returns>
        public static String Singularize(String word)
        {
            return ApplyRules(singulars, word);
        }

        #region Helper methods

        private static void AddIrregular(String singular, String plural)
        {
            AddPlural(String.Format(WholeWordTemplate, singular[0], singular.Substring(1)), WholeWordReplacement + plural.Substring(1));
            AddPlural(String.Format(WholeWordTemplate, plural[0], plural.Substring(1)), WholeWordReplacement + singular.Substring(1));
        }

        private static void AddUncountable(String word)
        {
            uncountables.Add(word.ToLower());
        }

        private static void AddPlural(String rule, String replacement)
        {
            plurals.Add(new TransformationRule(rule, replacement));
        }

        private static void AddSingular(String rule, String replacement)
        {
            singulars.Add(new TransformationRule(rule, replacement));
        }

        private static String ApplyRules(IEnumerable<TransformationRule> rules, String word)
        {
            String result = word;

            if (!uncountables.Contains(word.ToLower()))
            {
                foreach (var rule in rules.Reverse())
                {
                    if ((result = rule.Apply(word)) != null)
                    {
                        break;
                    }
                }
            }

            return result;
        }

        #endregion
    }
}