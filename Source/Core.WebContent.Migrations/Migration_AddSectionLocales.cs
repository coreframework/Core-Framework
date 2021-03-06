﻿using ECM7.Migrator.Framework;
using Framework.Migrator.Extensions;

namespace Core.WebContent.Migrations
{
    /// <summary>
    /// Adds ContentPageLocales table.
    /// </summary>
    [Migration(2)]
    public class Migration_AddSectionLocales : Migration
    {
        /// <summary>
        /// Executes migration.
        /// </summary>
        public override void Apply()
        {
            Database.AddTable("WebContent_SectionLocales", t =>
            {
                t.PrimaryKey();
                t.String("Title").Length(255);
                t.Text("Description").Null();
                t.Text("Culture").Length(10).Null();
                t.ForeignKey("Section").Table("WebContent_Sections").OnDelete(ForeignKeyConstraint.Cascade);
            });
        }

        /// <summary>
        /// Rollbacks migration.
        /// </summary>
        public override void Revert()
        {
            Database.ChangeTable("WebContent_SectionLocales", t => t.RemoveForeignKey("Section").Table("WebContent_Sections"));
            Database.RemoveTable("WebContent_SectionLocales");
        }
    }
}
