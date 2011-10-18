using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.News.Migrations
{
    /// <summary>
    /// Adds Categories table.
    /// </summary>
    [Migration(4)]
    public class Migration_AddNewsCategory : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("News_Categories", t =>
            {
                t.PrimaryKey();
                t.DateTime("CreateDate").Null();
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.RemoveTable("News_Categories");
        }
    }
}
