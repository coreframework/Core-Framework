using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Web.Migrations
{
    /// <summary>
    /// Adds PageLayoutTemplates table.
    /// </summary>
    [Migration(12)]
    public class Migration_AddPageLayoutTemplates : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("PageLayoutTemplates", t =>
            {
                t.PrimaryKey();
                t.String("LayoutCssClass");
                t.Integer("Priority");
            });
        }

       /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.RemoveTable("PageLayoutTemplates");
        }
    }
}
