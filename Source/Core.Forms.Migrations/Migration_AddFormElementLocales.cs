using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Forms.Migrations
{
    /// <summary>
    /// Adds Forms_FormLocales table.
    /// </summary>
    [Migration(7)]
    public class Migration_AddFormElementLocales : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("Forms_FormElementLocales", t =>
            {
                t.PrimaryKey();
                t.String("Title").Length(255);
                t.Text("ElementValues").Null();
                t.Text("Culture").Length(10).Null();
                t.ForeignKey("FormElement").Table("Forms_FormElements").Column("FormElementId").OnDelete(ForeignKeyConstraint.Cascade);
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.ChangeTable("Forms_FormElementLocales", t => t.RemoveForeignKey("FormElement").Table("Forms_FormElements").Column("FormElementId"));
            Database.RemoveTable("Forms_FormElementLocales");
        }
    }
}
