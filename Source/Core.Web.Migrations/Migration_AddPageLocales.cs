using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Web.Migrations
{
    /// <summary>
    /// Adds PageLocales table.
    /// </summary>
    [Migration(201109081123)]
    public class Migration_AddPageLocales : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("PageLocales", t =>
            {
                t.PrimaryKey();
                t.String("Title");
                t.Text("Culture").Length(10).Null();
                t.ForeignKey("Page").Table("Pages").OnDelete(ForeignKeyConstraint.Cascade);
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.ChangeTable("PageLocales", t => t.RemoveForeignKey("Page").Table("Pages"));
            Database.RemoveTable("PageLocales");
        }
    }
}