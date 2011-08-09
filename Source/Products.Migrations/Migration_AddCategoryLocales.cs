using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Products.Migrations
{
     /// <summary>
    /// Adds Product_CategoryLocales table.
    /// </summary>
    [Migration(20072011104000)]
    public class Migration_AddCategoryLocales : Migration
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
                t.Text("Culture").Length(10);
                t.ForeignKey("NewsCategoryId").Table("News_Categories").OnDelete(ForeignKeyConstraint.Cascade);

            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.ChangeTable("News_CategoryLocales", t => t.RemoveForeignKey("NewsCategoryId").Table("News_Categories"));
            Database.RemoveTable("News_CategoryLocales");
        }
    }
   
}
