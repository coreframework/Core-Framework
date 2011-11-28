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
        public override void Apply()
        {
            Database.AddTable("WebContent_Articles", t =>
            {
                t.PrimaryKey();
                t.DateTime("CreateDate").Null();
                t.Long("UserId").Null();
                t.Integer("Status");
                t.String("Author").Null();
                t.DateTime("StartPublishingDate").Null();
                t.DateTime("FinishPublishingDate").Null();
                t.DateTime("LastModifiedDate").Null();
                t.String("Url");
                t.Integer("UrlType");
                t.ForeignKey("Category").Table("WebContent_Categories");
            });
        }

       /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.ChangeTable("WebContent_Articles", t => t.RemoveForeignKey("Category").Table("WebContent_Categories"));
            Database.RemoveTable("WebContent_Articles");
        }
    }
}
