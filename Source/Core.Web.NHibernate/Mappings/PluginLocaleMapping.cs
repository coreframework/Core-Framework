using Core.Web.NHibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.Web.NHibernate.Mappings
{
    public class PluginLocaleMapping : ClassMap<PluginLocale>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PluginLocaleMapping"/> class.
        /// </summary>
        public PluginLocaleMapping()
        {
            Cache.Region("PluginLocales").ReadWrite();
            Table("PluginLocales");
            Id(pluginLocale => pluginLocale.Id);
            References(pluginLocale => pluginLocale.Plugin).Column("PluginId").LazyLoad().Not.Nullable();
            Map(pluginLocale => pluginLocale.Culture).Length(5);
            Map(plugin => plugin.Title);
            Map(plugin => plugin.Description);
        }
    }
}