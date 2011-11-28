using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Web.Migrations
{
    /// <summary>
    /// Adds PageLayoutColumns table.
    /// </summary>
    [Migration(15)]
    public class Migration_AddPageLayoutColumns : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("PageLayoutColumns", t =>
            {
                t.PrimaryKey();
                t.Integer("DefaultWidthValue");
                t.Integer("DefaultColspan").Null();

                t.ForeignKey("PageLayoutColumnsRow").Table("PageLayoutRows").Column("RowId").OnDelete(ForeignKeyConstraint.Cascade);
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.ChangeTable("PageLayoutColumns", t => t.RemoveForeignKey("PageLayoutColumnsRow").Table("PageLayoutRows"));

            Database.RemoveTable("PageLayoutColumns");
        }
    }
}
