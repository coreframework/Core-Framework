using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Web.Migrations
{
    /// <summary>
    /// Adds EntityTypes table.
    /// </summary>
    [Migration(20)]
    public class MigrationAddEntityTypes : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("EntityTypes", t =>
            {
                t.PrimaryKey();
                t.String("Name").Length(500);
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.RemoveTable("EntityTypes");
        }
    }
}
