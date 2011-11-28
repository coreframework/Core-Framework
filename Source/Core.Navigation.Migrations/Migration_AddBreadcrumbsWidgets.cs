using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Navigation.Migrations
{
    /// <summary>
    /// Adds BreadcrumbsWidgets table.
    /// </summary>
    [Migration(2)]
    public class Migration_AddBreadcrumbsWidgets : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("BreadcrumbsWidgets", t =>
            {
                t.PrimaryKey();
                t.Bool("ShowHomePage").Default(0);
            });
        }

       /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.RemoveTable("BreadcrumbsWidgets");
        }
    }
}
