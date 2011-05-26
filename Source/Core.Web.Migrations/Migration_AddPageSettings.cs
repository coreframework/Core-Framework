using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Web.Migrations
{
    /// <summary>
    /// Adds PageSettings table.
    /// </summary>
    [Migration(17)]
    public class Migration_AddPageSettings : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("PageSettings", t =>
            {
                t.PrimaryKey();
                t.Text("CustomCss").Null();

                t.ForeignKey("PageSettingsLookAndFeel").Table("LookAndFeelSettings").Column("LookAndFeelSettingsId").OnDelete(ForeignKeyConstraint.Cascade);
                t.ForeignKey("PageSettingsPage").Table("Pages").Column("PageId").OnDelete(ForeignKeyConstraint.Cascade);
            });
        }

       /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.ChangeTable("PageSettings", t => t.RemoveForeignKey("PageSettingsLookAndFeel").Table("LookAndFeelSettings"));
            Database.ChangeTable("PageSettings", t => t.RemoveForeignKey("PageSettingsPage").Table("Pages"));

            Database.RemoveTable("PageSettings");
        }
    }
}
