using Core.WebContent.NHibernate.Models;
using Core.WebContent.NHibernate.Static;
using FluentNHibernate.Mapping;

namespace Core.WebContent.NHibernate.Mappings
{
    public class WebContentWidgetMapping : ClassMap<WebContentWidget>
    {
        public WebContentWidgetMapping()
        {
            Cache.Region("WebContent_WebContentWidgets").ReadWrite();
            Table("WebContent_WebContentWidgets");
            Id(widget => widget.Id);
            Map(widget => widget.ItemsNumber);
            Map(widget => widget.ShowPagination);
            Map(widget => widget.ViewMode).CustomType<WebContentWidgetViewMode>();
            References(widget => widget.Article);
            References(widget => widget.Section);
            HasMany(widget => widget.Categories).KeyColumn("WebContentWidgetId")
            .Table("WebContent_WebContentWidgetCategories").AsSet()
            .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.None)
            .Inverse()
            .LazyLoad()
            .Cascade.AllDeleteOrphan();
        }
    }
}
