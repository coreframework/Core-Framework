using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Web.Migrations
{
    /// <summary>
    /// Adds UserGroupsMembers table.
    /// </summary>
    [Migration(9)]
    public class MigrationAddUserGroupsMembers : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("UserGroupsMembers", t =>
            {
                t.PrimaryKey();
                t.ForeignKey("FK_UserGroupsMembers_Users").Table("Users").Column("UserId").OnDelete(ForeignKeyConstraint.Cascade);
                t.ForeignKey("FK_UserGroupsMembers_UserGroups").Table("UserGroups").Column("UserGroupId").OnDelete(ForeignKeyConstraint.Cascade);
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.ChangeTable("UserGroupsMembers", t =>
            {
                t.RemoveForeignKey("FK_UserGroupsMembers_Users").Table("Users");
                t.RemoveForeignKey("FK_UserGroupsMembers_UserGroups").Table("UserGroups");
            });
            Database.RemoveTable("UserGroupsMembers");
        }
    }
}

