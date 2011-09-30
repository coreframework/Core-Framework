using System;
using Framework.Core.Services;
using NHibernate;
using Products.NHibernate.Models;

namespace Products.NHibernate.Contracts
{
    public interface ICategoryLocaleService : IDataService<CategoryLocale>
    {
        /// <summary>
        /// Get Category Locale
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <param name="culture">The culture</param>
        /// <returns>Category Locale</returns>
        CategoryLocale GetLocale(long categoryId, String culture);

        /// <summary>
        /// Gets the search criteria.
        /// </summary>
        /// <param name="searchString">The search string.</param>
        /// <returns></returns>
        ICriteria GetSearchCriteria(String searchString);
    }
}
