using Core.News.Nhibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.News.Nhibernate.Mappings
{
    class NewsArticleWidgetToCategoriesMapping : ClassMap<NewsArticleWidgetToCategories>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewsArticleWidgetToCategoriesMapping"/> class.
        /// </summary>
        public NewsArticleWidgetToCategoriesMapping()
         {
             Cache.Region("News_ArticleWidgetToCategories").ReadWrite();
             Table("News_ArticleWidgetToCategories");
             Id(prodWidCat => prodWidCat.Id);
             Map(prodWidCat => prodWidCat.ArticleWidgetId);
             Map(prodWidCat => prodWidCat.CategoryId);
        }
    }
}
