using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Navigation.Migrations
{
    /// <summary>
    /// Adds NavigationMenuWidgets table.
    /// </summary>
    [Migration(5)]
    public class Migration_AddNavigationMenuWidgets : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("NavigationMenuWidgets", t =>
            {
                t.PrimaryKey();
                t.Integer("Orientation");
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.RemoveTable("NavigationMenuWidgets");
        }
    }
}
