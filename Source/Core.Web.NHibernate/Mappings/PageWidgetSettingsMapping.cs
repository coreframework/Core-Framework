using Core.Web.NHibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.Web.NHibernate.Mappings
{
    public class PageWidgetSettingsMapping : ClassMap<PageWidgetSettings>
    {
        public PageWidgetSettingsMapping()
        {
            Cache.Region("PageWidgetSettings").ReadWrite();
            Table("PageWidgetSettings");
            Id(pageWidgetSetting => pageWidgetSetting.Id);
            Map(pageWidgetSetting => pageWidgetSetting.CustomCSSClasses).Length(1024);
            References(pageWidgetSetting => pageWidgetSetting.LookAndFeelSettings, "LookAndFeelSettingsId").Cascade.All();
            References(pageWidgetSetting => pageWidgetSetting.Widget).Column("WidgetId");
        }
    }
}
