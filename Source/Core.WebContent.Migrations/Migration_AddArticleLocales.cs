using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.WebContent.Migrations
{
    /// <summary>
    /// Adds ContentPageLocales table.
    /// </summary>
    [Migration(7)]
    public class Migration_AddArticleLocales : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("WebContent_ArticleLocales", t =>
            {
                t.PrimaryKey();
                t.String("Title").Length(255);
                t.Text("Description").Null();
                t.Text("Culture").Length(10).Null();
                t.ForeignKey("Article").Table("WebContent_Articles").OnDelete(ForeignKeyConstraint.Cascade);
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.ChangeTable("WebContent_ArticleLocales", t => t.RemoveForeignKey("Article").Table("WebContent_Articles"));
            Database.RemoveTable("WebContent_ArticleLocales");
        }
    }
}
