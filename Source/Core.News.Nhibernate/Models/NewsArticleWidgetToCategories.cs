using FluentNHibernate.Data;

namespace Core.News.Nhibernate.Models
{
    public class NewsArticleWidgetToCategories : Entity
    {
        #region Properties
        /// <summary>
        /// Gets or sets the product widget id
        /// </summary>
        public virtual NewsArticleWidget NewsArticleWidget { get; set; }

        /// <summary>
        /// Gets or sets the category id
        /// </summary>
        public virtual NewsCategory Category { get; set; }

        #endregion
    }
}
