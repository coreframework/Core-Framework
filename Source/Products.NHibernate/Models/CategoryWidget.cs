using FluentNHibernate.Data;

namespace Products.NHibernate.Models
{
    public class CategoryWidget : Entity
    {
        /// <summary>
        /// Gets or sets the number of items per page
        /// </summary>
        public virtual int PageSize { get; set; }
    }
}
