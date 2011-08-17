using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.News.Migrations
{
    /// <summary>
    /// Adds News_CategoryLocales table.
    /// </summary>
    [Migration(5)]
    public class Migration_AddNewsCategoryLocales : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("News_CategoryLocales", t =>
            {
                t.PrimaryKey();
                t.String("Title");
                t.Text("Description");
                t.Text("Culture").Length(10).Null();
                t.ForeignKey("Category").Table("News_Categories").OnDelete(ForeignKeyConstraint.Cascade);

            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.ChangeTable("News_CategoryLocales", t => t.RemoveForeignKey("Category").Table("News_Categories"));
            Database.RemoveTable("News_CategoryLocales");
        }
    }
}
