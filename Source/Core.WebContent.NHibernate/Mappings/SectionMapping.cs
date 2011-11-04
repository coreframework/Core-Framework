using Core.WebContent.NHibernate.Models;
using FluentNHibernate.Mapping;
using Framework.Facilities.NHibernate.Filters;

namespace Core.WebContent.NHibernate.Mappings
{
    public class SectionMapping : ClassMap<Section>
    {
        public SectionMapping()
        {
            Cache.Region("WebContent_Sections").ReadWrite();
            Table("WebContent_Sections");
            Id(section => section.Id);
            Map(section => section.UserId);
            HasMany(section => section.CurrentLocales).KeyColumn("SectionId")
            .Table("SectionLocales").ApplyFilter<CultureFilter>()
            .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.None)
            .Inverse()
            .LazyLoad()
            .Cascade.All();
        }
    }
}
