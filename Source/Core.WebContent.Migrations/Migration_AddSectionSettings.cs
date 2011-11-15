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
                t.Integer("ShowTitle");
                t.Integer("TitleLinkable");
                t.Integer("ShowSummaryText");
                t.Integer("ShowContent");
                t.Integer("ShowSection");
                t.Integer("ShowCategory");
                t.Integer("ShowAuthor");
                t.Integer("ShowCreatedDate");
                t.Integer("ShowModifiedDate");
                t.Integer("ShowDownloadLink");
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
