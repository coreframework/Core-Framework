using System;
using System.Web.Mvc;

namespace Core.News.Helpers
{
    public static class HtmlExtensions
    {
        /// <summary>
        /// Render pager
        /// </summary>
        /// <param name="helper">The html helper</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <param name="currentPage">The current page</param>
        /// <param name="totalItemCount">Total items count</param>
        /// <param name="widgetId">Model id (for several pager in page)</param>
        /// <param name="url">Base URL</param>
        /// <returns></returns>
        public static string Pager(this HtmlHelper helper, int pageSize, int currentPage, int totalItemCount, long widgetId, string url)
        {
            int totalPages = (int)(totalItemCount - 0.5)/pageSize + 1;
            if (totalPages == 1)
                return String.Empty;

            url = url.Replace("&" + NewsConstants.CurrentPage + widgetId + "=" + currentPage, "");
            url = url.Replace("?" + NewsConstants.Newsvidgetid + "=" + widgetId, "");
            if (!url.Contains("?"))
            {
                var index = url.IndexOf("&");
                if (index > 0)
                {
                    var tempUrl = url.ToCharArray();
                    tempUrl[index] = '?';
                    url = new string(tempUrl);
                }
            }
            else
            {
                url = url.Replace("&" + NewsConstants.Newsvidgetid + "=" + widgetId, "");
            }


            var htmlControl = String.Empty;
            if (currentPage != 0)
                htmlControl += String.Format(NewsConstants.PagerLink,
                                             url + (url.Contains("?") ? "&" : "?") + NewsConstants.Newsvidgetid + "=" +
                                             widgetId + "&" +
                                             NewsConstants.CurrentPage + widgetId + "=" + (currentPage - 1), " < ");
            for (var i = 0; i < totalPages; i++ )
            {
                if (i != currentPage)
                    htmlControl += String.Format(NewsConstants.PagerLink,
                                                 url + (url.Contains("?") ? "&" : "?") + NewsConstants.Newsvidgetid +
                                                 "=" + widgetId + "&" +
                                                 NewsConstants.CurrentPage + widgetId + "=" + (i), i + 1);
                else
                    htmlControl += String.Format(NewsConstants.PagerCurrent, i + 1);
            }
            if (currentPage != totalPages - 1)
                htmlControl += String.Format(NewsConstants.PagerLink,
                                             url + (url.Contains("?") ? "&" : "?") + NewsConstants.Newsvidgetid + "=" +
                                             widgetId + "&" +
                                             NewsConstants.CurrentPage + widgetId + "=" + (currentPage + 1), " > ");
            return string.Format(NewsConstants.Pager, htmlControl);
        }
    }

}
