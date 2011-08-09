using Core.News.Nhibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.News.Nhibernate.Mappings
{
    public class ArticleToCategoryMapping : ClassMap<ArticleToCategory>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductToCategoryMapping"/> class.
        /// </summary>
        public ArticleToCategoryMapping()
         {
             Cache.Region("News_ArticlesToCategories").ReadWrite();
             Table("News_ArticlesToCategories");
             Id(prodCat => prodCat.Id);
             Map(prodCat => prodCat.ArticleId);
             Map(prodCat => prodCat.CategoryId);
        }
    }
}
