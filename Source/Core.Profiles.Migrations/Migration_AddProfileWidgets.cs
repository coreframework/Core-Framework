using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Profiles.Migrations
{
    /// <summary>
    /// Adds Profiles_ProfileWidgets table.
    /// </summary>
    [Migration(10)]
    public class Migration_AddProfileWidgets : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("Profiles_ProfileWidgets", t =>
            {
                t.PrimaryKey();
                t.Integer("DisplayMode");
            });
        }

       /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.RemoveTable("Profiles_ProfileWidgets");
        }
    }
}
