using Core.Web.NHibernate.Models;
using Core.Web.NHibernate.Models.Static;
using FluentNHibernate.Mapping;

namespace Core.Web.NHibernate.Mappings
{
    public class PageWidgetMapping : ClassMap<PageWidget>
    {
        public PageWidgetMapping()
        {
            Cache.Region("PageWidgets").ReadWrite();
            Table("PageWidgets");
            Id(pageWidget => pageWidget.Id);
            Map(pageWidget => pageWidget.InstanceId);
            Map(pageWidget => pageWidget.PageSection).CustomType(typeof(PageSection));
            Map(pageWidget => pageWidget.ColumnNumber);
            Map(pageWidget => pageWidget.OrderNumber);
            Map(pageWidget => pageWidget.ParentWidgetId);
            Map(pageWidget => pageWidget.TemplateWidgetId);
            References(pageWidget => pageWidget.Page);
            HasOne(pageWidget => pageWidget.Settings).PropertyRef(pageWidgetSettings => pageWidgetSettings.Widget).
                Cascade.All().LazyLoad();
            References(pageWidget => pageWidget.User).Column("UserId").Nullable();
            References(pageWidget => pageWidget.Widget).Column("WidgetId").Nullable();
            HasMany(pageWidget => pageWidget.HolderInstances).KeyColumn("TemplateWidgetId")
            .Table("PageWidgets")
            .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.None)
            .Inverse()
            .LazyLoad()
            .Cascade.AllDeleteOrphan();
        }
    }
}
