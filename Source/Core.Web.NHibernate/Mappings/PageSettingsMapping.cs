using Core.Web.NHibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.Web.NHibernate.Mappings
{
    public class PageSettingsMapping : ClassMap<PageSettings>
    {
        public PageSettingsMapping()
        {
            Cache.Region("PageSettings").ReadWrite();
            Table("PageSettings");
            Id(pageSettings => pageSettings.Id);
            Map(pageSettings => pageSettings.CustomCSS);
            References(pageSettings => pageSettings.LookAndFeelSettings, "LookAndFeelSettingsId").Cascade.All();
            References(pageSettings => pageSettings.Page).Column("PageId");
        }
    }
}
