using FluentNHibernate.Data;

namespace Products.NHibernate.Models
{
    public class ProductToCategory: Entity
    {
        /// <summary>
        /// Gets or sets the product id
        /// </summary>
        public virtual long ProductId { get; set; }

        /// <summary>
        /// Gets or sets the category id
        /// </summary>
        public virtual long CategoryId { get; set; }
    }
}
