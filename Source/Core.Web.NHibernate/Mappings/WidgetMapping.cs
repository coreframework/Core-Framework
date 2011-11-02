using Core.Web.NHibernate.Models;
using FluentNHibernate.Mapping;
using Framework.Facilities.NHibernate.Filters;

namespace Core.Web.NHibernate.Mappings
{
    public class WidgetMapping : ClassMap<Widget>
    {
        /// <summary>
        /// Initializes a new instance of the9 <see cref="WidgetMapping"/> class.
        /// </summary>
        public WidgetMapping()
        {
            Cache.Region("Widgets").ReadWrite();
            Table("Widgets");
            Id(widget => widget.Id);
            Map(widget => widget.Identifier).Length(255);
            Map(widget => widget.Status).CustomType(typeof(WidgetStatus));
            References(widget => widget.Plugin);
            Map(widget => widget.IsDetailsWidget);

            HasMany(widget => widget.CurrentWidgetLocales).KeyColumn("WidgetId")
            .Table("WidgetLocales").ApplyFilter<CultureFilter>()
            .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.None)
            .Inverse()
            .LazyLoad()
            .Cascade.All();
        }
    }
}
