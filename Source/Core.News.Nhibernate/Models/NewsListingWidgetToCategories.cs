using FluentNHibernate.Data;

namespace Core.News.Nhibernate.Models
{
    public class NewsListingWidgetToCategories : Entity
    {
        #region Properties

        /// <summary>
        /// Gets or sets the news listing widget.
        /// </summary>
        /// <value>The news listing widget.</value>
        public virtual NewsListingWidget NewsListingWidget { get; set; }

        /// <summary>
        /// Gets or sets the category id
        /// </summary>
        public virtual NewsCategory Category { get; set; }

        #endregion
    }
}
