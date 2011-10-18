using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Products.Migrations
{
    /// <summary>
    /// Adds Categories table.
    /// </summary>
    [Migration(15072011130000)]
    public class Migration_AddCategory : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("Product_Categories", t =>
            {
                t.PrimaryKey();
                t.DateTime("CreateDate").Null();
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.RemoveTable("Product_Categories");
        }
    }
}
