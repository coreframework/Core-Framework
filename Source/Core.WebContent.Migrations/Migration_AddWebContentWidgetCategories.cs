using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.WebContent.Migrations
{
    /// <summary>
    /// Adds ContentPages table.
    /// </summary>
    [Migration(10)]
    public class Migration_AddWebContentWidgetCategories : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("WebContent_WebContentWidgetCategories", t =>
            {
                t.PrimaryKey();
                t.ForeignKey("WebContentWidget").Table("WebContent_WebContentWidgets").OnDelete(ForeignKeyConstraint.Cascade);
                t.ForeignKey("Category").Table("WebContent_Categories").OnDelete(ForeignKeyConstraint.Cascade);
            });
        }

       /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.ChangeTable("WebContent_WebContentWidgetCategories", t => t.RemoveForeignKey("WebContentWidget").Table("WebContent_WebContentWidgets"));
            Database.ChangeTable("WebContent_WebContentWidgetCategories", t => t.RemoveForeignKey("Category").Table("WebContent_Categories"));
            Database.RemoveTable("WebContent_WebContentWidgetCategories");
        }
    }
}
