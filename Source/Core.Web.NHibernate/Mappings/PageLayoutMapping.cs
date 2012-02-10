using Core.Web.NHibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.Web.NHibernate.Mappings
{
    public class PageLayoutMapping : ClassMap<PageLayout>
    {
        public PageLayoutMapping()
        {
            Cache.Region("PageLayouts").ReadWrite();
            Table("PageLayouts");
            Id(pageLayouts => pageLayouts.Id);
            References(pageLayouts => pageLayouts.LayoutTemplate).Column("TemplateId");
            References(pageSettings => pageSettings.Page).Column("PageId");
            HasMany(pageSettings => pageSettings.ColumnWidths).KeyColumn("PageLayoutId")
                .Table("PageLayoutColumnWidthValues").AsSet()
                .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.None)
                .Inverse()
                .LazyLoad()
                .Cascade.AllDeleteOrphan();
        }
    }
}
