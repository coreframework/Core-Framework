using Core.Web.NHibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.Web.NHibernate.Mappings
{
    public class PageLayoutTemplateMapping : ClassMap<PageLayoutTemplate>
    {
        public PageLayoutTemplateMapping()
        {
            Cache.Region("PageLayoutTemplates").ReadWrite();
            Table("PageLayoutTemplates");
            Id(pageLayoutTemplate => pageLayoutTemplate.Id);
            Map(pageLayoutTemplate => pageLayoutTemplate.LayoutCssClass).Length(50);
            Map(pageLayoutTemplate => pageLayoutTemplate.Priority);
            Map(pageLayoutTemplate => pageLayoutTemplate.ColumnsNumber).Formula(
                @"(SELECT count(*)
                FROM PageLayoutColumns
                WHERE PageLayoutColumns.RowId in (SELECT PageLayoutRows.Id FROM PageLayoutRows WHERE PageLayoutRows.TemplateId = Id))").LazyLoad();
            HasMany(pageLayoutTemplate => pageLayoutTemplate.Rows).KeyColumn("TemplateId")
                .Table("PageLayoutRows")
                .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.None)
                .Inverse()
                .LazyLoad()
                .Cascade.AllDeleteOrphan();
        }

    }
}
