using Core.Web.NHibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.Web.NHibernate.Mappings
{
    public class WidgetMapping : ClassMap<Widget>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WidgetMapping"/> class.
        /// </summary>
        public WidgetMapping()
        {
            Cache.Region("Widgets").ReadWrite();
            Table("Widgets");
            Id(widget => widget.Id);
            Map(widget => widget.Identifier).Length(255);
            Map(widget => widget.Title).Length(255);
            Map(widget => widget.Status).CustomType(typeof(WidgetStatus));
            References(widget => widget.Plugin);
        }
    }
}
