using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Web.Migrations
{
    /// <summary>
    /// Adds PluginLocales table.
    /// </summary>
    [Migration(201109081211)]
    public class Migration_AddRoleLocales : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("RoleLocales", t =>
            {
                t.PrimaryKey();
                t.String("Name");
                t.Text("Culture").Length(10).Null();
                t.ForeignKey("Role").Table("Roles").OnDelete(ForeignKeyConstraint.Cascade);
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.ChangeTable("RoleLocales", t => t.RemoveForeignKey("Role").Table("Roles"));
            Database.RemoveTable("RoleLocales");
        }
    }
}