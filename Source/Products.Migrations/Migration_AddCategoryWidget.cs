using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Products.Migrations
{
    /// <summary>
    /// Adds CategoryWidgets table.
    /// </summary>
    [Migration(19072011100940)]
    public class Migration_AddCategoryWidget : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("Product_CategoryWidgets", t =>
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
            Database.RemoveTable("Product_CategoryWidgets");
        }

    }
}
