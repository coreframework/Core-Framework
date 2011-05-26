using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Web.Migrations
{
    /// <summary>
    /// Adds UsersToRoles table.
    /// </summary>
    [Migration(6)]
    public class MigrationAddUsersToRoles : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("UsersToRoles", t =>
            {
                t.PrimaryKey();
                t.ForeignKey("FK_UsersToRoles_Users").Table("Users").Column("UserId").OnDelete(ForeignKeyConstraint.Cascade);
                t.ForeignKey("FK_UsersToRoles_Roles").Table("Roles").Column("RoleId").OnDelete(ForeignKeyConstraint.Cascade);
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.ChangeTable("UsersToRoles", t =>
            {
                t.RemoveForeignKey("FK_UsersToRoles_Users").Table("Users");
                t.RemoveForeignKey("FK_UsersToRoles_Roles").Table("Roles");
            });
            Database.RemoveTable("UsersToRoles");
        }
    }
}