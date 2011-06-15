using System;
using System.Collections.Generic;

namespace Core.Forms.Validation
{
    public class RegexTemplatesConfig
    {
        public Dictionary<RegexTemplates, String> ValidationTemplates = new Dictionary<RegexTemplates, String>
                                                                            {
            {RegexTemplates.PositivIntValue, "^[1-9]\\d{0,6}$"},
            {RegexTemplates.IntValue, "^(-)?\\d{1,6}$"},
            {RegexTemplates.DoubleValue, "^(-)?\\d{1,6}$"},
            {RegexTemplates.MoneyValue, @"^[0-9]{1,7}([\.][0-9]{1,2})?$"},
            {RegexTemplates.Email, "\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*"},
            {RegexTemplates.Url, "(http://)?([/\\w-]+\\.)+[\\w-]+(/[\\w- ./?%&=]*)?"},
            {RegexTemplates.UrlPart, "^[a-zA-Z0-9-_\\.]+$"},
            {RegexTemplates.AlphaNumeric, "^[a-zA-Z0-9]+$"},
            {RegexTemplates.Phone, @"^((\d+)*(\+)*(\s)*(\-)*(\d+)*(\\)*(\/)*)*(\(((\d+)*(\+)*(\s)*(\-)*(\d+)*(\\)*(\/)*)*\))*((\d+)*(\+)*(\s)*(\-)*(\.+)*(\d+)*(\\)*(\/)*)*$"},
        };
    }
}