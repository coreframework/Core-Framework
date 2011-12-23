using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.OpenIDLogin.Migrations
{
    [Migration(201112201522)]
    public class Migration_AddOpenIDLoginWidgets : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("OpenIDLogin_OpenIDLoginWidgets", t =>
            {
                t.PrimaryKey();
                t.Bool("ShowTitle");
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.RemoveTable("OpenIDLogin_OpenIDLoginWidgets");
        }
    }
}
