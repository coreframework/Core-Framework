using System;
using System.Collections.Generic;
using Core.WebContent.NHibernate.Static;
using FluentNHibernate.Data;

namespace Core.WebContent.NHibernate.Models
{
    public class WebContentWidget : Entity
    {
        #region Fields

        private readonly IList<WebContentWidgetCategory> categories;

        #endregion

        #region Constructor

        public WebContentWidget()
        {
            categories = new List<WebContentWidgetCategory>();
        }

        #endregion

        #region Properties
        /// <summary>
        ///  Gets or sets the categories
        /// </summary>
        public virtual IEnumerable<WebContentWidgetCategory> Categories
        {
            get { return categories; }
        }

        /// <summary>
        /// Gets or sets the items number.
        /// </summary>
        /// <value>The items number.</value>
        public virtual int ItemsNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show pagination].
        /// </summary>
        /// <value><c>true</c> if [show pagination]; otherwise, <c>false</c>.</value>
        public virtual bool ShowPagination { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        public virtual String Url { get; set; }

        /// <summary>
        /// Gets or sets the view mode.
        /// </summary>
        /// <value>The view mode.</value>
        public virtual WebContentWidgetViewMode ViewMode { get; set; }

        /// <summary>
        /// Gets or sets the article.
        /// </summary>
        /// <value>The article.</value>
        public virtual Article Article { get; set; }

        /// <summary>
        /// Gets or sets the section.
        /// </summary>
        /// <value>The section.</value>
        public virtual Section Section { get; set; }

        #endregion

        #region Helper Methods

        public virtual void AddCategory(WebContentWidgetCategory category)
        {
            categories.Add(category);
        }

        #endregion
    }
}
