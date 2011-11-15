using Core.WebContent.NHibernate.Models;
using Core.WebContent.NHibernate.Static;
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

            Map(setting => setting.ShowTitle).CustomType<SectionSettingsVisibility>();
            Map(setting => setting.TitleLinkable).CustomType<SectionSettingsVisibility>();
            Map(setting => setting.ShowSummaryText).CustomType<SectionSettingsVisibility>();
            Map(setting => setting.ShowContent).CustomType<SectionSettingsVisibility>();
            Map(setting => setting.ShowSection).CustomType<SectionSettingsVisibility>();
            Map(setting => setting.ShowCategory).CustomType<SectionSettingsVisibility>();
            Map(setting => setting.ShowAuthor).CustomType<SectionSettingsVisibility>();
            Map(setting => setting.ShowCreatedDate).CustomType<SectionSettingsVisibility>();
            Map(setting => setting.ShowModifiedDate).CustomType<SectionSettingsVisibility>();
            Map(setting => setting.ShowDownloadLink).CustomType<SectionSettingsVisibility>();
            Map(setting => setting.AlternativeReadMoreText);

            References(setting => setting.Section).Column("SectionId");
        }
    }
}
