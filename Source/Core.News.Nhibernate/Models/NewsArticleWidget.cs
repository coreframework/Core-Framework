using System;
using System.Collections.Generic;
using FluentNHibernate.Data;

namespace Core.News.Nhibernate.Models
{
    public class NewsArticleWidget : Entity
    {
        #region Fields

        private readonly IList<NewsArticleWidgetToCategories> categories;

        #endregion

        #region Constructor

        public NewsArticleWidget()
        {
            categories = new List<NewsArticleWidgetToCategories>();
        }

        #endregion

        #region Properties
        /// <summary>
        ///  Gets or sets the categories
        /// </summary>
        public virtual IEnumerable<NewsArticleWidgetToCategories> Categories
        {
            get { return categories; }
        }

        public virtual int ItemsOnPage { get; set; }

        public virtual bool ShowPaginator { get; set; }

        public virtual String Url { get; set; }

        #endregion

        #region Helper Methods

        public virtual void AddCategory(NewsArticleWidgetToCategories category)
        {
            categories.Add(category);
        }

        #endregion
    }
}
