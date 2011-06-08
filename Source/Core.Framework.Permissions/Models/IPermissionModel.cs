namespace Core.Framework.Permissions.Models
{
    public interface IPermissionModel
    {
        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        /// <value>The permissions.</value>
        long Permissions { get; set; }

        /// <summary>
        /// Gets or sets the role id.
        /// </summary>
        /// <value>The role id.</value>
        long RoleId { get;}

        /// <summary>
        /// Gets or sets the entity id.
        /// </summary>
        /// <value>The entity id.</value>
        long? EntityId { get; set; }
    }
}
