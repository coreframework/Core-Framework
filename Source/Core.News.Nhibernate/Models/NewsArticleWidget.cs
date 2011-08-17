using System.Collections.Generic;
using FluentNHibernate.Data;

namespace Core.News.Nhibernate.Models
{
    public class NewsArticleWidget : Entity
    {
        #region Fields

        private readonly IList<NewsArticleWidgetToCategories> _categories;

        #endregion

        #region Constructor

        public NewsArticleWidget()
        {
            _categories = new List<NewsArticleWidgetToCategories>();
        }

        #endregion

        #region Properties
        /// <summary>
        ///  Gets or sets the categories
        /// </summary>
        public virtual IEnumerable<NewsArticleWidgetToCategories> Categories
        {
            get { return _categories; }
        }

        public virtual int ItemsOnPage { get; set; }

        public virtual bool ShowPaginator { get; set; }

        #endregion

        #region Helper Methods

        public virtual void AddCategory(NewsArticleWidgetToCategories category)
        {
            _categories.Add(category);
        }

        #endregion
    }
}
