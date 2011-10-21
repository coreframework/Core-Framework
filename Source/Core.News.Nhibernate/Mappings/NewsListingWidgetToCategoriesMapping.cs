using Core.News.Nhibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.News.Nhibernate.Mappings
{
    public class NewsListingWidgetToCategoriesMapping : ClassMap<NewsListingWidgetToCategories>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewsListingWidgetToCategoriesMapping"/> class.
        /// </summary>
        public NewsListingWidgetToCategoriesMapping()
         {
             Cache.Region("News_ListingWidgetToCategories").ReadWrite();
             Table("News_ListingWidgetToCategories");
             Id(newsWidCat => newsWidCat.Id);
             References(newsWidCat => newsWidCat.Category);
             References(newsWidCat => newsWidCat.NewsListingWidget);
        }
    }
}
