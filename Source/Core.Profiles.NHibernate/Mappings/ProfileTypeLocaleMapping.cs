using Core.Profiles.NHibernate.Models;
using FluentNHibernate.Mapping;
using Framework.Facilities.NHibernate.Filters;

namespace Core.Profiles.NHibernate.Mappings
{
    public class ProfileTypeLocaleMapping: ClassMap<ProfileTypeLocale>
    {
        public ProfileTypeLocaleMapping()
        {
            Cache.Region("Profiles_ProfileTypeLocales").ReadWrite();
            Table("Profiles_ProfileTypeLocales");
            Id(article => article.Id);
            Map(profileTypeLocale => profileTypeLocale.Title).Length(255);
            Map(profileTypeLocale => profileTypeLocale.Culture);
            References(profileTypeLocale => profileTypeLocale.ProfileType).Column("ProfileTypeId").LazyLoad().Not.Nullable();
            Map(widgetLocale => widgetLocale.Priority).Formula(CultureFilter.CultureFilterPriorityExpression());
        }
    }
}
