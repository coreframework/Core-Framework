// --------------------------------------------------------------------------------------------------------------------
// <copyright file="YamlResourceCacheHolder.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Mvc.Helpers;
using Application = Core.Framework.MEF.Web.Application;
using Environment = Framework.Core.Configuration.Environment;

namespace Framework.Mvc.Resources
{
    /// <summary>
    /// YAML resources collection.
    /// </summary>
    public class YamlResourceCacheHolder : IResourceCachesHolder
    {
        private const String PathSeparator = "\\";

        private IDictionary<String, YamlResourceCache> caches;

        /// <summary>
        /// Initializes a new instance of the <see cref="YamlResourceCacheHolder"/> class.
        /// </summary>
        /// <param name="environment">The environment.</param>
        /// <param name="defaultResourcesPath">The default resources path.</param>
        public YamlResourceCacheHolder(Environment environment, String defaultResourcesPath)
        {
            caches = new Dictionary<String, YamlResourceCache>();
            caches.Add(String.Empty, new YamlResourceCache(environment, defaultResourcesPath));
            foreach (var plugin in Application.Plugins)
            {
                if (!String.IsNullOrEmpty(plugin.ResourcesDirectory))
                {
                    var chains = (IEnumerable<String>)plugin.PluginLocation.ToLower().Split(PathSeparator.ToCharArray());
                    chains = chains.Reverse();
                    chains = chains.Skip(1);
                    String pluginDirectory = chains.First();
                    chains = chains.Reverse();
                    chains = chains.Concat(new[] { plugin.ResourcesDirectory });
                    caches.Add(pluginDirectory, new YamlResourceCache(environment, String.Join(PathSeparator, chains)));
                }
            }
        }

        /// <summary>
        /// Gets the cache.
        /// </summary>
        /// <param name="scope">The scope.</param>
        /// <returns>
        /// Cache associated with <paramref name="scope"/> specified or <c>null</c>.
        /// </returns>
        public IResourceCache GetCache(String scope)
        {
            String area = scope.Split(YamlResourceCache.ScopeSeparator.ToCharArray()).FirstOrDefault();
            if (String.IsNullOrEmpty(area) || ResourceHelper.MainAreas.Contains(area.ToLower()))
            {
                // return framework built-in cache.
                return caches[String.Empty];
            }
            if (caches.ContainsKey(area.ToLower()))
            {
                return caches[area.ToLower()];    
            }

            // return framework built-in cache.
            return caches[String.Empty];
        }

        /// <summary>
        /// Determines whether the specified cache key contains cache.
        /// </summary>
        /// <param name="cacheKey">The cache key.</param>
        /// <returns>
        /// <c>true</c> if the specified cache key contains cache; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsCache(String cacheKey)
        {
            return caches.ContainsKey(cacheKey.ToLowerInvariant());
        }
    }
}
