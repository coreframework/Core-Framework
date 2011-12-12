using Core.Profiles.NHibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.Profiles.NHibernate.Mappings
{
    public class UserProfileElementMapping : ClassMap<UserProfileElement> 
    {
        public UserProfileElementMapping()
        {
            Cache.Region("Profiles_UserProfileElements").ReadWrite();
            Table("Profiles_UserProfileElements");

            Id(item => item.Id);
            Map(item => item.Value);
            References(item => item.UserProfile);
            References(item => item.ProfileElement);
        }
    }
}
