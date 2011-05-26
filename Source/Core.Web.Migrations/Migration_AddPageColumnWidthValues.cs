using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Web.Migrations
{
    /// <summary>
    /// Adds PageLayoutColumnWidthValues table.
    /// </summary>
    [Migration(16)]
    public class Migration_AddPageLayoutColumnWidthValues : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("PageLayoutColumnWidthValues", t =>
            {
                t.PrimaryKey();
                t.Integer("WidthValue");
                t.Integer("Colspan").Null();

                t.ForeignKey("PageLayoutColumnWidthValuesColumn").Table("PageLayoutColumns").Column("ColumnId").OnDelete(ForeignKeyConstraint.Cascade);
                t.ForeignKey("PageLayoutColumnWidthValuesLayout").Table("PageLayouts").Column("PageLayoutId").OnDelete(ForeignKeyConstraint.Cascade);
            });
        }

       /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.ChangeTable("PageLayoutColumnWidthValues", t => t.RemoveForeignKey("PageLayoutColumnWidthValuesColumn").Table("PageLayoutColumns"));
            Database.ChangeTable("PageLayoutColumnWidthValues", t => t.RemoveForeignKey("PageLayoutColumnWidthValuesLayout").Table("PageLayouts"));

            Database.RemoveTable("PageLayoutColumnWidthValues");
        }
    }
}
