using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Web.Migrations
{
    /// <summary>
    /// Adds Roles table.
    /// </summary>
    [Migration(5)]
    public class MigrationAddRoles : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("Roles", t =>
            {
                t.PrimaryKey();
                t.Bool("IsSystemRole");
                t.Bool("NotAssignableRole");
                t.Bool("NotPermissible");
            });
        }

       /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.RemoveTable("Roles");
        }
    }
}
