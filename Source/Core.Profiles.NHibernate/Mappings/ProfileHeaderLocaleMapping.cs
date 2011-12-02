using Core.Profiles.NHibernate.Models;
using FluentNHibernate.Mapping;
using Framework.Facilities.NHibernate.Filters;

namespace Core.Profiles.NHibernate.Mappings
{
    public class ProfileHeaderLocaleMapping: ClassMap<ProfileHeaderLocale>
    {
        public ProfileHeaderLocaleMapping()
        {
            Cache.Region("Profiles_ProfileHeaderLocales").ReadWrite();
            Table("Profiles_ProfileHeaderLocales");
            Id(profileHeaderLocale => profileHeaderLocale.Id);
            Map(profileHeaderLocale => profileHeaderLocale.Title).Length(255);
            Map(profileHeaderLocale => profileHeaderLocale.Culture);
            References(profileHeaderLocale => profileHeaderLocale.ProfileHeader).Column("ProfileHeaderId").LazyLoad().Not.Nullable();
            Map(widgetLocale => widgetLocale.Priority).Formula(CultureFilter.CultureFilterPriorityExpression());
        }
    }
}
