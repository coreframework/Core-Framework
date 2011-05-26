using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Web.Migrations
{
    /// <summary>
    /// Adds ContentPages table.
    /// </summary>
    [Migration(14)]
    public class Migration_AddPageLayoutRows : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("PageLayoutRows", t =>
            {
                t.PrimaryKey();

                t.ForeignKey("PageLayoutRowsTemplate").Table("PageLayoutTemplates").Column("TemplateId");
            });
        }

       /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.ChangeTable("PageLayoutRows", t => t.RemoveForeignKey("PageLayoutRowsTemplate").Table("PageLayoutTemplates"));

            Database.RemoveTable("PageLayoutRows");
        }
    }
}
