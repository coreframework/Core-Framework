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
        public override void Up()
        {
            Database.AddTable("Plugins", t =>
            {
                t.PrimaryKey();
                t.String("Identifier");
                t.Integer("Status");
                t.DateTime("CreateDate");
                t.String("Version").Null();
                t.String("Title").Null();
            });
        }

       /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.RemoveTable("Plugins");
        }
    }
}
