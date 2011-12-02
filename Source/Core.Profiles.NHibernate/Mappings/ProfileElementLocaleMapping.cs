using Core.Profiles.NHibernate.Models;
using FluentNHibernate.Mapping;
using Framework.Facilities.NHibernate.Filters;

namespace Core.Profiles.NHibernate.Mappings
{
    public class ProfileElementLocaleMapping: ClassMap<ProfileElementLocale>
    {
        public ProfileElementLocaleMapping()
        {
            Cache.Region("Profiles_ProfileTypeLocales").ReadWrite();
            Table("Profiles_ProfileTypeLocales");
            Id(article => article.Id);
            Map(profileTypeLocale => profileTypeLocale.Title).Length(255);
            Map(profileTypeLocale => profileTypeLocale.Culture);
            References(profileTypeLocale => profileTypeLocale.ProfileElement).Column("ProfileTypeId").LazyLoad().Not.Nullable();
            Map(widgetLocale => widgetLocale.Priority).Formula(CultureFilter.CultureFilterPriorityExpression());
        }
    }
}
