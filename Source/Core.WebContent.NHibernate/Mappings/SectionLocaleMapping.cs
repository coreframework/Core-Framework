using Core.WebContent.NHibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.WebContent.NHibernate.Mappings
{
    public class SectionLocaleMapping: ClassMap<SectionLocale>
    {
        public SectionLocaleMapping()
        {
            Cache.Region("WebContent_SectionLocales").ReadWrite();
            Table("WebContent_SectionLocales");
            Id(section => section.Id);
        }
    }
}
