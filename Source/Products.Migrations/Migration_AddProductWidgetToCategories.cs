using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Products.Migrations
{

    /// <summary>
    /// Adds ProductWidgetToCategories table.
    /// </summary>
    [Migration(25072011144000)]
    public class Migration_AddProductWidgetToCategories : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("Product_ProductWidgetToCategories", t =>
            {
                t.PrimaryKey();
                t.ForeignKey("ProductWidget").Table("Product_ProductWidgets").Column("ProductWidgetId").OnDelete(ForeignKeyConstraint.Cascade);
                t.ForeignKey("Category").Table("Product_Categories").Column("CategoryId").OnDelete(ForeignKeyConstraint.Cascade);
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.ChangeTable("Product_ProductWidgetToCategories", t =>
            {
                t.RemoveForeignKey("FK_ProductWidgetToCategories_ProductWidgets").Table("Product_ProductWidgets");
                t.RemoveForeignKey("FK_ProductWidgetToCategories_Categories").Table("Product_Categories");
            });
            Database.RemoveTable("Product_ProductWidgetToCategories");
        }
    }
}
