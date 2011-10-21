using Core.News.Nhibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.News.Nhibernate.Mappings
{
    public class NewsListingWidgetMapping : ClassMap<NewsListingWidget>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewsArticleWidgetMapping"/> class.
        /// </summary>
        public NewsListingWidgetMapping()
        {
            Cache.Region("News_ListingWidgets").ReadWrite();
            Table("News_ListingWidgets");
            Id(newsListingWidget => newsListingWidget.Id);
            Map(newsListingWidget => newsListingWidget.ItemsOnPage);
            Map(newsListingWidget => newsListingWidget.ShowPaginator);
            Map(newsListingWidget => newsListingWidget.Url);            

            HasManyToMany(newsListingWidget => newsListingWidget.Categories).Table("News_ListingWidgetToCategories").ParentKeyColumn("NewsListingWidgetId")
                .ChildKeyColumn("CategoryId").Cascade.SaveUpdate().LazyLoad();
        }
    }
}