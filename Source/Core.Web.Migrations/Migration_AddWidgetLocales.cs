using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Web.Migrations
{
    /// <summary>
    /// Adds PluginLocales table.
    /// </summary>
    [Migration(201109081214)]
    public class Migration_AddWidgetLocales : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("WidgetLocales", t =>
            {
                t.PrimaryKey();
                t.String("Title").Null();
                t.Text("Culture").Length(10).Null();
                t.ForeignKey("Widget").Table("Widgets").OnDelete(ForeignKeyConstraint.Cascade);
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.ChangeTable("WidgetLocales", t => t.RemoveForeignKey("Widget").Table("Widgets"));
            Database.RemoveTable("WidgetLocales");
        }
    }
}