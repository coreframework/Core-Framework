using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Products.Migrations
{
    /// <summary>
    /// Adds ProductWidgets table.
    /// </summary>
    [Migration(09072011101400)]
    public class Migration_AddProductWidgets : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("Product_ProductWidgets", t =>
            {
                t.PrimaryKey();
                t.Integer("PageSize");
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.RemoveTable("Product_ProductWidgets");
        }
    }
}
