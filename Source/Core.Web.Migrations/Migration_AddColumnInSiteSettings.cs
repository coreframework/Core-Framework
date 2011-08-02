using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ECM7.Migrator.Framework;

namespace Core.Web.Migrations
{
    /// <summary>
    /// Add Column In Site Settings
    /// </summary>
    [Migration(24)]
    public class Migration_AddColumnInSiteSettings : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddColumn("SiteSettings", "ShowPanel", DbType.Boolean, true);
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.RemoveColumn("SiteSettings", "ShowPanel");
        }
    }
}
