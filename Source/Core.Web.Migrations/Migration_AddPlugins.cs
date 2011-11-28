using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Web.Migrations
{
    /// <summary>
    /// Adds Plugins table.
    /// </summary>
    [Migration(1)]
    public class MigrationAddPlugins : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("Plugins", t =>
            {
                t.PrimaryKey();
                t.String("Identifier");
                t.Integer("Status");
                t.DateTime("CreateDate");
                t.String("Version").Null();
            });
        }

       /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.RemoveTable("Plugins");
        }
    }
}
