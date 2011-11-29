using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Navigation.Migrations
{
    /// <summary>
    /// Adds ContentPages table.
    /// </summary>
    [Migration(4)]
    public class Migration_AddListMenuWidgetPages : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("ListMenuWidgetPages", t =>
            {
                t.PrimaryKey();

                t.ForeignKey("ListMenuWidgetPagesPage").Table("Pages").Column("PageId").OnDelete(ForeignKeyConstraint.Cascade);
                t.ForeignKey("ListMenuWidgetPagesWidget").Table("ListMenuWidgets").Column("ListMenuWidgetId").NotRequired().OnDelete(ForeignKeyConstraint.Cascade);
               
            });
        }

       /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.ChangeTable("ListMenuWidgetPages", t => t.RemoveForeignKey("ListMenuWidgetPagesPage").Table("Pages").Column("PageId"));
            Database.ChangeTable("ListMenuWidgetPages", t => t.RemoveForeignKey("ListMenuWidgetPagesWidget").Table("ListMenuWidgets").Column("ListMenuWidgetId"));

            Database.RemoveTable("ListMenuWidgetPages");
        }
    }
}
