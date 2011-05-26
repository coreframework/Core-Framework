using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Web.Migrations
{
    /// <summary>
    /// Adds PageWidgets table.
    /// </summary>
    [Migration(18)]
    public class Migration_AddPageWidgets : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("PageWidgets", t =>
            {
                t.PrimaryKey();
                t.Long("InstanceId").Null();
                t.String("WidgetIdentifier");
                t.Integer("ColumnNumber");
                t.Integer("OrderNumber");

                t.ForeignKey("PageWidgetsUser").Table("Users").Column("UserId").NotRequired().OnDelete(ForeignKeyConstraint.SetNull);

                t.ForeignKey("PageWidgetsPage").Table("Pages").Column("PageId").OnDelete(ForeignKeyConstraint.Cascade);

            });
        }

       /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.ChangeTable("PageWidgets", t => t.RemoveForeignKey("PageWidgetsUser").Table("Users"));
            Database.ChangeTable("PageWidgets", t => t.RemoveForeignKey("PageWidgetsPage").Table("Pages"));

            Database.RemoveTable("PageWidgets");
        }
    }
}
