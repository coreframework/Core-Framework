using Core.Profiles.NHibernate.Models;
using FluentNHibernate.Mapping;
using Framework.Facilities.NHibernate.Filters;

namespace Core.Profiles.NHibernate.Mappings
{
    public class ProfileElementLocaleMapping: ClassMap<ProfileElementLocale>
    {
        public ProfileElementLocaleMapping()
        {
            Cache.Region("Profiles_ProfileElementLocales").ReadWrite();
            Table("Profiles_ProfileElementLocales");
            Id(profileElementLocale => profileElementLocale.Id);
            Map(profileElementLocale => profileElementLocale.Title).Length(255);
            Map(profileElementLocale => profileElementLocale.Culture);
            References(profileElementLocale => profileElementLocale.ProfileElement).Column("ProfileElementId").LazyLoad().Not.Nullable();
            Map(profileElementLocale => profileElementLocale.Priority).Formula(CultureFilter.CultureFilterPriorityExpression());
        }
    }
}
