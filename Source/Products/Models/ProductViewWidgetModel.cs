using System.Collections.Generic;
using Products.NHibernate.Models;

namespace Products.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductViewWidgetModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        /// <value>The page size.</value>
        public int PageSize { get; set; }
        /// <summary>
        /// Gets or sets the current page.
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// Gets or sets total items count.
        /// </summary>
        public int TotalItemsCount { get; set; }
        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>The categories.</value>
        public long[] CategoriesId { get; set; }
        /// <summary>
        /// Gets or sets the categories
        /// </summary>
        public IEnumerable<Product> Products;

        #endregion

    }
}