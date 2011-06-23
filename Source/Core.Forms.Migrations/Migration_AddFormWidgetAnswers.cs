using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Forms.Migrations
{
    /// <summary>
    /// Adds Forms_FormWidgetAnswers table.
    /// </summary>
    [Migration(4)]
    public class Migration_AddFormWidgetAnswers : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("Forms_FormWidgetAnswers", t =>
            {
                t.PrimaryKey();
                t.String("Title").Length(255);
                t.DateTime("CreateDate");
                t.ForeignKey("FormWidget").Table("Forms_FormsBuilderWidgets").Column("FormWidgetId").OnDelete(ForeignKeyConstraint.Cascade);
                t.ForeignKey("AnswerUser").Table("Users").Column("UserId").NotRequired().OnDelete(ForeignKeyConstraint.SetNull);
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.ChangeTable("Forms_FormWidgetAnswers", t => t.RemoveForeignKey("FormWidget").Table("Forms_FormsBuilderWidgets"));
            Database.ChangeTable("Forms_FormWidgetAnswers", t => t.RemoveForeignKey("AnswerUser").Table("Users"));
            Database.RemoveTable("Forms_FormWidgetAnswers");
        }
    }
}
