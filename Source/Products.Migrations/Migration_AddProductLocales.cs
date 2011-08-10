using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Products.Migrations
{
    /// <summary>
    /// Adds Product_ProductLocales table.
    /// </summary>
    [Migration(20072011103000)]
    public class Migration_AddProductLocales : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("Product_ProductLocales", t =>
            {
                t.PrimaryKey();
                t.String("Title");
                t.Text("Description");
                t.Text("Culture").Length(10).Null();
                t.ForeignKey("Product").Table("Product_Products").OnDelete(ForeignKeyConstraint.Cascade);

            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.ChangeTable("Product_ProductLocales", t => t.RemoveForeignKey("Product").Table("Product_Products"));
            Database.RemoveTable("Product_ProductLocales");
        }
    }
}
