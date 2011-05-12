using Core.Web.NHibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.Web.NHibernate.Mappings
{
    public class MigrationMapping : ClassMap<Migration>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MigrationMapping"/> class.
        /// </summary>
        public MigrationMapping()
        {
            Cache.Region("Migrations").ReadWrite();
            Table("Migrations");
            Id(migration => migration.Id);
            Map(migration => migration.Version);
            References(migration => migration.Plugin).LazyLoad().Not.Nullable();
        }
    }
}
