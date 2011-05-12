using Core.Web.NHibernate.Models.Widgets;
using FluentNHibernate.Mapping;

namespace Core.Web.NHibernate.Mappings.Widgets
{
    public class BreadcrumbsWidgetMapping:ClassMap<BreadcrumbsWidget>
    {
        public BreadcrumbsWidgetMapping()
        {
            Cache.Region("BreadcrumbsWidgets").ReadWrite();
            Table("BreadcrumbsWidgets");
            Id(breadcrumbsWidget => breadcrumbsWidget.Id);
            Map(breadcrumbsWidget => breadcrumbsWidget.ShowHomePage);
        }
    }
}
