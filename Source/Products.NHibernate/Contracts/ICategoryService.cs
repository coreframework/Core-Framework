using System.Collections.Generic;
using System.Linq;
using Framework.Core.Services;
using Products.NHibernate.Models;

namespace Products.NHibernate.Contracts
{
    public interface ICategoryService : IDataService<Category>
    {
        /// <summary>
        /// Gets the search.
        /// </summary>
        /// <param name="search">The search string.</param>
        /// <returns>Categories</returns>
        IQueryable<Category> GetSearchQuery(string search);

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <param name="searchQuery">The base query.</param>
        /// <returns>Count</returns>
        int GetCount(IQueryable<Category> searchQuery);

        /// <summary>
        /// Gets the category search list.
        /// </summary>
        /// <param name="search">The search string.</param>
        /// <returns>Categories</returns>
        IEnumerable<Category> GetCategories(string search);

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <param name="searchQuery">The search string.</param>
        /// <returns>Count</returns>
        int GetCount(string searchQuery);
    }
}
