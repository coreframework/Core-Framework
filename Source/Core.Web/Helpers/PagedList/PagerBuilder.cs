using System;
using System.Text;

namespace Core.Web.Helpers.PagedList
{
//    public class PagerBuilder
//    {
//        private const string linkFormatClassnameUrlprefixPagenumberText = " <a class=\"{0}\" href=\"{1}/{2}\">{3}</a> ";
//        private const string nextPagingClass = "nextPaging";
//        private const string nextText = ">";
//        private const string numberPagingClass = "numberPaging";
//        private const int pageNumberIntervall = 7;
//        private const string previousPagingClass = "previousPaging";
//        private const string previousText = "<";
//        private const string selectedNumberPagingClass = "selectedNumberPaging";
//
        /// <summary>
        /// Builds the pager.
        /// </summary>
        /// <param name="pagedList">The paged list.</param>
        /// <param name="urlPrefix">The URL prefix.</param>
        /// <returns></returns>
//        public string BuildPager(IPagedList pagedList, string urlPrefix)
//        {
//            if (pagedList.TotalRecords == 0)
//                return string.Empty;
//
//            var pagingStringBuilder = new StringBuilder();
//
//            pagingStringBuilder.Append("<div id=\"pager\">");
//
//            BuildPreviousLink(pagedList, pagingStringBuilder, urlPrefix);
//
//            BuildPageNumbers(pagedList, pagingStringBuilder, urlPrefix);
//
//            BuildNextLink(pagedList, pagingStringBuilder, urlPrefix);
//
//            pagingStringBuilder.Append("</div>");
//
//            return pagingStringBuilder.ToString();
//        }
//
//        private static void BuildNextLink(IPagedList pagedList, StringBuilder pagingStringBuilder, string urlPrefix)
//        {
//            if (pagedList.HasNextPage)
//                pagingStringBuilder.Append(RenderPagingLink(nextPagingClass, urlPrefix, pagedList.CurrentPage + 1,
//                                                            nextText));
//        }
//
//        private static void BuildPreviousLink(IPagedList pagedList, StringBuilder pagingStringBuilder, string urlPrefix)
//        {
//            if (pagedList.HasPreviousPage)
//                pagingStringBuilder.Append(RenderPagingLink(previousPagingClass, urlPrefix, pagedList.CurrentPage - 1,
//                                                            previousText));
//        }
//
//        private static string RenderPagingLink(string styleClass, string urlPrefix, int pageNumber, string text)
//        {
//            return string.Format(linkFormatClassnameUrlprefixPagenumberText, styleClass, urlPrefix, pageNumber,
//                                 text);
//        }
//
//        private static void BuildPageNumbers(IPagedList pagedList, StringBuilder pagingStringBuilder, string urlPrefix)
//        {
//            int startPage = Math.Max(1, pagedList.CurrentPage - pageNumberIntervall);
//            int endPage = Math.Min(pagedList.TotalPages, pagedList.CurrentPage + pageNumberIntervall);
//            int pagesShown = endPage - startPage;
//
//            if (pagesShown < pageNumberIntervall * 2)
//            {
//                int pagesMissing = pageNumberIntervall * 2 - pagesShown;
//                if (startPage > 1)
//                    startPage = Math.Max(1, startPage - pagesMissing);
//                else
//                    endPage = Math.Min(pagedList.TotalPages, endPage + pagesMissing);
//            }
//
//            for (int i = startPage; i <= endPage; i++)
//            {
//                if (i >= 1 && i <= pagedList.TotalPages)
//                {
//                    string numberPagingStyleClass = numberPagingClass;
//                    if (i == pagedList.CurrentPage)
//                        numberPagingStyleClass = selectedNumberPagingClass;
//
//                    pagingStringBuilder.Append(RenderPagingLink(numberPagingStyleClass, urlPrefix, i, i.ToString()));
//                }
//            }
//        }
//
//    }
}