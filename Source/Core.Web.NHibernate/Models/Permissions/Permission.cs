// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Permission.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Core.Framework.Permissions.Models;
using Framework.Core.DomainModel;

namespace Core.Web.NHibernate.Models.Permissions
{
    /// <summary>
    /// Allow or deny some operation for specified user or role.
    /// </summary>
    public class Permission : Entity, IPermissionModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Permission"/> class.
        /// </summary>
        public Permission()
        {
            Role = new Role();
            EntityType = new EntityType();
        }
        
        /// <summary>
        /// Gets or sets the entity this permission applies to (for entity-specific operations).
        /// </summary>
        /// <value>The entity this permission applies to.</value>
        public virtual EntityType EntityType { get; set; }

        /// <summary>
        /// Gets or sets the entity id.
        /// </summary>
        /// <value>The entity id.</value>
        public virtual long? EntityId { get; set; }

        public virtual long? InstanceId { get; set; }

        /// <summary>
        /// Gets or sets the role this permission belongs to.
        /// </summary>
        /// <value>The role this permission applies to.</value>
        public virtual Role Role { get; set; }

        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        /// <value>The permissions.</value>
        public virtual long Permissions { get; set; }

        /// <summary>
        /// Gets or sets the role id.
        /// </summary>
        /// <value>The role id.</value>
        public virtual long RoleId
        {
            get { return Role.Id; }
        }
    }
}