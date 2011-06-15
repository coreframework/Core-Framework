// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IResourceCachesHolder.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Framework.MVC.Resources
{
    /// <summary>
    /// Specifies interface for resource caches holder.
    /// </summary>
    public interface IResourceCachesHolder
    {
        /// <summary>
        /// Gets the cache.
        /// </summary>
        /// <param name="scope">The scope.</param>
        /// <returns>Cache associated with <paramref name="scope"/> specified or <c>null</c>.</returns>
        IResourceCache GetCache(String scope);

        /// <summary>
        /// Determines whether the specified cache key contains cache.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        /// <returns>
        /// <c>true</c> if the specified cache key contains cache; otherwise, <c>false</c>.
        /// </returns>
        bool ContainsCache(string cacheKey);
    }
}
