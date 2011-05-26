using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Web.Migrations
{
    /// <summary>
    /// Adds LookAndFeelSettings table.
    /// </summary>
    [Migration(10)]
    public class MigrationAddLookAndFeelSettings : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("LookAndFeelSettings", t =>
            {
                t.PrimaryKey();
                t.String("BackgroundColor").Length(7).Null();
                t.String("FontFamily").Length(50).Null();
                t.Double("FontSizeValue").Null();
                t.String("FontSizeUnit").Length(50).Null();
                t.String("Color").Length(7).Null();
                t.Text("Content").Null();
                t.Double("WidthValue").Null();
                t.String("WidthUnit").Length(50).Null();
                t.Double("HeightValue").Null();
                t.String("HeightUnit").Length(50).Null();
                t.Text("OtherStyles").Null();
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.RemoveTable("LookAndFeelSettings");
        }
    }
}
