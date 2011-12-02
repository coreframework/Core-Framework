using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Profiles.Migrations
{
    /// <summary>
    /// Adds ProfileTypes table.
    /// </summary>
    [Migration(1)]
    public class Migration_AddProfileTypes : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("Profiles_ProfileTypes", t =>
            {
                t.PrimaryKey();
                t.DateTime("CreateDate").Null();
                t.Long("UserId").Null();
            });
        }

       /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.RemoveTable("Profiles_ProfileTypes");
        }
    }
}
