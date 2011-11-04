using Core.WebContent.NHibernate.Models;
using FluentNHibernate.Mapping;
using Framework.Facilities.NHibernate.Filters;

namespace Core.WebContent.NHibernate.Mappings
{
    public class SectionLocaleMapping: ClassMap<SectionLocale>
    {
        public SectionLocaleMapping()
        {
            Cache.Region("WebContent_SectionLocales").ReadWrite();
            Table("WebContent_SectionLocales");
            Id(section => section.Id);
            Map(formlocale => formlocale.Title).Length(255);
            Map(formlocale => formlocale.Culture);
            Map(formlocale => formlocale.Description);
            References(formlocale => formlocale.Section).Column("SectionId").LazyLoad().Not.Nullable();
            Map(widgetLocale => widgetLocale.Priority).Formula(CultureFilter.CultureFilterPriorityExpression());
        }
    }
}
