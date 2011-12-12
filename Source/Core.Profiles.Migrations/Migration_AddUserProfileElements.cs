using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Profiles.Migrations
{
    /// <summary>
    /// Adds Profiles_UserProfiles table.
    /// </summary>
    [Migration(8)]
    public class Migration_AddUserProfileElements : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("Profiles_UserProfileElements", t =>
            {
                t.PrimaryKey();
                t.Text("Value");
                t.ForeignKey("UserProfile").Table("Profiles_UserProfiles").OnDelete(ForeignKeyConstraint.Cascade);
                t.ForeignKey("ProfileElement").Table("Profiles_ProfileElements");
            });
        }

       /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.ChangeTable("Profiles_UserProfileElements", t => t.RemoveForeignKey("UserProfile").Table("Profiles_UserProfiles").Column("UserProfileId"));
            Database.ChangeTable("Profiles_UserProfileElements", t => t.RemoveForeignKey("ProfileElement").Table("Profiles_ProfileElements").Column("ProfileElementId"));
            Database.RemoveTable("Profiles_UserProfileElements");
        }
    }
}
