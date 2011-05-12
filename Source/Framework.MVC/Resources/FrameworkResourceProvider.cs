// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrameworkResourceProvider.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Web.Compilation;

using Microsoft.Practices.ServiceLocation;

namespace Framework.MVC.Resources
{
    /// <summary>
    /// Get resources from <see cref="IResourceCache"/>.
    /// </summary>
    public class FrameworkResourceProvider : IResourceProvider
    {
        #region Fields

        private const String ScopeSeparator = ".";

        private readonly String scope;

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

        #region IResourceProvider members

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
                cultureName = Thread.CurrentThread.CurrentUICulture.Name;
            }

            String resource = null;
            var resourceCache = ServiceLocator.Current.GetInstance<IResourceCache>();
            if (resourceCache != null)
            {
                // Try to retieve resource for specific culture (en-US).
                resource = resourceCache.GetResource(GetResourceKey(scope, resourceKey, cultureName));

                // Try to retieve resource for general culture (en).
                if (resource == null && cultureName.Length > 3)
                {
                    resource = resourceCache.GetResource(GetResourceKey(scope, resourceKey, cultureName.Substring(0, 2)));
                }

                // Try to retieve resource for invariant culture (only for production).
                if (resource == null)
                {
                    resource = resourceCache.GetResource(GetResourceKey(scope, resourceKey, null));
                }
            }

            return resource;
        }

        #endregion

        #region Helper members

        private static String GetResourceKey(String scope, String resourceKey, String cultureName)
        {
            var chains = new List<String>();

            if (!String.IsNullOrEmpty(cultureName))
            {
                chains.Add(cultureName);
            }

            if (!String.IsNullOrEmpty(scope))
            {
                chains.Add(scope);
            }

            chains.Add(resourceKey);

            return String.Join(ScopeSeparator, chains);
        }

        #endregion
    }
}