using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Core.Services;
using NHibernate;
using Products.NHibernate.Models;


namespace Products.NHibernate.Contracts
{
    public interface IProductService : IDataService<Product>
    {
        /// <summary>
        /// Gets the search query.
        /// </summary>
        /// <param name="search">The search string.</param>
        /// <returns>Products</returns>
        IQueryable<Product> GetSearchQuery(string search);

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <param name="searchQuery">The base query.</param>
        /// <returns>Count</returns>
        int GetCount(IQueryable<Product> searchQuery);

        /// <summary>
        /// Gets products assigned to the category
        /// </summary>
        /// <param name="category">The category</param>
        /// <returns>Products</returns>
        IQueryable<Product> GetProductsByCategory(Category category);

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <param name="searchCriteria">The base criteria.</param>
        /// <returns>Count</returns>
        int GetCount(ICriteria searchCriteria);

        /// <summary>
        /// Get Product Criteria by categories ids
        /// </summary>
        /// <param name="categoriesIds">Categories ids</param>
        /// <returns>ICriteria</returns>
        ICriteria GetProductCriteria(long[] categoriesIds);

        /// <summary>
        /// Get Product by product id and widget id
        /// </summary>
        /// <param name="id">The Product Id</param>
        /// <param name="widgetId">The Widget id</param>
        /// <returns>ICriteria</returns>
        Product GetProduct(long id, long widgetId);


    }
}
