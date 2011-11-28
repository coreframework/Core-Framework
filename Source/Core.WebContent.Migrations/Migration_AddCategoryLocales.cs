using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.WebContent.Migrations
{
    /// <summary>
    /// Adds ContentPageLocales table.
    /// </summary>
    [Migration(5)]
    public class Migration_AddCategoryLocales : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("WebContent_CategoryLocales", t =>
            {
                t.PrimaryKey();
                t.String("Title").Length(255);
                t.Text("Description").Null();
                t.Text("Culture").Length(10).Null();
                t.ForeignKey("Category").Table("WebContent_Categories").OnDelete(ForeignKeyConstraint.Cascade);
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.ChangeTable("WebContent_CategoryLocales", t => t.RemoveForeignKey("Category").Table("WebContent_Categories"));
            Database.RemoveTable("WebContent_CategoryLocales");
        }
    }
}
