using System;
using System.Collections.Generic;
using FluentNHibernate.Data;

namespace Core.News.Nhibernate.Models
{
    public class NewsListingWidget : Entity
    {
        #region Constructor

        #endregion

        #region Properties
        /// <summary>
        ///  Gets or sets the categories
        /// </summary>
        public virtual IEnumerable<NewsCategory> Categories { get; set; }

        public virtual int ItemsOnPage { get; set; }

        public virtual bool ShowPaginator { get; set; }

        public virtual String Url { get; set; }

        #endregion

    }
}
