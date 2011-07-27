using System;
using Framework.Core.Services;
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
    }
}
