using Core.Web.NHibernate.Models;
using FluentNHibernate.Mapping;
using Framework.Facilities.NHibernate.Filters;

namespace Core.Web.NHibernate.Mappings
{
    public class WidgetLocaleMapping : ClassMap<WidgetLocale>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PluginLocaleMapping"/> class.
        /// </summary>
        public WidgetLocaleMapping()
        {
            Cache.Region("WidgetLocales").ReadWrite();
            Table("WidgetLocales");
            Id(widgetLocale => widgetLocale.Id);
            References(widgetLocale => widgetLocale.Widget).Column("WidgetId").LazyLoad().Not.Nullable();
            Map(widgetLocale => widgetLocale.Culture).Length(5);
            Map(widgetLocale => widgetLocale.Title).Length(255);
            Map(widgetLocale => widgetLocale.Priority).Formula(CultureFilter.CultureFilterPriorityExpression());
        }
    }
}