// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegexValidationConfig.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Framework.Core.Helpers.Regex
{
    /// <summary>
    /// Describes regex validation templates.
    /// </summary>
    public static class RegexValidationConfig
    {
        public static Dictionary<RegexTemplates, String> ValidationTemplates = new Dictionary<RegexTemplates, String>()
        {
            { RegexTemplates.UIntValue, "^\\d{1,6}$" },
            { RegexTemplates.PositivIntValue, "^[1-9]\\d{0,6}$" },
            { RegexTemplates.UIntValueOrUnlimited, "(^\\d{1,6}$)|(^2147483647$)" },
            { RegexTemplates.IntValue, "^(-)?\\d{1,6}$" },
            { RegexTemplates.UDoubleValue, "^\\d{1,6}$" },
            { RegexTemplates.DoubleValue, "^(-)?\\d{1,6}$" },
            { RegexTemplates.MoneyValue, @"^[0-9]{1,7}([\.][0-9]{1,2})?$" },
            { RegexTemplates.SizeValue, @"^[0-9]{1,7}([\.][0-9]{1,7})?$" },
            { RegexTemplates.Email, "\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*" },
            { RegexTemplates.Url, "(http://)?([/\\w-]+\\.)+[\\w-]+(/[\\w- ./?%&=]*)?" },
            { RegexTemplates.UrlPart, "^[a-zA-Z0-9-_\\.]+$" },
            { RegexTemplates.ValidName, "^[a-zA-Z0-9\\s\\-'_]+$" },
            { RegexTemplates.FileName, "^[a-zA-Z0-9\\-'_]+$" },
            { RegexTemplates.AlphaNumeric, "^[a-zA-Z0-9]+$" },
            { RegexTemplates.Phone, @"^((\d+)*(\+)*(\s)*(\-)*(\d+)*(\\)*(\/)*)*(\(((\d+)*(\+)*(\s)*(\-)*(\d+)*(\\)*(\/)*)*\))*((\d+)*(\+)*(\s)*(\-)*(\.+)*(\d+)*(\\)*(\/)*)*$" },
            { RegexTemplates.FaxOptional, @"^(((\d+)*(\+)*(\s)*(\-)*(\d+)*(\\)*(\/)*)*(\(((\d+)*(\+)*(\s)*(\-)*(\d+)*(\\)*(\/)*)*\))*((\d+)*(\+)*(\s)*(\-)*(\.+)*(\d+)*(\\)*(\/)*)*)*$" },
        };

        /// <summary>
        /// Gets the regex pattern by template key.
        /// </summary>
        /// <param name="templateKey">The template key.</param>
        /// <returns></returns>
        public static String GetPattern(RegexTemplates templateKey)
        {
            String regex = String.Empty;
            if (ValidationTemplates.ContainsKey(templateKey))
            {
                regex = ValidationTemplates[templateKey];
            }
            return regex;
        }
    }
}
