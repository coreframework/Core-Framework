using System;
using System.Collections.Generic;
using Core.Forms.NHibernate.Models;

namespace Core.Forms.Validation
{
    public static class RegexTemplatesConfig
    {
        #region Fields

        public readonly static Dictionary<RegexTemplate, String> ValidationTemplates = new Dictionary<RegexTemplate, String>
                                                                            {
            {RegexTemplate.PositiveIntValue, "^[1-9]\\d{0,6}$"},
            {RegexTemplate.IntValue, "^(-)?\\d{1,6}$"},
            {RegexTemplate.DoubleValue, "^(-)?\\d{1,6}$"},
            {RegexTemplate.MoneyValue, @"^[0-9]{1,7}([\.][0-9]{1,2})?$"},
            {RegexTemplate.Email, "\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*"},
            {RegexTemplate.Url, "(http://)?([/\\w-]+\\.)+[\\w-]+(/[\\w- ./?%&=]*)?"},
            {RegexTemplate.UrlPart, "^[a-zA-Z0-9-_\\.]+$"},
            {RegexTemplate.AlphaNumeric, "^[a-zA-Z0-9]+$"},
            {RegexTemplate.Phone, @"^((\d+)*(\+)*(\s)*(\-)*(\d+)*(\\)*(\/)*)*(\(((\d+)*(\+)*(\s)*(\-)*(\d+)*(\\)*(\/)*)*\))*((\d+)*(\+)*(\s)*(\-)*(\.+)*(\d+)*(\\)*(\/)*)*$"},
        };

        #endregion
    }
}