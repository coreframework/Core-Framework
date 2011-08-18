using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Core.Web.Helpers
{
    public static class ConstantsHelper
    {
        public static string JqueryDateFormat
        {
            get
            {
                return CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern.Replace("M", "m").Replace("yy", "y");
            }
        }

        public static string DateFormat
        {
            get
            {
                return CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
            }
        }
    }
}