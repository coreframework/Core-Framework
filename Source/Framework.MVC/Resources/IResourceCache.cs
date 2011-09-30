// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IResourceCache.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Framework.Mvc.Resources
{
    /// <summary>
    /// Specifies interface for resource cache.
    /// </summary>
    public interface IResourceCache : IEnumerable<KeyValuePair<String, String>>
    {
        /// <summary>
        /// Updates resources cache.
        /// </summary>
        void Update();

        /// <summary>
        /// Gets the resource by key.
        /// </summary>
        /// <param name="key">The resource key.</param>
        /// <returns>value associated with <paramref name="key"/> specified or <c>null</c>.</returns>
        String GetResource(String key);
    }
}