using Core.Profiles.NHibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.Profiles.NHibernate.Mappings
{
    public class RegistrationWidgetMapping : ClassMap<RegistrationWidget>
    {
        public RegistrationWidgetMapping()
        {
            Cache.Region("Profiles_RegistrationWidgets").ReadWrite();
            Table("Profiles_RegistrationWidgets");
            Id(widget => widget.Id);
            References(widget => widget.ProfileType).Column("ProfileTypeId");
        }
    }
}
