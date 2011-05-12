using Core.Web.NHibernate.Models.Widgets;
using FluentNHibernate.Mapping;

namespace Core.Web.NHibernate.Mappings.Widgets
{
    public class SiteMapWidgetMapping : ClassMap<SiteMapWidget>
    {
        public SiteMapWidgetMapping()
        {
            Cache.Region("SiteMapWidgets").ReadWrite();
            Table("SiteMapWidgets");
            Id(siteMapWidget => siteMapWidget.Id);
            Map(siteMapWidget => siteMapWidget.Depth);
            Map(siteMapWidget => siteMapWidget.IncludeRootInTree);
            References(siteMapWidget => siteMapWidget.RootPage).Column("RootPageId").Nullable();
        }
    }
}
