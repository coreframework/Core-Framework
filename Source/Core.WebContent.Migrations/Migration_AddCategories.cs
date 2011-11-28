using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.WebContent.Migrations
{
    /// <summary>
    /// Adds ContentPages table.
    /// </summary>
    [Migration(4)]
    public class Migration_AddCategories : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("WebContent_Categories", t =>
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
        public override void Revert()
        {
            Database.ChangeTable("WebContent_Categories", t => t.RemoveForeignKey("Section").Table("WebContent_Sections"));
            Database.RemoveTable("WebContent_Categories");
        }
    }
}
