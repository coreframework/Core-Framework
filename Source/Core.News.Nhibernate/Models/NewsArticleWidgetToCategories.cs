using FluentNHibernate.Data;

namespace Core.News.Nhibernate.Models
{
    public class NewsArticleWidgetToCategories : Entity
    {
        /// <summary>
        /// Gets or sets the product widget id
        /// </summary>
        public virtual long ArticleWidgetId { get; set; }

        /// <summary>
        /// Gets or sets the category id
        /// </summary>
        public virtual long CategoryId { get; set; }
    }
}
