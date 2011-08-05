using FluentNHibernate.Data;

namespace Products.NHibernate.Models
{
    public class ProductWidgetToCategory:Entity
    {
        #region Properties
        /// <summary>
        /// Gets or sets the product widget id
        /// </summary>
        public virtual ProductWidget ProductWidget { get; set; }

        /// <summary>
        /// Gets or sets the category id
        /// </summary>
        public virtual Category Category { get; set; }

        #endregion
    }
}
