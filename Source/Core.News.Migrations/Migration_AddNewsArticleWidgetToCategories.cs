using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.News.Migrations
{

    /// <summary>
    /// Adds ProductWidgetToCategories table.
    /// </summary>
    [Migration(7)]
    public class Migration_AddNewsArticleWidgetToCategories : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("News_ArticleWidgetToCategories", t =>
            {
                t.PrimaryKey();
                t.ForeignKey("NewsArticleWidget").Table("News_ArticleWidgets").Column("NewsArticleWidgetId").OnDelete(ForeignKeyConstraint.Cascade);
                t.ForeignKey("Category").Table("News_Categories").Column("CategoryId").OnDelete(ForeignKeyConstraint.Cascade);
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.ChangeTable("News_ArticleWidgetToCategories", t =>
            {
                t.RemoveForeignKey("FK_ArticleWidgetToCategories_ArticleWidgets").Table("News_ArticleWidgets");
                t.RemoveForeignKey("FK_ArticleWidgetToCategories_Categories").Table("News_Categories");
            });
            Database.RemoveTable("News_ArticleWidgetToCategories");
        }
    }
}
