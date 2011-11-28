using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Web.Migrations
{
    /// <summary>
    /// Adds PageLayouts table.
    /// </summary>
    [Migration(13)]
    public class Migration_AddPageLayouts : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("PageLayouts", t =>
            {
                t.PrimaryKey();
                t.ForeignKey("PageLayoutsTemplate").Table("PageLayoutTemplates").Column("TemplateId").OnDelete(ForeignKeyConstraint.Cascade);
                t.ForeignKey("PageLayoutsPage").Table("Pages").Column("PageId").OnDelete(ForeignKeyConstraint.Cascade);
            });
        }

       /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.ChangeTable("PageLayouts", t => t.RemoveForeignKey("PageLayoutsTemplate").Table("PageLayoutTemplates"));
            Database.ChangeTable("PageLayouts", t => t.RemoveForeignKey("PageLayoutsPage").Table("Pages"));
            Database.RemoveTable("PageLayouts");
        }
    }
}
