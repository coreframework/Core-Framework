using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Products.Helpers
{
    public class Pager
    {
        private HtmlHelper helper;
        private readonly int pageSize;
        private readonly int currentPage;
        private readonly int totalItemCount;
        private readonly RouteValueDictionary linkWithoutPageValuesDictionary;
        private readonly long id;
        private readonly String curPagePreffix;

        public Pager(HtmlHelper helper, long id, int pageSize, int currentPage, int totalItemCount, String preffix, RouteValueDictionary valueDictionary)
        {
            this.helper = helper;
            this.pageSize = pageSize;
            this.currentPage = currentPage;
            this.totalItemCount = totalItemCount;
            this.linkWithoutPageValuesDictionary = valueDictionary;
            this.id = id;
            curPagePreffix = preffix;
        }

        public String RenderHtml()
        {
            int pageCount = (int)Math.Ceiling(this.totalItemCount / (double)this.pageSize);
            if (pageCount < 2)
                return String.Empty;
            const int numberOfPagesToDisplay = 10;

            var sb = new StringBuilder();
            sb.Append("<div class=\"pager\">");

            // Previous
            if (this.currentPage > 1)
            {
                sb.Append(GeneratePageLink("<", this.currentPage - 1));
            }
            else
            {
                sb.Append("<span class=\"disabled\"><</span>");
            }

            int start = 1;
            int end = pageCount;

            if (pageCount > numberOfPagesToDisplay)
            {
                int middle = (int)Math.Ceiling(numberOfPagesToDisplay / 2d) - 1;
                int below = this.currentPage - middle;
                int above = this.currentPage + middle;

                if (below < 4)
                {
                    above = numberOfPagesToDisplay;
                    below = 1;
                }
                else if (above > (pageCount - 4))
                {
                    above = pageCount;
                    below = pageCount - numberOfPagesToDisplay;
                }

                start = below;
                end = above;
            }

            if (start > 3)
            {
                sb.Append(GeneratePageLink("1", 1));
                sb.Append(GeneratePageLink("2", 2));
                sb.Append("...");
            }
            for (int i = start; i <= end; i++)
            {
                if (i == this.currentPage)
                {
                    sb.AppendFormat("<span class=\"current\">{0}</span>", i);
                }
                else
                {
                    var str = GeneratePageLink(i.ToString(), i);
                    sb.Append(str);
                }
            }
            if (end < (pageCount - 3))
            {
                sb.Append("...");
                sb.Append(GeneratePageLink((pageCount - 1).ToString(), pageCount - 1));
                sb.Append(GeneratePageLink(pageCount.ToString(), pageCount));
            }

            // Next
            if (this.currentPage < pageCount)
            {
                sb.Append(GeneratePageLink(">", (this.currentPage + 1)));
            }
            else
            {
                sb.Append("<span class=\"disabled\">&gt;</span>");
            }

            sb.Append("</div>");

            return sb.ToString();
        }

        private String GeneratePageLink(String linkText, int pageNumber)
        {
            var pageLinkValueDictionary = new RouteValueDictionary(this.linkWithoutPageValuesDictionary);
            if (pageLinkValueDictionary.ContainsKey(ProductConstants.CurrentPageQueryRequestParam + curPagePreffix + id))
                pageLinkValueDictionary.Remove(ProductConstants.CurrentPageQueryRequestParam + curPagePreffix + id);
            pageLinkValueDictionary.Add(ProductConstants.CurrentPageQueryRequestParam + curPagePreffix + id, pageNumber);

            var routeParams = String.Empty;
            foreach (var key in pageLinkValueDictionary.Keys.Where(key => key.StartsWith(ProductConstants.CurrentPageQueryRequestParam) || key.StartsWith(ProductConstants.ProductIdQueryRequestParam)))
            {
                if (String.IsNullOrEmpty(routeParams))
                {
                    routeParams += "?";
                } 
                else
                {
                    routeParams += "&";
                }
                    
                routeParams += key + "=" + pageLinkValueDictionary[key];
            }
            if (pageLinkValueDictionary.ContainsKey("url") && !pageLinkValueDictionary.ContainsKey("action"))
                return @"<a href=""" + pageLinkValueDictionary["url"] + routeParams+ "\">" + linkText + "</a>";

            return helper.ActionLink(linkText, pageLinkValueDictionary["action"].ToString(), pageLinkValueDictionary, null).ToString();
        }
    }
}