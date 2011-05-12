using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.TestModule.Migrations
{
    /// <summary>
    /// Adds ContentPages table.
    /// </summary>
    [Migration(2000)]
    public class Migration_AddContentPages2 : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("ContentPages444", t =>
            {
                t.PrimaryKey();
                t.String("Title");
                t.Text("Content");
                t.DateTime("CreateDate");
                t.DateTime("LastModifiedDate");
            });
        }

       /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.RemoveTable("ContentPages444");
        }
    }
}
