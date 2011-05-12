// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPermissionsQuery.cs" company="Itransition">
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
    /// Specifies fluent interface for permissions querying.
    /// </summary>
    public interface IPermissionsQuery
    {
        /// <summary>
        /// Specifies resource for permissions query.
        /// </summary>
        /// <typeparam name="T">Resource type.</typeparam>
        /// <param name="resource">The resource to assign permission on.</param>
        /// <returns>Permission builder instance.</returns>
        IPermissionsQuery On<T>(T resource)
            where T : IPermissible;

        /// <summary>
        /// Specifies resource for permissions query.
        /// </summary>
        /// <typeparam name="T">Resource type.</typeparam>
        /// <param name="resourceId">The resource id.</param>
        /// <returns>Permission builder instance.</returns>
        IPermissionsQuery On<T>(long resourceId)
            where T : IPermissible;

        /// <summary>
        /// Specifies resource for permissions query.
        /// </summary>
        /// <param name="resourceType">Type of the resource.</param>
        /// <param name="resourceId">The resource id.</param>
        /// <returns>Permission builder instance.</returns>
        IPermissionsQuery On(Type resourceType, long resourceId);

        /// <summary>
        /// Specifies user for permissions query.
        /// </summary>
        /// <param name="forUser">The user for permissions query.</param>
        /// <returns>Permission builder instance.</returns>
        IPermissionsQuery For(ICorePrincipal forUser);

        /// <summary>
        /// Specifies role for permissions query.
        /// </summary>
        /// <param name="forRole">The role for permissions query.</param>
        /// <returns>Permission builder instance.</returns>
        IPermissionsQuery For(Role forRole);

        /// <summary>
        /// Determines whether operation is allowed for specified query.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the specified operation is allowed; otherwise, <c>false</c>.
        /// </returns>
        bool Check();
    }
}