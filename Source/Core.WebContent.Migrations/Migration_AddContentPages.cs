using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.ContentPages.Migrations
{
    /// <summary>
    /// Adds ContentPages table.
    /// </summary>
    [Migration(1)]
    public class Migration_AddContentPages : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("ContentPages", t =>
            {
                t.PrimaryKey();
                t.DateTime("CreateDate").Null();
                t.DateTime("LastModifiedDate").Null();
            });
        }

       /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.RemoveTable("ContentPages");
        }
    }
}
