using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.ContentPages.Migrations
{
    /// <summary>
    /// Adds ContentPageLocales table.
    /// </summary>
    [Migration(3)]
    public class Migration_AddContentPageLocales : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("ContentPageLocales", t =>
            {
                t.PrimaryKey();
                t.String("Title");
                t.Text("Content");
                t.Text("Culture").Length(10);
                t.ForeignKey("ContentPage").Table("ContentPages").OnDelete(ForeignKeyConstraint.Cascade);
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.ChangeTable("ContentPageLocales", t => t.RemoveForeignKey("ContentPage").Table("ContentPages"));
            Database.RemoveTable("ContentPageLocales");
        }
    }
}
