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
        public override void Apply()
        {
            Database.AddTable("SiteSettings", t =>
            {
                t.PrimaryKey();
                t.Bool("ShowPanel");
                t.String("WebsiteName").Length(255).Null();
            });
        }

       /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.RemoveTable("SiteSettings");
        }
    }
}
