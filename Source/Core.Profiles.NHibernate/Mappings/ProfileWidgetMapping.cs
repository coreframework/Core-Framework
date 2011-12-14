using Core.Profiles.NHibernate.Models;
using Core.Profiles.NHibernate.Static;
using FluentNHibernate.Mapping;

namespace Core.Profiles.NHibernate.Mappings
{
    public class ProfileWidgetMapping : ClassMap<ProfileWidget>
    {
        public ProfileWidgetMapping()
        {
            Cache.Region("Profiles_ProfileWidgets").ReadWrite();
            Table("Profiles_ProfileWidgets");
            Id(widget => widget.Id);
            Map(widget => widget.DisplayMode).CustomType(typeof(ProfileWidgetDisplayMode));
        }
    }
}
