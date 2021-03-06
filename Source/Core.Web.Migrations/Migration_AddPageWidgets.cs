﻿using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Web.Migrations
{
    /// <summary>
    /// Adds PageWidgets table.
    /// </summary>
    [Migration(18)]
    public class Migration_AddPageWidgets : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("PageWidgets", t =>
            {
                t.PrimaryKey();
                t.Long("InstanceId").Null();
                t.Long("ParentWidgetId").Null();
                t.Integer("PageSection");
                t.Integer("ColumnNumber");
                t.Integer("OrderNumber");
                t.ForeignKey("PageWidgetsUser").Table("Users").Column("UserId").NotRequired().OnDelete(ForeignKeyConstraint.SetNull);
                t.ForeignKey("PageWidgetsPage").Table("Pages").Column("PageId").OnDelete(ForeignKeyConstraint.Cascade);
                t.ForeignKey("PageWidgetsWidget").Table("Widgets").Column("WidgetId").NotRequired().OnDelete(ForeignKeyConstraint.SetNull);
                t.ForeignKey("PageWidgetsTemplateWidget").Table("Widgets").Column("TemplateWidgetId").NotRequired().OnDelete(ForeignKeyConstraint.SetNull);
            });
        }

       /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.ChangeTable("PageWidgets", t => t.RemoveForeignKey("PageWidgetsUser").Table("Users"));
            Database.ChangeTable("PageWidgets", t => t.RemoveForeignKey("PageWidgetsPage").Table("Pages"));
            Database.ChangeTable("PageWidgets", t => t.RemoveForeignKey("PageWidgetsWidget").Table("Widgets"));
            Database.ChangeTable("PageWidgets", t => t.RemoveForeignKey("PageWidgetsTemplateWidget").Table("Widgets"));

            Database.RemoveTable("PageWidgets");
        }
    }
}
