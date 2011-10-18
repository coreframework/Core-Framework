using FluentNHibernate.Data;

namespace Core.News.Nhibernate.Models
{
    public class NewsCategoryWidget : Entity
    {
        /// <summary>
        /// Gets or sets the number of items per page
        /// </summary>
        public virtual int PageSize { get; set; }
    }
}
