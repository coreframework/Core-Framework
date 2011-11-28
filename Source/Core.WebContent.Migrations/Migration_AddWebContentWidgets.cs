using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.WebContent.Migrations
{
    /// <summary>
    /// Adds ContentPages table.
    /// </summary>
    [Migration(9)]
    public class Migration_AddWebContentWidgets : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("WebContent_WebContentWidgets", t =>
            {
                t.PrimaryKey();
                t.Bool("ShowPagination");
                t.Integer("ItemsNumber").Null();
                t.Integer("ViewMode");
                t.ForeignKey("Article").Table("WebContent_Articles").NotRequired();
                t.ForeignKey("Section").Table("WebContent_Sections");
            });
        }

       /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.ChangeTable("WebContent_WebContentWidgets", t => t.RemoveForeignKey("Article").Table("WebContent_Articles"));
            Database.ChangeTable("WebContent_WebContentWidgets", t => t.RemoveForeignKey("Section").Table("WebContent_Sections"));
            Database.RemoveTable("WebContent_WebContentWidgets");
        }
    }
}
