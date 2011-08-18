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
            Cache.Region("News_ArticleWidgets").ReadWrite();
            Table("News_ArticleWidgets");
            Id(newsArticleWidget => newsArticleWidget.Id);
            Map(newsArticleWidget => newsArticleWidget.ItemsOnPage);
            Map(newsArticleWidget => newsArticleWidget.ShowPaginator);
            Map(newsArticleWidget => newsArticleWidget.Url);
            /*
            HasManyToMany(newsArticleWidget => newsArticleWidget.Categories)
                                       .Table("News_ArticleWidgetToCategories").ParentKeyColumn("ArticleWidgetId")
                                       .ChildKeyColumn("CategoryId").Cascade.SaveUpdate().LazyLoad();*/

            HasMany(newsArticleWidget => newsArticleWidget.Categories).KeyColumn("NewsArticleWidgetId")
              .Table("News_ArticleWidgetToCategories")
              .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.Underscore)
              .Inverse()
              .LazyLoad()
              .Cascade.AllDeleteOrphan();
        }
    }
}
