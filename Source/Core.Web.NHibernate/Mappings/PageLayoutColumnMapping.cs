using Core.Web.NHibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.Web.NHibernate.Mappings
{
    public class PageLayoutColumnMapping : ClassMap<PageLayoutColumn>
    {
        public PageLayoutColumnMapping()
        {
            Cache.Region("PageLayoutColumns").ReadWrite();
            Table("PageLayoutColumns");
            Id(pageColumn => pageColumn.Id);
            Map(pageColumn => pageColumn.DefaultWidthValue);
            Map(pageColumn => pageColumn.DefaultColspan);
        }

    }
}