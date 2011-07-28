using FluentNHibernate.Data;

namespace Products.NHibernate.Models
{
    public class ProductWidgetToCategory:Entity
    {
        /// <summary>
        /// Gets or sets the product widget id
        /// </summary>
        public virtual long ProductWidgetId { get; set; }

        /// <summary>
        /// Gets or sets the category id
        /// </summary>
        public virtual long CategoryId { get; set; }
    }
}
