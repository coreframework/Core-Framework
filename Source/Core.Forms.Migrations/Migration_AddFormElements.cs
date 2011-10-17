using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.Forms.Migrations
{
    /// <summary>
    /// Adds Forms_FormElements table.
    /// </summary>
    [Migration(3)]
    public class Migration_AddFormElements : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("Forms_FormElements", t =>
            {
                t.PrimaryKey();
                t.Integer("Type");
                t.Bool("IsRequired").Default(0);
                t.Integer("OrderNumber");
                t.Integer("RegexTemplate");
                t.Long("MaxLength").Null();
                t.ForeignKey("FormElementForm").Table("Forms_Forms").Column("FormId").OnDelete(ForeignKeyConstraint.Cascade);
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.ChangeTable("Forms_FormElements", t => t.RemoveForeignKey("FormElementForm").Table("Forms_Forms"));
            Database.RemoveTable("Forms_FormElements");
        }
    }
}
