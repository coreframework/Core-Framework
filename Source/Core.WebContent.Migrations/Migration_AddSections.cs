using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.WebContent.Migrations
{
    /// <summary>
    /// Adds ContentPages table.
    /// </summary>
    [Migration(1)]
    public class Migration_AddSections : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("WebContent_Sections", t =>
            {
                t.PrimaryKey();
                t.DateTime("CreateDate").Null();
                t.Long("UserId").Null();
            });
        }

       /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.RemoveTable("WebContent_Sections");
        }
    }
}
