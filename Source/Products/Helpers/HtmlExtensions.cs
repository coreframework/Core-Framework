using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace Products.Helpers
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
        /// <param name="id">Model id (for several pager in page)</param>
        /// <param name="values">Request params</param>
        /// <returns></returns>
        public static string Pager(this HtmlHelper helper, int pageSize, int currentPage, int totalItemCount, long id, object values)
        {
            return Pager(helper, id, pageSize, currentPage, totalItemCount, String.Empty, new RouteValueDictionary(values));
        }

        /// <summary>
        /// Render pager
        /// </summary>
        /// <param name="helper">The html helper</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <param name="currentPage">The current page</param>
        /// <param name="totalItemCount">Total items count</param>
        /// <param name="id">Model id (for several pager in page)</param>
        /// <param name="preffix">Part of route values name</param>
        /// <param name="values">Request params</param>
        /// <returns></returns>
        public static string Pager(this HtmlHelper helper, int pageSize, int currentPage, int totalItemCount, long id, string preffix, object values)
        {
            return Pager(helper, id, pageSize, currentPage, totalItemCount, preffix, new RouteValueDictionary(values));
        }

        public static string Pager(this HtmlHelper helper, long id, int pageSize, int currentPage, int totalItemCount, string preffix, RouteValueDictionary valuesDictionary)
        {
            if (valuesDictionary == null)
            {
                valuesDictionary = new RouteValueDictionary();
            }
            valuesDictionary = GetParams(valuesDictionary);
            var pager = new Pager(helper, id, pageSize, currentPage, totalItemCount, preffix, valuesDictionary);
            return pager.RenderHtml();
        }

        public static string DetailsLink(this HtmlHelper helper, string linkText, long widgetId, long modelId, object values)
        {
            var valuesDictionary = new RouteValueDictionary(values);
            valuesDictionary = GetParams(valuesDictionary);

            var routeParams = String.Empty;
            foreach (var key in valuesDictionary.Keys.Where(key => key.StartsWith(ProductConstants.CurrentPageQueryRequestParam) || key.StartsWith(ProductConstants.ProductIdQueryRequestParam)))
            {
                if (routeParams.Equals(String.Empty))
                    routeParams += "?";
                else
                    routeParams += "&";
                routeParams += key + "=" + valuesDictionary[key];
            }
            routeParams = String.IsNullOrEmpty(routeParams) ? "?" : routeParams + "&";
            if (valuesDictionary.ContainsKey("url"))
                return @"<a href=""" + valuesDictionary["url"] + routeParams + ProductConstants.ProductIdQueryRequestParam + widgetId + "=" + modelId + "\">" + linkText + "</a>";

            return String.Empty;
        }

        public static string ListLink(this HtmlHelper helper, string linkText, long widgetId, object values)
        {
            var valuesDictionary = new RouteValueDictionary(values);
            valuesDictionary = GetParams(valuesDictionary);

            var routeParams = String.Empty;
            foreach (var key in valuesDictionary.Keys.Where(key => key.StartsWith(ProductConstants.CurrentPageQueryRequestParam) || key.StartsWith(ProductConstants.ProductIdQueryRequestParam)))
            {
                if (routeParams.Equals(String.Empty))
                    routeParams += "?";
                else if (!routeParams.Equals("?"))
                    routeParams += "&";

                if (!key.Equals(ProductConstants.ProductIdQueryRequestParam + widgetId))
                    routeParams += key + "=" + valuesDictionary[key];
                else
                    routeParams = routeParams.TrimEnd('&');
            }
            if (valuesDictionary.ContainsKey("url"))
                return @"<a href=""" + valuesDictionary["url"] + routeParams + "\">" + linkText + "</a>";
            return String.Empty;
        }

        private static RouteValueDictionary GetParams(RouteValueDictionary valuesDictionary)
        {
            if (valuesDictionary.ContainsKey(ProductConstants.CurrentRequestParams))
            {
                var values = valuesDictionary[ProductConstants.CurrentRequestParams];

                var routeValueDictionary = values as NameValueCollection;
                if (routeValueDictionary != null)
                    foreach (string key in routeValueDictionary.Keys)
                    {
                        if (key.StartsWith(ProductConstants.CurrentPageQueryRequestParam))
                            valuesDictionary.Add(key, routeValueDictionary[key]);
                        if (key.StartsWith(ProductConstants.ProductIdQueryRequestParam))
                            valuesDictionary.Add(key, routeValueDictionary[key]);
                        if (key.ToLower().Equals("url"))
                        {
                            if (valuesDictionary.ContainsKey(ProductConstants.IsAjaxPageQueryRequestParam) && (bool)valuesDictionary[ProductConstants.IsAjaxPageQueryRequestParam] && !String.IsNullOrEmpty(routeValueDictionary["HTTP_REFERER"]))
                                valuesDictionary.Add(key, routeValueDictionary["HTTP_REFERER"].Split('?')[0]);
                            else
                                valuesDictionary.Add(key, routeValueDictionary[key]);
                        }

                    }
                valuesDictionary.Remove(ProductConstants.CurrentRequestParams);
            }
            return valuesDictionary;
        }
    }
}