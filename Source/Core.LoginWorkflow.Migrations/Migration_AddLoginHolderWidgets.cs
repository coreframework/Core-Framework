using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.LoginWorkflow.Migrations
{
    [Migration(201112201625)]
    public class Migration_AddLoginHolderWidgets : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("LoginWorkflow_LoginHolderWidget", t =>
            {
                t.PrimaryKey();
                t.ForeignKey("LoginHolderWidget_FormLogin").Table("FormLogin_FormLoginWidgets").Column("FormLoginWidgetId").OnDelete(ForeignKeyConstraint.Cascade);
                t.ForeignKey("LoginHolderWidget_OpenIDLogin").Table("OpenIDLogin_OpenIDLoginWidgets").Column("OpenIdLoginWidgetId").NotRequired().OnDelete(ForeignKeyConstraint.SetNull);
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.ChangeTable("LoginWorkflow_LoginHolderWidget", t => t.RemoveForeignKey("LoginHolderWidget_FormLogin").Table("FormLogin_FormLoginWidgets").Column("FormLoginWidgetId"));
            Database.ChangeTable("LoginWorkflow_LoginHolderWidget", t => t.RemoveForeignKey("LoginHolderWidget_OpenIDLogin").Table("OpenIDLogin_OpenIDLoginWidgets").Column("OpenIdLoginWidgetId"));
            Database.RemoveTable("LoginWorkflow_LoginHolderWidget");
        }
    }
}
