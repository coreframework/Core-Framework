using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Web.Migrations
{
    /// <summary>
    /// Adds ContentPages table.
    /// </summary>
    [Migration(2)]
    public class MigrationAddWidgets : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("Widgets", t =>
            {
                t.PrimaryKey();
                t.String("Identifier");
                t.Integer("Status");
                t.ForeignKey("WidgetPlugin").Table("Plugins").Column("PluginId").OnDelete(ForeignKeyConstraint.Cascade);
            });
        }

       /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.ChangeTable("Widgets", t => t.RemoveForeignKey("WidgetPlugin").Table("Plugins"));
            Database.RemoveTable("Widgets");
        }
    }
}
