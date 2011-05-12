using Core.ContentPages.NHibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.ContentPages.NHibernate.Mappings
{
    public class ContentPageWidgetMapping : ClassMap<ContentPageWidget>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentPageWidgetMapping"/> class.
        /// </summary>
        public ContentPageWidgetMapping()
        {
            Cache.Region("ContentPageWidgets").ReadWrite();
            Table("ContentPageWidgets");
            Id(contentPageWidget => contentPageWidget.Id);
            References(contentPageWidget => contentPageWidget.ContentPage).Column("ContentPageId").LazyLoad().Not.Nullable();
        }
    }
}