using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Profiles.Migrations
{
    /// <summary>
    /// Adds Profiles_RegistrationWidgets table.
    /// </summary>
    [Migration(9)]
    public class Migration_AddRegistrationWidgets : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("Profiles_RegistrationWidgets", t =>
            {
                t.PrimaryKey();
                t.ForeignKey("ProfileType").Table("Profiles_ProfileTypes").OnDelete(ForeignKeyConstraint.Cascade);
            });
        }

       /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.ChangeTable("Profiles_RegistrationWidgets", t => t.RemoveForeignKey("ProfileType").Table("Profiles_ProfileTypes").Column("ProfileTypeId"));
            Database.RemoveTable("Profiles_RegistrationWidgets");
        }
    }
}
