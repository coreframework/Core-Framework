using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

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

        public static string PageUrl(this HtmlHelper helper, object values)
        {
            var valuesDictionary = new RouteValueDictionary(values);
            valuesDictionary = GetParams(valuesDictionary);

            if (valuesDictionary.ContainsKey("url"))
                return valuesDictionary["url"].ToString();

            return String.Empty;
        }

        public static string DetailsLink(this HtmlHelper helper, string linkText, long widgetId, long modelId, object values)
        {
            var valuesDictionary = new RouteValueDictionary(values);
            valuesDictionary = GetParams(valuesDictionary);

            var routeParams = String.Empty;
            foreach (var key in valuesDictionary.Keys.Where(key => key.StartsWith(NewsConstants.CurrentPage) || key.StartsWith(NewsConstants.Articleid)))
            {
                if (routeParams.Equals(String.Empty))
                    routeParams += "?";
                else
                    routeParams += "&";
                routeParams += key + "=" + valuesDictionary[key];
            }
            routeParams = String.IsNullOrEmpty(routeParams) ? "?" : routeParams + "&";
            if (valuesDictionary.ContainsKey("url"))
                return @"<a href=""" + valuesDictionary["url"] + routeParams + NewsConstants.Articleid + widgetId + "=" + modelId + "\">" + linkText + "</a>";

            return String.Empty;
        }

        private static RouteValueDictionary GetParams(RouteValueDictionary valuesDictionary)
        {
            if (valuesDictionary.ContainsKey(NewsConstants.CurrentRequestParams))
            {
                var values = valuesDictionary[NewsConstants.CurrentRequestParams];

                var routeValueDictionary = values as NameValueCollection;
                if (routeValueDictionary != null)
                    foreach (string key in routeValueDictionary.Keys)
                    {
                        if (key.StartsWith(NewsConstants.CurrentPage))
                            valuesDictionary.Add(key, routeValueDictionary[key]);
                        if (key.StartsWith(NewsConstants.Articleid))
                            valuesDictionary.Add(key, routeValueDictionary[key]);
                        if (key.ToLower().Equals("url"))
                        {
                            if (valuesDictionary.ContainsKey(NewsConstants.IsAjaxPageQueryRequestParam) && (bool)valuesDictionary[NewsConstants.IsAjaxPageQueryRequestParam] && !String.IsNullOrEmpty(routeValueDictionary["HTTP_REFERER"]))
                                valuesDictionary.Add(key, routeValueDictionary["HTTP_REFERER"].Split('?')[0]);
                            else
                                valuesDictionary.Add(key, routeValueDictionary[key]);
                        }

                    }
                valuesDictionary.Remove(NewsConstants.CurrentRequestParams);
            }
            return valuesDictionary;
        }
    }



}
