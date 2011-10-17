using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Forms.Migrations
{
    /// <summary>
    /// Adds Forms_FormLocales table.
    /// </summary>
    [Migration(2)]
    public class Migration_AddFormLocales : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("Forms_FormLocales", t =>
            {
                t.PrimaryKey();
                t.String("Title").Length(255);
                t.String("SubmitButtonText").Length(255).Null();
                t.String("ResetButtonText").Length(255).Null();
                t.Text("Culture").Length(10).Null();
                t.ForeignKey("Form").Table("Forms_Forms").Column("formId").OnDelete(ForeignKeyConstraint.Cascade);
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.ChangeTable("Forms_FormLocales", t => t.RemoveForeignKey("Form").Table("Forms_Forms"));
            Database.RemoveTable("Forms_FormLocales");
        }
    }
}
