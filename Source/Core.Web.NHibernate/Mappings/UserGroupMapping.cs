using Core.Web.NHibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.Web.NHibernate.Mappings
{
    public class UserGroupMapping : ClassMap<UserGroup>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserGroupMapping"/> class.
        /// </summary>
        public UserGroupMapping()
        {
            Cache.Region("UserGroups").ReadWrite();
            Table("UserGroups");
            Id(userGroups => userGroups.Id);
            Map(userGroups => userGroups.Name).Length(255);
            Map(userGroups => userGroups.Description).Length(4096);
            HasManyToMany(userGroups => userGroups.Users)
                .Table("UserGroupsMembers").ParentKeyColumn("UserGroupId")
                .ChildKeyColumn("UserId").Cascade.SaveUpdate();
        }
    }
}
