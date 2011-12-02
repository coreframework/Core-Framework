using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Profiles.Migrations
{
    /// <summary>
    /// Adds ProfileTypeLocales table.
    /// </summary>
    [Migration(4)]
    public class Migration_AddProfileHeaderLocales : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("Profiles_ProfileHeaderLocales", t =>
            {
                t.PrimaryKey();
                t.String("Title").Length(255);
                t.Text("Culture").Length(10).Null();
                t.ForeignKey("ProfileHeader").Table("Profiles_ProfileHeaders").OnDelete(ForeignKeyConstraint.Cascade);
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.ChangeTable("Profiles_ProfileHeaderLocales", t => t.RemoveForeignKey("ProfileHeader").Table("Profiles_ProfileHeaders"));
            Database.RemoveTable("Profiles_ProfileHeaderLocales");
        }
    }
}
