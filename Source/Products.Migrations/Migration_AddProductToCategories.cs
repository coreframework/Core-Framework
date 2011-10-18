using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Products.Migrations
{
    /// <summary>
    /// Adds ProductsToCategories table.
    /// </summary>
    [Migration(15072011130400)]
    public class Migration_AddProductToCategories:Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("Product_ProductsToCategories", t =>
            {
                t.PrimaryKey();
                t.ForeignKey("Product").Table("Product_Products").Column("ProductId").OnDelete(ForeignKeyConstraint.Cascade);
                t.ForeignKey("Category").Table("Product_Categories").Column("CategoryId").OnDelete(ForeignKeyConstraint.Cascade);
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.ChangeTable("Product_ProductsToCategories", t =>
            {
                t.RemoveForeignKey("FK_ProductsToCategories_Products").Table("Product_Products");
                t.RemoveForeignKey("FK_ProductsToCategories_Categories").Table("Product_Categories");
            });
            Database.RemoveTable("Product_ProductsToCategories");
        }
    }
}
