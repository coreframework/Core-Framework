using Core.Framework.Permissions.Models;
using Core.Web.NHibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.Web.NHibernate.Mappings
{
    public class UserMapping : ClassMap<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserMapping"/> class.
        /// </summary>
        public UserMapping()
        {
            Cache.Region("Users").ReadWrite();
            Table("Users");
            Id(user => user.Id);
            Map(user => user.Email).Length(255);
            Map(user => user.Username).Length(255);
            Map(user => user.Hash).Length(255);
            Map(user => user.Salt).Length(255);
            Map(user => user.EncryptionMode).CustomType(typeof(PasswordMode));
            Map(user => user.Status).CustomType(typeof(UserStatus));
            HasManyToMany(user => user.UserGroups)
                .Table("UserGroupsMembers").ParentKeyColumn("UserId")
                .ChildKeyColumn("UserGroupId").Cascade.SaveUpdate().LazyLoad();

            HasManyToMany(user => user.Roles)
               .Table("UsersToRoles").ParentKeyColumn("UserId")
               .ChildKeyColumn("RoleId").Cascade.SaveUpdate().LazyLoad();
        }
    }
}