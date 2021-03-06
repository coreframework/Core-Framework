﻿using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Forms.Migrations
{
    /// <summary>
    /// Adds Forms_FormsBuilderWidgets table.
    /// </summary>
    [Migration(6)]
    public class Migration_AddFormAnswerValues : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("Forms_FormAnswerValues", t =>
            {
                t.PrimaryKey();
                t.String("Field").Text();
                t.Bool("Value").Text().Null();
                t.ForeignKey("FormAnswer").Table("Forms_FormWidgetAnswers").Column("FormAnswerId").OnDelete(ForeignKeyConstraint.Cascade);
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.ChangeTable("Forms_FormAnswerValues", t => t.RemoveForeignKey("FormAnswer").Table("Forms_FormWidgetAnswers").Column("FormAnswerId"));
            Database.RemoveTable("Forms_FormAnswerValues");
        }
    }
}
