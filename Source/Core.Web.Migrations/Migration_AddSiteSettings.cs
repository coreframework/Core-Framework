using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Web.Migrations
{
    /// <summary>
    /// Adds SiteSettings table.
    /// </summary>
    [Migration(23)]
    public class Migration_AddSiteSettings : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("SiteSettings", t =>
            {
                t.PrimaryKey();
                t.Bool("ShowMainMenu");
            });
        }

       /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.RemoveTable("SiteSettings");
        }
    }
}
