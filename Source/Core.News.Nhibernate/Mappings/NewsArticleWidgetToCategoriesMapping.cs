using Core.News.Nhibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.News.Nhibernate.Mappings
{
    public class NewsArticleWidgetToCategoriesMapping : ClassMap<NewsArticleWidgetToCategories>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewsArticleWidgetToCategoriesMapping"/> class.
        /// </summary>
        public NewsArticleWidgetToCategoriesMapping()
         {
             Cache.Region("News_ArticleWidgetToCategories").ReadWrite();
             Table("News_ArticleWidgetToCategories");
             Id(newsWidCat => newsWidCat.Id);
             References(newsWidCat => newsWidCat.Category);
             References(newsWidCat => newsWidCat.NewsArticleWidget);
        }
    }
}
