using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.News.Migrations
{
    /// <summary>
    /// Adds NewsArticlesWidgets table.
    /// </summary>
    [Migration(2)]
    public class Migration_AddNewsArticlesWidgets : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("News_ArticleWidgets", t =>
            {
                t.PrimaryKey();
                t.Integer("ItemsOnPage");
                t.Bool("ShowPaginator");
                t.String("Url").Null();
                //t.ForeignKey("NewsArticleId").Table("News").OnDelete(ForeignKeyConstraint.Cascade);
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            //Database.ChangeTable("NewsArticleWidgets", t => t.RemoveForeignKey("NewsArticleId").Table("News"));
            Database.RemoveTable("News_ArticleWidgets");
        }
    }
}
