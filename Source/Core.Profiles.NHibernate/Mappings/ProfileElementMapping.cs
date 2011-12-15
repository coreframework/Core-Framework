using Core.Profiles.NHibernate.Models;
using FluentNHibernate.Mapping;
using Framework.Facilities.NHibernate.Filters;

namespace Core.Profiles.NHibernate.Mappings
{
    public class ProfileElementMapping : ClassMap<ProfileElement>
    {
        public ProfileElementMapping()
        {
            Cache.Region("Profiles_ProfileElements").ReadWrite();
            Table("Profiles_ProfileElements");
            Id(profileElement => profileElement.Id);
            Map(profileElement => profileElement.Type);
            Map(profileElement => profileElement.OrderNumber);
            Map(profileElement => profileElement.IsRequired);
            Map(profileElement => profileElement.ShowOnMemberProfile);
            Map(profileElement => profileElement.ShowOnMemberPublicProfile);
            Map(profileElement => profileElement.ShowOnMemberRegistration);

            References(profileElement => profileElement.ProfileHeader).Column("ProfileHeaderId");

            HasMany(profileElement => profileElement.CurrentLocales).KeyColumn("ProfileElementId")
             .Table("Profiles_ProfileElementLocales").ApplyFilter<CultureFilter>()
             .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.None)
             .Inverse()
             .LazyLoad()
             .Cascade.All();

            HasMany(profileElement => profileElement.UserProfileElements).KeyColumn("ProfileElementId")
            .Table("Profiles_UserProfileElements")
            .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.None)
            .Inverse()
            .LazyLoad()
            .Cascade.All();
        }
    }
}
