using System.Collections.Generic;
using FluentNHibernate.Data;

namespace Core.News.Nhibernate.Models
{
    public class NewsArticleWidget : Entity
    {
        /// <summary>
        ///  Gets or sets the categories
        /// </summary>
        public virtual IList<NewsCategory> Categories { get; set; }

        public virtual int ItemsOnPage { get; set; }

        public virtual bool ShowPaginator { get; set; }
    }
}
