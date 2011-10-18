using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.News.Migrations
{
    /// <summary>
    /// Adds News table.
    /// </summary>
    [Migration(1)]
    public class Migration_AddNewsArticles : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("News_News", t =>
            {
                t.PrimaryKey();
                t.Integer("StatusId").Null();
                t.DateTime("CreateDate").Null();
                t.DateTime("LastModifiedDate").Null();
                t.DateTime("PublishDate").Null();
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.RemoveTable("News_News");
        }
    }
}
