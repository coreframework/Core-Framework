using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Web.Migrations
{
    /// <summary>
    /// Adds Pages table.
    /// </summary>
    [Migration(11)]
    public class Migration_AddPages : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("Pages", t =>
            {
                t.PrimaryKey();
                t.String("Url");
                t.Long("ParentPageId").Null();
                t.Integer("OrderNumber").Null();
                t.Bool("HideInMainMenu").Null();
                t.Bool("IsServicePage").Null();
                t.ForeignKey("PageUser").Table("Users").Column("UserId").NotRequired().OnDelete(ForeignKeyConstraint.SetNull);
            });
        }

       /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.ChangeTable("Pages", t => t.RemoveForeignKey("PageUser").Table("Users"));
            Database.RemoveTable("Pages");
        }
    }
}
