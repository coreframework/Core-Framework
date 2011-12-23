using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.FormLogin.Migrations
{
    [Migration(201112201304)]
    public class Migration_AddFormLoginWidgets : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("FormLogin_FormLoginWidgets", t =>
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
            Database.RemoveTable("FormLogin_FormLoginWidgets");
        }
    }
}
