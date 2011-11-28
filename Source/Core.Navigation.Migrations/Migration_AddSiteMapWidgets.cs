using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Navigation.Migrations
{
    /// <summary>
    /// Adds ContentPages table.
    /// </summary>
    [Migration(1)]
    public class Migration_AddSiteMapWidgets : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("SiteMapWidgets", t =>
            {
                t.PrimaryKey();
                t.Integer("Depth").Null();
                t.Bool("IncludeRootInTree").Default(0);

                t.ForeignKey("SiteMapWidgetRootPage").Table("Pages").Column("RootPageId").NotRequired().OnDelete(ForeignKeyConstraint.SetNull);
            });
        }

       /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.ChangeTable("SiteMapWidgets", t => t.RemoveForeignKey("SiteMapWidgetRoorPage").Table("Pages"));

            Database.RemoveTable("SiteMapWidgets");
        }
    }
}
