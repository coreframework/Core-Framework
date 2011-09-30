using System;
using System.Globalization;

namespace Core.Web.Helpers
{
    public static class ConstantsHelper
    {
        public static String JqueryDateFormat
        {
            get
            {
                return CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern.Replace("M", "m").Replace("yy", "y");
            }
        }

        public static String DateFormat
        {
            get
            {
                return CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
            }
        }
    }
}