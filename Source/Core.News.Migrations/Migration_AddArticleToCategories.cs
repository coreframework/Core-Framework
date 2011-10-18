using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.News.Migrations
{
    /// <summary>
    /// Adds News_CategoryLocales table.
    /// </summary>
    [Migration(8)]
    public class Migration_AddArticleToCategories : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("News_ArticlesToCategories", t =>
            {
                t.PrimaryKey();
                t.ForeignKey("Article").Table("News_News").Column("ArticleId").OnDelete(ForeignKeyConstraint.Cascade);
                t.ForeignKey("Category").Table("News_Categories").Column("CategoryId").OnDelete(ForeignKeyConstraint.Cascade);
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.ChangeTable("News_ArticlesToCategories", t =>
            {
                t.RemoveForeignKey("FK_ArticlesToCategories_Articles").Table("News_News");
                t.RemoveForeignKey("FK_ArticlesToCategories_Categories").Table("News_Categories");
            });
            Database.RemoveTable("News_ArticlesToCategories");
        }
    }
}
