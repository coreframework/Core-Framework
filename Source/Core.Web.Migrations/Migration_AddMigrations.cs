using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Web.Migrations
{
    /// <summary>
    /// Adds Migrations table.
    /// </summary>
    [Migration(3)]
    public class MigrationAddMigrations : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("Migrations", t =>
            {
                t.PrimaryKey();
                t.Long("Version");
                t.ForeignKey("MigrationsPlugin").Table("Plugins").Column("PluginId").OnDelete(ForeignKeyConstraint.Cascade); ;
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.ChangeTable("Migrations", t => t.RemoveForeignKey("MigrationsPlugin").Table("Plugins"));
            Database.RemoveTable("Migrations");
        }
    }
}