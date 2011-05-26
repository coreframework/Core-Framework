using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Web.Migrations
{
    /// <summary>
    /// Adds UserGroupsToRoles table.
    /// </summary>
    [Migration(8)]
    public class MigrationAddUserGroupsToRoles : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("UserGroupsToRoles", t =>
            {
                t.PrimaryKey();
                t.ForeignKey("FK_UserGroupsToRoles_UserGroups").Table("UserGroups").Column("UserGroupId").OnDelete(ForeignKeyConstraint.Cascade);
                t.ForeignKey("FK_UserGroupsToRoles_Roles").Table("Roles").Column("RoleId").OnDelete(ForeignKeyConstraint.Cascade);
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.ChangeTable("UserGroupsToRoles", t =>
            {
                t.RemoveForeignKey("FK_UserGroupsToRoles_UserGroups").Table("UserGroups");
                t.RemoveForeignKey("FK_UserGroupsToRoles_Roles").Table("Roles");
            });
            Database.RemoveTable("UserGroupsToRoles");
        }
    }
}