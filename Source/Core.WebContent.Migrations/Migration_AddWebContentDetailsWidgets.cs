using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.WebContent.Migrations
{
    /// <summary>
    /// Adds WebContent_DetailsWidgets table.
    /// </summary>
    [Migration(11)]
    public class Migration_AddWebContentDetailsWidgets : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("WebContent_DetailsWidgets", t =>
            {
                t.PrimaryKey();
                t.Integer("LinkMode");
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.RemoveTable("WebContent_DetailsWidgets");
        }
    }
}
