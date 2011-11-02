using System;
using System.Collections.Generic;
using System.Linq;
using Core.News.Nhibernate.Models;
using Framework.Core.Services;

namespace Core.News.Nhibernate.Contracts
{
    public interface INewsArticleService : IDataService<NewsArticle>
    {
        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <param name="baseQuery">The base query.</param>
        /// <returns></returns>
        int GetCount(IQueryable<NewsArticle> baseQuery);

        /// <summary>
        /// Gets the search query.
        /// </summary>
        /// <param name="searchString">The search string.</param>
        /// <returns></returns>
        IQueryable<NewsArticle> GetSearchQuery(String searchString);

        NewsArticle FindPublished(long id);

        NewsArticle FindPublished(String url);

        IEnumerable<NewsArticle> FindPublished();

        IEnumerable<NewsArticle> GetForListingWidget(NewsListingWidget listingWidget, int currentPage);
        
        int GetCountForListingWidget(NewsListingWidget listingWidget);

    }
}
