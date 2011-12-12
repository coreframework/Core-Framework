using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Profiles.Migrations
{
    /// <summary>
    /// Adds Profiles_UserProfiles table.
    /// </summary>
    [Migration(7)]
    public class Migration_AddUserProfiles : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("Profiles_UserProfiles", t =>
            {
                t.PrimaryKey();
                t.ForeignKey("ProfileType").Table("Profiles_ProfileTypes").OnDelete(ForeignKeyConstraint.Cascade);
                t.ForeignKey("User").Table("Users").OnDelete(ForeignKeyConstraint.Cascade);
            });
        }

       /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.ChangeTable("Profiles_UserProfiles", t => t.RemoveForeignKey("ProfileType").Table("Profiles_ProfileTypes").Column("ProfileTypeId"));
            Database.ChangeTable("Profiles_UserProfiles", t => t.RemoveForeignKey("User").Table("Users").Column("UserId"));
            Database.RemoveTable("Profiles_UserProfiles");
        }
    }
}
