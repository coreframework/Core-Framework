using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Languages.Migrations
{
    /// <summary>
    /// Adds Forms_Forms table.
    /// </summary>
    [Migration(1)]
    public class Migration_AddLanguages : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("Languages_Languages", t =>
            {
                t.PrimaryKey();
                t.String("Title").Length(255);
                t.String("Code").Length(3);
                t.String("Culture").Length(10);
                t.Bool("IsDefault").Bool();
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.RemoveTable("Languages_Languages");
        }
    }
}
