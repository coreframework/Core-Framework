using System;
using Framework.Core.Services;
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
    }
}
