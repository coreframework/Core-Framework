using Core.Web.NHibernate.Models.Permissions;
using FluentNHibernate.Mapping;

namespace Core.Web.NHibernate.Mappings.Permissions
{
    /// <summary>
    /// NHibernate mapping for <see cref="Permission"/> model.
    /// </summary>
    public class EntityTypeMapping : ClassMap<EntityType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionMapping"/> class.
        /// </summary>
        public EntityTypeMapping()
        {
            Cache.Region("EntityTypes").ReadWrite();
            Table("EntityTypes");
            Id(permission => permission.Id);
            Map(permission => permission.Name);
        }
    }
}
