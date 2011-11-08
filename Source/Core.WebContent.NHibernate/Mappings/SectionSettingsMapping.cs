using Core.WebContent.NHibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.WebContent.NHibernate.Mappings
{
    public class SectionSettingsMapping : ClassMap<SectionSettings>
    {
        public SectionSettingsMapping()
        {
            Cache.Region("WebContent_SectionSettings").ReadWrite();
            Table("WebContent_SectionSettings");
            Id(setting => setting.Id);
            Map(setting => setting.ShowTitle);
            Map(setting => setting.TitleLinkable);
            Map(setting => setting.ShowSummaryText);
            Map(setting => setting.ShowSection);
            Map(setting => setting.ShowCategory);
            Map(setting => setting.ShowAuthor);
            Map(setting => setting.ShowCreatedDate);
            Map(setting => setting.ShowModifiedDate);
            Map(setting => setting.ShowPdfIcon);
            Map(setting => setting.ShowPrintIcon);
            Map(setting => setting.ShowEmailIcon);
            Map(setting => setting.AlternativeReadMoreText);
            References(setting => setting.Section).Column("SectionId");
        }
    }
}
