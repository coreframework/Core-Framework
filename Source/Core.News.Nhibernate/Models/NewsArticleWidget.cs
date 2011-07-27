using System.Collections.Generic;
using FluentNHibernate.Data;

namespace Core.News.Nhibernate.Models
{
    public class NewsArticleWidget : Entity
    {
        public virtual List<NewsArticle> NewsArticles { get; set; }

        public virtual int ItemsOnPage { get; set; }
    }
}
