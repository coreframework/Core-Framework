using Core.Profiles.NHibernate.Models;
using FluentNHibernate.Mapping;
using Framework.Facilities.NHibernate.Filters;

namespace Core.Profiles.NHibernate.Mappings
{
    public class ProfileTypeMapping : ClassMap<ProfileType>
    {
        public ProfileTypeMapping()
        {
            Cache.Region("WebContent_ProfileTypes").ReadWrite();
            Table("WebContent_ProfileTypes");
            Id(profileType => profileType.Id);
            Map(profileType => profileType.UserId);
            Map(profileType => profileType.CreateDate);

            HasMany(profileType => profileType.CurrentLocales).KeyColumn("ProfileTypeId")
            .Table("ProfileTypeLocales").ApplyFilter<CultureFilter>()
            .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.None)
            .Inverse()
            .LazyLoad()
            .Cascade.All();
        }
    }
}
