// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPermissionsBuilder.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Core.Framework.Permissions.Models;
using Core.Web.NHibernate.Models;
using Core.Web.NHibernate.Models.Permissions;

namespace Core.Web.NHibernate.Contracts.Permissions
{
    /// <summary>
    /// Specifies fluent interface for permissions assigning.
    /// </summary>
    public interface IPermissionsBuilder
    {
        /// <summary>
        /// Specifies entity to grant opertaion on.
        /// </summary>
        /// <typeparam name="T">Entity type.</typeparam>
        /// <param name="entityType">The entity to assign permission on.</param>
        /// <returns>Permission builder instance.</returns>
        IPermissionsBuilder On<T>(Type entityType)
            where T : IPermissible;

        /// <summary>
        /// Specifies entity to grant opertaion on.
        /// </summary>
        /// <typeparam name="T">Entity type.</typeparam>
        /// <param name="entityId">The entity id.</param>
        /// <returns>Permission builder instance.</returns>
        IPermissionsBuilder On<T>(long entityId)
            where T : IPermissible;

        /// <summary>
        /// Specifies operation to grant opertaion on.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="entityId">The entity id.</param>
        /// <returns>Permission builder instance.</returns>
        IPermissionsBuilder On(Type entityType, long? entityId);

        /// <summary>
        /// Specifies role to grant permission.
        /// </summary>
        /// <param name="role">The role to grant permission.</param>
        /// <returns>Permission builder instance.</returns>
        IPermissionsBuilder For(Role role);

        /// <summary>
        /// Creates permissions rule without saving.
        /// </summary>
        /// <returns>Created permission rule instance.</returns>
        Permission Build();

        /// <summary>
        /// Saves permission rule.
        /// </summary>
        /// <returns>Save permission rule instance.</returns>
        Permission Save();
    }
}