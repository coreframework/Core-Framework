using System;
using Framework.Core.Services;
using NHibernate;
using Products.NHibernate.Models;

namespace Products.NHibernate.Contracts
{
    public interface IProductLocaleService : IDataService<ProductLocale>
    {
        /// <summary>
        /// Get Product Locale
        /// </summary>
        /// <param name="productId">The product id.</param>
        /// <param name="culture">The culture</param>
        /// <returns>Product Locale</returns>
        ProductLocale GetLocale(long productId, String culture);

        /// <summary>
        /// Gets the search criteria.
        /// </summary>
        /// <param name="searchString">The search string.</param>
        /// <returns></returns>
        ICriteria GetSearchCriteria(String searchString);
    }
}
