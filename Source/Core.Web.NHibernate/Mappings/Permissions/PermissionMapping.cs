using Core.Web.NHibernate.Models.Permissions;
using FluentNHibernate.Mapping;

namespace Core.Web.NHibernate.Mappings.Permissions
{
    /// <summary>
    /// NHibernate mapping for <see cref="Permission"/> model.
    /// </summary>
    public class PermissionMapping : ClassMap<Permission>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionMapping"/> class.
        /// </summary>
        public PermissionMapping()
        {
            Cache.Region("Permissions").ReadWrite();
            Table("Permissions");
            Id(permission => permission.Id);
            Map(permission => permission.EntityId);
            Map(permission => permission.Permissions);
            References(permission => permission.Role);
            References(permission => permission.EntityType);
        }
    }
}