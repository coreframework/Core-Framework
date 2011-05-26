using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Navigation.Migrations
{
    /// <summary>
    /// Adds ListMenuWidgets table.
    /// </summary>
    [Migration(3)]
    public class Migration_AddListMenuWidgets : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("ListMenuWidgets", t =>
            {
                t.PrimaryKey();
                t.Integer("Orientation");
            });
        }

       /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.RemoveTable("ListMenuWidgets");
        }
    }
}
