using System;
using System.Globalization;

namespace Core.News.Helpers
{
    public static class NewsConstants
    {
        public static readonly String Newsvidgetid = "newsvidgetid";
        public static readonly String Articleid = "articleid";
        public static readonly String CurrentPage = "currpage";

        public static readonly String PagerLink = "<a href='{0}'>{1}</a>";
        public static readonly String Pager= "<div class='pager'>{0}</div>";
        public static readonly String PagerCurrent = "<span class='current'>{0}</span>";

        public static readonly String CurrentRequestParams = "currentRequestParams";
        public static readonly String IsAjaxPageQueryRequestParam = "isAjax";

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