using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.News.Migrations
{
    /// <summary>
    /// Adds News_CategoryLocales table.
    /// </summary>
    [Migration(6)]
    public class Migration_AddNewsCategoryWidget : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("News_CategoryWidgets", t =>
            {
                t.PrimaryKey();
                t.Integer("PageSize");

            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.RemoveTable("News_CategoryWidgets");
        }
    }
}
