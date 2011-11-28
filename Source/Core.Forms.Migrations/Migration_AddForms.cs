using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Forms.Migrations
{
    /// <summary>
    /// Adds Forms_Forms table.
    /// </summary>
    [Migration(1)]
    public class Migration_AddForms : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("Forms_Forms", t =>
            {
                t.PrimaryKey();
                t.Bool("ShowSubmitButton");
                t.Bool("ShowResetButton");
                t.ForeignKey("FormUser").Table("Users").Column("UserId").NotRequired().OnDelete(ForeignKeyConstraint.SetNull);
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.ChangeTable("Forms_Forms", t => t.RemoveForeignKey("FormUser").Table("Users"));
            Database.RemoveTable("Forms_Forms");
        }
    }
}
