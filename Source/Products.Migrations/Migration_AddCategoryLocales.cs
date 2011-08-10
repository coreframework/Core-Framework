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
            Database.AddTable("Product_CategoryLocales", t =>
            {
                t.PrimaryKey();
                t.String("Title");
                t.Text("Description");
                t.Text("Culture").Length(10);
                t.ForeignKey("CategoryId").Table("Product_Categories").OnDelete(ForeignKeyConstraint.Cascade);

            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.ChangeTable("Product_CategoryLocales", t => t.RemoveForeignKey("ProductCategoryId").Table("Product_Categories"));
            Database.RemoveTable("Product_CategoryLocales");
        }
    }
   
}
