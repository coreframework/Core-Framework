using Core.News.Nhibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.News.Nhibernate.Mappings
{
    public class NewsDetailsWidgetMapping : ClassMap<NewsDetailsWidget>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewsArticleWidgetMapping"/> class.
        /// </summary>
        public NewsDetailsWidgetMapping()
        {
            Cache.Region("News_DetailsWidgets").ReadWrite();
            Table("News_DetailsWidgets");
            Id(newsListingWidget => newsListingWidget.Id);
            Map(newsListingWidget => newsListingWidget.LinkMode).CustomType(typeof(NewsDetailsLinkMode));
        }
    }
}