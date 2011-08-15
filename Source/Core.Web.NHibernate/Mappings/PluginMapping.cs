using Core.Web.NHibernate.Models;
using FluentNHibernate.Mapping;
using Framework.Facilities.NHibernate.Filters;

namespace Core.Web.NHibernate.Mappings
{
    public class PluginMapping : ClassMap<Plugin>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PluginMapping"/> class.
        /// </summary>
        public PluginMapping()
        {
            Cache.Region("Plugins").ReadWrite();
            Table("Plugins");
            Id(plugin => plugin.Id);
            Map(plugin => plugin.Identifier).Length(255);
            Map(plugin => plugin.Status).CustomType(typeof(PluginStatus));
            Map(plugin => plugin.Version).Length(255);
            Map(plugin => plugin.CreateDate);
            HasMany(plugin => plugin.CurrentPluginLocales).KeyColumn("PluginId")
            .Table("PluginLocales").ApplyFilter<CultureFilter>()
            .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.Underscore)
            .Inverse()
            .LazyLoad()
            .Cascade.All();

        }
    }
}
