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
        public override void Apply()
        {
            Database.AddTable("Pages", t =>
            {
                t.PrimaryKey();
                t.String("Url");
                t.Long("ParentPageId").Null();
                t.Integer("OrderNumber").Null();
                t.Bool("HideInMainMenu");
                t.Bool("IsServicePage");
                t.Bool("IsTemplate");
                t.ForeignKey("PageUser").Table("Users").Column("UserId").NotRequired().OnDelete(ForeignKeyConstraint.SetNull);
                t.ForeignKey("PageTemplate").Table("Pages").Column("TemplateId").NotRequired().OnDelete(ForeignKeyConstraint.SetNull);
            });
        }

       /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.ChangeTable("Pages", t => t.RemoveForeignKey("PageUser").Table("Users"));
            Database.ChangeTable("Pages", t => t.RemoveForeignKey("PageTemplate").Table("Pages"));
            Database.RemoveTable("Pages");
        }
    }
}
