using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.ContentPages.Migrations
{
    /// <summary>
    /// Adds ContentPageWidgets table.
    /// </summary>
    [Migration(2)]
    public class Migration_AddContentPageWidgets : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("ContentPageWidgets", t =>
            {
                t.PrimaryKey();
                t.ForeignKey("ContentPage").Table("ContentPages");
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.ChangeTable("ContentPageWidgets", t => t.RemoveForeignKey("ContentPage").Table("ContentPages"));
            Database.RemoveTable("ContentPageWidgets");
        }
    }
}
