// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PermissionsHelper.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Framework.Core;

namespace Core.Web.NHibernate.Helpers.Permissions
{
    /// <summary>
    /// Provide permissions module helper methods.
    /// </summary>
    public static class PermissionsHelper
    {
        #region Methods

        /// <summary>
        /// Gets the entity type name.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <returns>The entity type name.</returns>
        public static String GetEntityType<T>()
        {
            return GetEntityType(typeof(T));
        }

        /// <summary>
        /// Gets the entity type name.
        /// </summary>
        /// <param name="entityType">Type of the entity.</param>
        /// <returns>The entity type name.</returns>
        public static String GetEntityType(Type entityType)
        {
            return String.Format("{0}.{1}", entityType.Namespace, entityType.Name);
        }

        /// <summary>
        /// Generates operation name for system action using naming convention module/action.
        /// </summary>
        /// <param name="module">The module name.</param>
        /// <param name="action">The action name.</param>
        /// <returns>The operation name.</returns>
        public static String GetOperationName(String module, String action)
        {
            Check.IsNotEmpty(module, "module");
            Check.IsNotEmpty(action, "action");

            return String.Format("{0}/{1}", module, action);
        }

        /// <summary>
        /// Generates operation name for entity-specific action using naming convention module/entity/action.
        /// </summary>
        /// <param name="module">The module name.</param>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="action">The action name.</param>
        /// <returns>The operation name.</returns>
        public static String GetOperationName(String module, Type entityType, String action)
        {
            Check.IsNotEmpty(module, "module");
            Check.IsNotNull(entityType, "entityType");
            Check.IsNotEmpty(action, "action");

            return String.Format("{0}/{1}/{2}", module, entityType.Name, action);
        }

        /// <summary>
        /// Generates operation name for entity-specific action using naming convention module/entity/action.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="module">The module name.</param>
        /// <param name="action">The action name.</param>
        /// <returns>The operation name.</returns>
        public static String GetOperationName<T>(String module, String action)
        {
            return GetOperationName(module, typeof(T), action);
        }

        #endregion
    }
}