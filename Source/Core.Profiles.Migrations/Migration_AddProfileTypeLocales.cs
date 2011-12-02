using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Profiles.Migrations
{
    /// <summary>
    /// Adds ProfileTypeLocales table.
    /// </summary>
    [Migration(2)]
    public class Migration_AddProfileTypeLocales : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("Profiles_ProfileTypeLocales", t =>
            {
                t.PrimaryKey();
                t.String("Title").Length(255);
                t.Text("Culture").Length(10).Null();
                t.ForeignKey("ProfileType").Table("Profiles_ProfileTypes").OnDelete(ForeignKeyConstraint.Cascade);
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.ChangeTable("Profiles_ProfileTypeLocales", t => t.RemoveForeignKey("ProfileType").Table("Profiles_ProfileTypes"));
            Database.RemoveTable("Profiles_ProfileTypeLocales");
        }
    }
}
