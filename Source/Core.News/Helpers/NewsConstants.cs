using System;
using System.Globalization;

namespace Core.News.Helpers
{
    public class NewsConstants
    {
        public const string Newsvidgetid = "newsvidgetid";
        public const string Articleid = "articleid";
        public const string CurrentPage = "currpage";

        public const String PagerLink = "<a href='{0}'>{1}</a>";
        public const String Pager= "<div class='pager'>{0}</div>";
        public const String PagerCurrent = "<span class='current'>{0}</span>";

        public const String CurrentRequestParams = "currentRequestParams";
        public const String IsAjaxPageQueryRequestParam = "isAjax";

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