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
            Database.AddTable("NewsArticleLocales", t =>
            {
                t.PrimaryKey();
                t.String("Title");
                t.Text("Content");
                t.Text("Culture").Length(10);
                t.ForeignKey("NewsArticle").Table("News").OnDelete(ForeignKeyConstraint.Cascade);
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.ChangeTable("NewsArticleLocales", t => t.RemoveForeignKey("NewsArticle").Table("News"));
            Database.RemoveTable("NewsArticleLocales");
        }
    }
}
