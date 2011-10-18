using System.Collections.Generic;
using FluentNHibernate.Data;

namespace Products.NHibernate.Models
{
    public class ProductWidget : Entity
    {
        #region Fields

        private readonly IList<ProductWidgetToCategory> categories;

        #endregion

        #region Constructor

        public ProductWidget()
        {
            categories = new List<ProductWidgetToCategory>();
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the number of items per page
        /// </summary>
        public virtual int PageSize { get; set; }
        /// <summary>
        ///  Gets or sets the categories
        /// </summary>
        public virtual IEnumerable<ProductWidgetToCategory> Categories
        {
            get { return categories;}
        }

        #endregion

        #region Helper Methods

        public virtual void AddCategory(ProductWidgetToCategory category)
        {
            categories.Add(category);
        }

        #endregion
    }
}