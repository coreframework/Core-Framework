using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.WebContent.Migrations
{
    /// <summary>
    /// Adds ContentPages table.
    /// </summary>
    [Migration(6)]
    public class Migration_AddArticles : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("WebContent_Articles", t =>
            {
                t.PrimaryKey();
                t.DateTime("CreateDate").Null();
                t.Long("UserId").Null();
                t.Integer("Status");
                t.ForeignKey("Section").Table("WebContent_Sections");
            });
        }

       /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.ChangeTable("WebContent_Articles", t => t.RemoveForeignKey("Section").Table("WebContent_Sections"));
            Database.RemoveTable("WebContent_Articles");
        }
    }
}
