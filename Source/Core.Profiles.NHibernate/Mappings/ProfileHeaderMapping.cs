using Core.Profiles.NHibernate.Models;
using FluentNHibernate.Mapping;
using Framework.Facilities.NHibernate.Filters;

namespace Core.Profiles.NHibernate.Mappings
{
    public class ProfileHeaderMapping : ClassMap<ProfileHeader>
    {
        public ProfileHeaderMapping()
        {
            Cache.Region("Profiles_ProfileHeaders").ReadWrite();
            Table("Profiles_ProfileHeaders");
            Id(profileHeader => profileHeader.Id);
            Map(profileHeader => profileHeader.OrderNumber);
            References(profileHeader => profileHeader.ProfileType);

            HasMany(profileHeader => profileHeader.ProfileElements).KeyColumn("ProfileHeaderId")
           .Table("Profiles_ProfilesElements")
           .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.None)
           .Inverse()
           .LazyLoad()
           .Cascade.AllDeleteOrphan();

            HasMany(profileHeader => profileHeader.CurrentLocales).KeyColumn("ProfileHeaderId")
            .Table("ProfileHeaderLocales").ApplyFilter<CultureFilter>()
            .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.None)
            .Inverse()
            .LazyLoad()
            .Cascade.All();
        }
    }
}
