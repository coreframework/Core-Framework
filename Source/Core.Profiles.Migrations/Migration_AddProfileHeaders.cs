using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Profiles.Migrations
{
    /// <summary>
    /// Adds ProfileTypes table.
    /// </summary>
    [Migration(3)]
    public class Migration_AddProfileHeaders : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("Profiles_ProfileHeaders", t =>
            {
                t.PrimaryKey();
                t.Integer("OrderNumber");
                t.Bool("ShowOnMemberProfile");
                t.Bool("ShowOnMemberRegistration");
                t.ForeignKey("ProfileType").Table("Profiles_ProfileTypes").OnDelete(ForeignKeyConstraint.Cascade);

            });
        }

       /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.ChangeTable("Profiles_ProfileHeaders", t => t.RemoveForeignKey("ProfileType").Table("Profiles_ProfileTypes").Column("ProfileTypeId"));
            Database.RemoveTable("Profiles_ProfileHeaders");
        }
    }
}
