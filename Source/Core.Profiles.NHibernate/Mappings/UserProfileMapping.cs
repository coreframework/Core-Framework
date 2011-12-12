using Core.Profiles.NHibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.Profiles.NHibernate.Mappings
{
    public class UserProfileMapping : ClassMap<UserProfile> 
    {
        public UserProfileMapping()
        {
            Cache.Region("Profiles_UserProfiles").ReadWrite();
            Table("Profiles_UserProfiles");

            Id(item => item.Id);
            References(item => item.ProfileType);
            References(item => item.User);

            HasMany(item => item.ProfileElements).KeyColumn("UserProfileId")
             .Table("Profiles_UserProfileElements")
             .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.None)
             .Inverse()
             .LazyLoad()
             .Cascade.AllDeleteOrphan();
        }
    }
}
