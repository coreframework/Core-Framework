using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Forms.Migrations
{
    /// <summary>
    /// Adds Forms_FormsBuilderWidgets table.
    /// </summary>
    [Migration(3)]
    public class Migration_AddFormsBuilderWidgets : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("Forms_FormsBuilderWidgets", t =>
            {
                t.PrimaryKey();
                t.String("Title").Length(255);
                t.Bool("SaveData");
                t.Bool("SendEmail");
                t.String("SenderEmail").Length(255).Null();
                t.ForeignKey("FormsBuilder").Table("Forms_Forms").Column("FormId").OnDelete(ForeignKeyConstraint.Cascade);
                t.ForeignKey("FormsBuilderWidgetUser").Table("Users").Column("UserId").NotRequired().OnDelete(ForeignKeyConstraint.SetNull);
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.ChangeTable("Forms_FormsBuilderWidgets", t => t.RemoveForeignKey("FormsBuilder").Table("Forms_Forms"));
            Database.ChangeTable("Forms_FormsBuilderWidgets", t => t.RemoveForeignKey("FormsBuilderWidgetUser").Table("Users"));
            Database.RemoveTable("Forms_FormsBuilderWidgets");
        }
    }
}
