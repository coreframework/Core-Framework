using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Profiles.Migrations
{
    /// <summary>
    /// Adds Profiles_ProfileLocales table.
    /// </summary>
    [Migration(6)]
    public class Migration_AddProfileElementLocales : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("Profiles_ProfileElementLocales", t =>
            {
                t.PrimaryKey();
                t.String("Title").Length(255);
                t.Text("ElementValues").Null();
                t.Text("Culture").Length(10).Null();
                t.ForeignKey("ProfileElement").Table("Profiles_ProfileElements").Column("ProfileElementId").OnDelete(ForeignKeyConstraint.Cascade);
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.ChangeTable("Profiles_ProfileElementLocales", t => t.RemoveForeignKey("ProfileElement").Table("Profiles_ProfileElements").Column("ProfileElementId"));
            Database.RemoveTable("Profiles_ProfileElementLocales");
        }
    }
}
