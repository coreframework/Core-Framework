using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.News.Migrations
{
    /// <summary>
    /// Adds NewsArticlesLocales table.
    /// </summary>
    [Migration(3)]
    public class Migration_AddNewsArticlesLocales : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("News_ArticleLocales", t =>
            {
                t.PrimaryKey();
                t.String("Title");
                t.String("Summary");
                t.Text("Content");
                t.Text("Culture").Length(10).Null();;
                t.ForeignKey("NewsArticle").Table("News_News").OnDelete(ForeignKeyConstraint.Cascade);
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.ChangeTable("NewsArticleLocales", t => t.RemoveForeignKey("NewsArticle").Table("News_News"));
            Database.RemoveTable("NewsArticleLocales");
        }
    }
}
