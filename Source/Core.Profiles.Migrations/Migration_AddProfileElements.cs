using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Profiles.Migrations
{
    /// <summary>
    /// Adds Profiles_ProfileElements table.
    /// </summary>
    [Migration(5)]
    public class Migration_AddProfileElements : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("Profiles_ProfileElements", t =>
            {
                t.PrimaryKey();
                t.Integer("Type");
                t.Bool("IsRequired").Default(0);
                t.Integer("OrderNumber");
                t.Long("MaxLength").Null();
                t.ForeignKey("ProfileHeader").Table("Profiles_ProfileHeaders").Column("ProfileHeaderId").OnDelete(ForeignKeyConstraint.Cascade);
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.ChangeTable("Profiles_ProfileElements", t => t.RemoveForeignKey("ProfileElementHeader").Table("Profiles_ProfileHeaders").Column("ProfileHeaderId"));
            Database.RemoveTable("Profiles_ProfileElements");
        }
    }
}
