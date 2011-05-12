using Core.Web.NHibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.Web.NHibernate.Mappings
{
    public class LookAndFeelMapping : ClassMap<LookAndFeelSettings>
    {
        public LookAndFeelMapping()
        {
            Cache.Region("LookAndFeelSettings").ReadWrite();
            Table("LookAndFeelSettings");
            Id(lookAndFeelSetting => lookAndFeelSetting.Id);
            Map(lookAndFeelSetting => lookAndFeelSetting.BackgroundColor);
            Map(lookAndFeelSetting => lookAndFeelSetting.FontFamily);
            Map(lookAndFeelSetting => lookAndFeelSetting.FontSizeValue);
            Map(lookAndFeelSetting => lookAndFeelSetting.FontSizeUnit);
            Map(lookAndFeelSetting => lookAndFeelSetting.Color);
            Map(lookAndFeelSetting => lookAndFeelSetting.OtherStyles);            
        }
    }
}
