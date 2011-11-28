using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.WebContent.Migrations
{
    /// <summary>
    /// Adds ContentPageLocales table.
    /// </summary>
    [Migration(8)]
    public class Migration_AddArticleFiles : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("WebContent_ArticleFiles", t =>
            {
                t.PrimaryKey();
                t.String("Title").Length(255);
                t.Text("FileName").Null();
                t.ForeignKey("Article").Table("WebContent_Articles").OnDelete(ForeignKeyConstraint.Cascade);
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.ChangeTable("WebContent_ArticleFiles", t => t.RemoveForeignKey("Article").Table("WebContent_Articles"));
            Database.RemoveTable("WebContent_ArticleFiles");
        }
    }
}
