using System.Collections.Generic;
using FluentNHibernate.Data;

namespace Products.NHibernate.Models
{
    public class ProductWidget : Entity
    {
        /// <summary>
        /// Gets or sets the number of items per page
        /// </summary>
        public virtual int PageSize { get; set; }
        /// <summary>
        ///  Gets or sets the categories
        /// </summary>
        public virtual IList<Category> Categories { get; set; }
    }
}