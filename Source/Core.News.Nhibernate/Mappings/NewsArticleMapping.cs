using Core.News.Nhibernate.Models;
using FluentNHibernate.Mapping;
using Framework.Facilities.NHibernate.Filters;

namespace Core.News.Nhibernate.Mappings
{
    public class NewsArticleMapping : ClassMap<NewsArticle>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewsArticleMapping"/> class.
        /// </summary>
        public NewsArticleMapping()
        {
            Cache.Region("News").ReadWrite();
            Table("News");
            Id(newsArticle => newsArticle.Id);
//            HasMany(newsArticle => newsArticle.Widgets).KeyColumn("NewsArticleId")
//            .Table("NewsArticleWidgets")
//            .Inverse()
//            .LazyLoad()
//            .Cascade.All();
            HasMany(newsArticle => newsArticle.CurrentNewsArticleLocales).KeyColumn("NewsArticleId")
            .Table("NewsArticleLocales").ApplyFilter<CultureFilter>()
            .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.Underscore)
            .Inverse()
            .LazyLoad()
            .Cascade.All();
        }
    }
}
