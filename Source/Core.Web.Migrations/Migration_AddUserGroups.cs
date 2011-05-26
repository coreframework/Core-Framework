using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Web.Migrations
{
    /// <summary>
    /// Adds UserGroups table.
    /// </summary>
    [Migration(7)]
    public class MigrationAddUserGroups : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("UserGroups", t =>
            {
                t.PrimaryKey();
                t.String("Name");
                t.Text("Description").Null();
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.RemoveTable("UserGroups");
        }
    }
}

