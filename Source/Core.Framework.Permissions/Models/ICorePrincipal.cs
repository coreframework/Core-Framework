// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICorePrincipal.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Security.Principal;

namespace Core.Framework.Permissions.Models
{
    /// <summary>
    /// Specifies interface for system user.
    /// </summary>
    public interface ICorePrincipal : IPrincipal
    {
        /// <summary>
        /// Gets the principal id.
        /// </summary>
        /// <value>The principal id.</value>
        long PrincipalId { get; }
    }
}