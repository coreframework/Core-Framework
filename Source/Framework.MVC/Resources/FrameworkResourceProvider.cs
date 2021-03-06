// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrameworkResourceProvider.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web.Compilation;
using Framework.Core.Localization;
using Microsoft.Practices.ServiceLocation;

namespace Framework.Mvc.Resources
{
    /// <summary>
    /// Get resources from <see cref="IResourceCache"/>.
    /// </summary>
    public class FrameworkResourceProvider : IResourceProvider
    {
        #region Fields

        private const String ScopeSeparator = ".";

        private readonly String scope;

        private IResourceCachesHolder resourceCachesHolder;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameworkResourceProvider"/> class.
        /// </summary>
        /// <param name="scope">The resources scope.</param>
        public FrameworkResourceProvider(String scope)
        {
            this.scope = scope;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets an object to read resource values from a source.
        /// </summary>
        /// <value>Resource reader.</value>
        /// <returns>
        /// The <see cref="T:System.Resources.IResourceReader"/> associated with the current resource provider.
        /// </returns>
        public IResourceReader ResourceReader
        {
            get
            {
                return new FrameworkResourceReader(scope);
            }
        }

        private IResourceCachesHolder ResourceCachesHolder
        {
            get
            {
                if (resourceCachesHolder == null)
                {
                    resourceCachesHolder = ServiceLocator.Current.GetInstance<IResourceCachesHolder>();
                }
                return resourceCachesHolder;
            }
        }

        #endregion

        #region IResourceProvider members

        /// <summary>
        /// Returns a resource object for the key and culture.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Object"/> that contains the resource value for the <paramref name="resourceKey"/> and <paramref name="culture"/>.
        /// </returns>
        /// <param name="resourceKey">The key identifying a particular resource.</param><param name="culture">The culture identifying a localized value for the resource.</param>
        public Object GetObject(String resourceKey, CultureInfo culture)
        {
            String cultureName;
            if (culture != null)
            {
                cultureName = culture.Name;
            }
            else
            {
                cultureName = CultureHelper.DefaultCultureName;
            }

            String resource = null;
            var resourceCacheHolder = ServiceLocator.Current.GetInstance<IResourceCachesHolder>();
            if (resourceCacheHolder != null)
            {
                var resourceCache = resourceCacheHolder.GetCache(scope);
                if (resourceCache != null)
                {
                    // Try to retieve resource for specific culture (en-US).
                    resource = resourceCache.GetResource(GetResourceKey(scope, resourceKey, cultureName));

                    // Try to retieve resource for general culture (en).
                    if (resource == null && cultureName.Length > 3)
                    {
                        resource =
                            resourceCache.GetResource(GetResourceKey(scope, resourceKey, cultureName.Substring(0, 2)));
                    }

                    // Try to retieve resource for invariant culture (only for production).
                    if (resource == null)
                    {
                        resource = resourceCache.GetResource(GetResourceKey(scope, resourceKey, null));
                    }
                }
            }

            return resource;
        }

        #endregion

        #region Helper members

        private String GetResourceKey(String scope, String resourceKey, String cultureName)
        {
            var chains = new List<String>();
            if (!String.IsNullOrEmpty(cultureName))
            {
                chains.Add(cultureName);
            }
            if (!String.IsNullOrEmpty(scope))
            {
                IEnumerable<String> scopeChains = scope.Split(YamlResourceCache.ScopeSeparator.ToCharArray());
                String area = scopeChains.FirstOrDefault();
                if (area != null && ResourceCachesHolder.ContainsCache(area))
                {
                    if (scopeChains.Count() > 1)
                    {
                        chains.Add(String.Join(YamlResourceCache.ScopeSeparator, scopeChains.Skip(1)));
                    }
                }
                else
                {
                    chains.Add(scope);
                }
            }
            chains.Add(resourceKey);

            return String.Join(ScopeSeparator, chains);
        }

        #endregion
    }
}