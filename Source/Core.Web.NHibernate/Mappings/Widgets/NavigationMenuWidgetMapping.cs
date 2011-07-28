
using Core.Web.NHibernate.Models.Static;
using Core.Web.NHibernate.Models.Widgets;
using FluentNHibernate.Mapping;

namespace Core.Web.NHibernate.Mappings.Widgets
{
   
    public class NavigationMenuWidgetMapping : ClassMap<NavigationMenuWidget>
    {
        public NavigationMenuWidgetMapping()
        {
            Cache.Region("NavigationMenuWidgets").ReadWrite();
            Table("NavigationMenuWidgets");
            Id(navigationMenuWidget => navigationMenuWidget.Id);
            Map(navigationMenuWidget => navigationMenuWidget.Orientation).CustomType(typeof(Orientation));
        }
    }
}
