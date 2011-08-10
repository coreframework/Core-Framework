using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Web.Migrations
{
    /// <summary>
    /// Adds PluginLocales table.
    /// </summary>
    [Migration(201109081129)]
    public class Migration_AddPluginLocales : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("PluginLocales", t =>
            {
                t.PrimaryKey();
                t.String("Title").Null();
                t.String("Description").Null();
                t.Text("Culture").Length(10).Null();
                t.ForeignKey("Plugin").Table("Plugins").OnDelete(ForeignKeyConstraint.Cascade);
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.ChangeTable("PluginLocales", t => t.RemoveForeignKey("Plugin").Table("Plugins"));
            Database.RemoveTable("PluginLocales");
        }
    }
}