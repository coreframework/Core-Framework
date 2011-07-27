using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Products.Migrations
{
    /// <summary>
    /// Adds Products table.
    /// </summary>
    [Migration(09072011101000)]
    public class Migration_AddProducts : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("Product_Products", t =>
            {
                t.PrimaryKey();
                t.DateTime("CreateDate").Null();
                t.DateTime("LastModifiedDate").Null();
                t.String("FileName").Null();
                t.Integer("Price");
                
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.RemoveTable("Product_Products");
        }
    }
}
