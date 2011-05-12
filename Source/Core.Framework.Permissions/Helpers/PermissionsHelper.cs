using System;

namespace Core.Framework.Permissions.Helpers
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

        #endregion
    }
}