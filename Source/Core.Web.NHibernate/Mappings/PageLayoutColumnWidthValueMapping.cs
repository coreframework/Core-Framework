using Core.Web.NHibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.Web.NHibernate.Mappings
{
    public class PageLayoutColumnWidthValueMapping : ClassMap<PageLayoutColumnWidthValue>
    {
        public PageLayoutColumnWidthValueMapping()
        {
            Cache.Region("PageLayoutColumnWidthValues").ReadWrite();
            Table("PageLayoutColumnWidthValues");
            Id(pageColumnWidthValue => pageColumnWidthValue.Id);
            References(pageColumnWidthValue => pageColumnWidthValue.Column).Column("ColumnId");
            References(pageColumnWidthValue => pageColumnWidthValue.PageLayout).Column("PageLayoutId");
            Map(pageColumnWidthValue => pageColumnWidthValue.WidthValue);
            Map(pageColumnWidthValue => pageColumnWidthValue.Colspan);
        }
    }
}
