using Core.WebContent.NHibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.WebContent.NHibernate.Mappings
{
    public class WebContentWidgetCategoryMapping : ClassMap<WebContentWidgetCategory>
    {
        public WebContentWidgetCategoryMapping()
        {
            Cache.Region("WebContent_WebContentWidgetCategories").ReadWrite();
            Table("WebContent_WebContentWidgetCategories");
            Id(widget => widget.Id);
            References(widget => widget.Category);
            References(widget => widget.WebContentWidget).Column("WebContentWidgetId");
        }
    }
}
