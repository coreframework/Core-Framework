using Core.Web.NHibernate.Models;
using FluentNHibernate.Mapping;
using Framework.Facilities.NHibernate.Filters;

namespace Core.Web.NHibernate.Mappings
{
    public class RoleMapping : ClassMap<Role>
    {
        public RoleMapping()
        {
            Cache.Region("Roles").ReadWrite();
            Table("Roles");
            Id(role => role.Id);
            Map(role => role.IsSystemRole);
            Map(role => role.NotAssignableRole);
            Map(role => role.NotPermissible);
            HasManyToMany(role => role.Users).Table("UsersToRoles").ParentKeyColumn("RoleId")
                .ChildKeyColumn("UserId").Cascade.SaveUpdate().LazyLoad();
            HasManyToMany(role => role.UserGroups).Table("UserGroupsToRoles").ParentKeyColumn("RoleId")
                .ChildKeyColumn("UserGroupId").Cascade.SaveUpdate().LazyLoad();
            HasMany(role => role.CurrentLocales).KeyColumn("RoleId")
            .Table("RoleLocales").ApplyFilter<CultureFilter>()
            .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.None)
            .Inverse()
            .LazyLoad()
            .Cascade.All();
        }
    }
}