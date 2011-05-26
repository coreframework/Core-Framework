using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Web.Migrations
{
    /// <summary>
    /// Adds Users table.
    /// </summary>
    [Migration(4)]
    public class MigrationAddUsers : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("Users", t =>
            {
                t.PrimaryKey();
                t.String("Username");
                t.String("Email");
                t.String("Hash");
                t.String("Salt");
                t.Integer("EncryptionMode");
                t.Integer("Status");
            });
        }

       /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.RemoveTable("Users");
        }
    }
}
