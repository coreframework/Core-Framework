using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Core.Services;
using Core.News.Nhibernate.Models;

namespace Core.News.NHibernate.Contracts
{
    public interface INewsCategoryService : IDataService<NewsCategory>
    {
        /// <summary>
        /// Gets the search.
        /// </summary>
        /// <param name="search">The search string.</param>
        /// <returns>Categories</returns>
        IQueryable<NewsCategory> GetSearchQuery(String search);

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <param name="searchQuery">The base query.</param>
        /// <returns>Count</returns>
        int GetCount(IQueryable<NewsCategory> searchQuery);

        /// <summary>
        /// Gets the category search list.
        /// </summary>
        /// <param name="search">The search string.</param>
        /// <returns>Categories</returns>
        IEnumerable<NewsCategory> GetCategories(String search);

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <param name="searchQuery">The search string.</param>
        /// <returns>Count</returns>
        int GetCount(String searchQuery);
    }
}
