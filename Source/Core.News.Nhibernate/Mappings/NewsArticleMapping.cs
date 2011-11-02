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
            Cache.Region("News_News").ReadWrite();
            Table("News_News");
            Id(newsArticle => newsArticle.Id);
            Map(newsArticle => newsArticle.CreateDate);
            Map(newsArticle => newsArticle.PublishDate);
            Map(newsArticle => newsArticle.LastModifiedDate);
            Map(newsArticle => newsArticle.StatusId);
            Map(newsArticle => newsArticle.Url).Length(255);
            HasManyToMany(newsArticle => newsArticle.Categories)
                           .Table("News_ArticlesToCategories").ParentKeyColumn("ArticleId")
                           .ChildKeyColumn("CategoryId").Cascade.SaveUpdate().LazyLoad();


            HasMany(newsArticle => newsArticle.CurrentNewsArticleLocales).KeyColumn("NewsArticleId")
            .Table("News_ArticleLocales").ApplyFilter<CultureFilter>()
            .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.None)
            .Inverse()
            .LazyLoad()
            .Cascade.All();
        }
    }
}
