using System;
using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.ContentPages.Migrations
{
    /// <summary>
    /// Adds ContentPages table.
    /// </summary>
    [Migration(1000)]
    public class Migration_AddContentPages : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("ContentPages333", t =>
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
            Database.RemoveTable("ContentPages333");
        }
    }
}
