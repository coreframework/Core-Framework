using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Web.Migrations
{
    /// <summary>
    /// Adds PageWidgetSettings table.
    /// </summary>
    [Migration(19)]
    public class Migration_AddPageWidgetSettings : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("PageWidgetSettings", t =>
            {
                t.PrimaryKey();
                t.Text("CustomCssClasses").Null();

                t.ForeignKey("PageWidgetSettingsLookAndFeel").Table("LookAndFeelSettings").Column("LookAndFeelSettingsId").OnDelete(ForeignKeyConstraint.Cascade);
                t.ForeignKey("PageWidgetSettingsWidget").Table("PageWidgets").Column("WidgetId").OnDelete(ForeignKeyConstraint.Cascade);
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.ChangeTable("PageWidgetSettings", t => t.RemoveForeignKey("PageWidgetSettingsLookAndFeel").Table("LookAndFeelSettings"));
            Database.ChangeTable("PageWidgetSettings", t => t.RemoveForeignKey("PageWidgetSettingsWidget").Table("PageWidgets"));

            Database.RemoveTable("PageWidgetSettings");
        }
    }
}
