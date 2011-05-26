using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Web.Migrations
{
    /// <summary>
    /// Adds Roles table.
    /// </summary>
    [Migration(21)]
    public class MigrationAddPermissions : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("Permissions", t =>
            {
                t.PrimaryKey();
                t.Long("EntityId").Null();
                t.Long("Permissions");
                t.ForeignKey("FK_Permissions_EntityType").Table("EntityTypes").Column("EntityTypeId").OnDelete(ForeignKeyConstraint.Cascade);
                t.ForeignKey("FK_Permissions_Users").Table("Roles").Column("RoleId").OnDelete(ForeignKeyConstraint.Cascade);
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.ChangeTable("Permissions", t => t.RemoveForeignKey("FK_Permissions_EntityType").Table("EntityTypes"));
            Database.ChangeTable("Permissions", t => t.RemoveForeignKey("FK_Permissions_Users").Table("Roles"));

            Database.RemoveTable("Permissions");
        }
    }
}
