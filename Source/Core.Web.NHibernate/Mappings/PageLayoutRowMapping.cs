using Core.Web.NHibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.Web.NHibernate.Mappings
{
    public class PageLayoutRowMapping : ClassMap<PageLayoutRow>
    {
        public PageLayoutRowMapping()
        {
            Cache.Region("PageLayoutRows").ReadWrite();
            Table("PageLayoutRows");
            Id(pageRow => pageRow.Id);
            HasMany(pageRow => pageRow.Columns).KeyColumn("RowId")
                .Table("PageLayoutColumns").AsSet()
                .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.None)
                .Inverse()
                .LazyLoad()
                .Cascade.AllDeleteOrphan();
        }

    }
}
