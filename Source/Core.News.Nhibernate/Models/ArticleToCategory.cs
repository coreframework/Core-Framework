using FluentNHibernate.Data;

namespace Core.News.Nhibernate.Models
{
    public class ArticleToCategory : Entity
    {
        /// <summary>
        /// Gets or sets the product id
        /// </summary>
        public virtual long ArticleId { get; set; }

        /// <summary>
        /// Gets or sets the category id
        /// </summary>
        public virtual long CategoryId { get; set; }
    }
}
