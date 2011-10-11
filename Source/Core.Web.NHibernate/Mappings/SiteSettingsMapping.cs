using Core.Web.NHibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.Web.NHibernate.Mappings
{
    public class SiteSettingsMapping : ClassMap<SiteSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SiteSettingsMapping"/> class.
        /// </summary>
        public SiteSettingsMapping()
        {
            Cache.Region("SiteSettings").ReadWrite();
            Table("SiteSettings");
            Id(siteSettings => siteSettings.Id);
            Map(siteSettings => siteSettings.ShowPanel);
            Map(siteSettings => siteSettings.WebsiteName);
        }
    }
}