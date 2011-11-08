using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.WebContent.Migrations
{
    /// <summary>
    /// Adds ContentPages table.
    /// </summary>
    [Migration(3)]
    public class Migration_AddSectionSettings : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Up()
        {
            Database.AddTable("WebContent_SectionSettings", t =>
            {
                t.PrimaryKey();
                t.DateTime("CreateDate").Null();
                t.Bool("ShowTitle");
                t.Bool("TitleLinkable");
                t.Bool("ShowSummaryText");
                t.Bool("ShowSection");
                t.Bool("ShowCategory");
                t.Bool("ShowAuthor");
                t.Bool("ShowCreatedDate");
                t.Bool("ShowModifiedDate");
                t.Bool("ShowPdfIcon");
                t.Bool("ShowPrintIcon");
                t.Bool("ShowEmailIcon");
                t.String("AlternativeReadMoreText").Null();
                t.ForeignKey("Section").Table("WebContent_Sections").OnDelete(ForeignKeyConstraint.Cascade);
            });
        }

       /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Down()
        {
            Database.ChangeTable("WebContent_SectionSettings", t => t.RemoveForeignKey("Section").Table("WebContent_Sections"));
            Database.RemoveTable("WebContent_SectionSettings");
        }
    }
}
