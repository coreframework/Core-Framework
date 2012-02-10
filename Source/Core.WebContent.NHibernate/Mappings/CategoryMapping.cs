using Core.WebContent.NHibernate.Models;
using Core.WebContent.NHibernate.Static;
using FluentNHibernate.Mapping;
using Framework.Facilities.NHibernate.Filters;

namespace Core.WebContent.NHibernate.Mappings
{
    public class CategoryMapping : ClassMap<WebContentCategory>
    {
        public CategoryMapping()
        {
            Cache.Region("WebContent_Categories").ReadWrite();
            Table("WebContent_Categories");
            Id(category => category.Id);
            Map(category => category.UserId);
            References(category => category.Section);
            Map(category => category.Status).CustomType(typeof(CategoryStatus));
            HasMany(category => category.CurrentLocales).KeyColumn("CategoryId")
            .Table("CategoryLocales").AsSet().ApplyFilter<CultureFilter>()
            .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.None)
            .Inverse()
            .LazyLoad()
            .Cascade.All();
        }
    }
}
