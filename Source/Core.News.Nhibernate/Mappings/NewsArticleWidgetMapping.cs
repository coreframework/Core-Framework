using Core.News.Nhibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.News.Nhibernate.Mappings
{
    public class NewsArticleWidgetMapping : ClassMap<NewsArticleWidget>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewsArticleWidgetMapping"/> class.
        /// </summary>
        public NewsArticleWidgetMapping()
        {
            Cache.Region("NewsArticleWidgets").ReadWrite();
            Table("NewsArticleWidgets");
            Id(newsArticleWidget => newsArticleWidget.Id);
            Map(newsArticleWidget => newsArticleWidget.ItemsOnPage);
//            HasMany(newsArticleWidget => newsArticleWidget.NewsArticles).KeyColumn("NewsArticleId")
//            .Table("NewsOnWidgets")
//            .Inverse()
//            .LazyLoad()
//            .Cascade.All();
        }
    }
}
