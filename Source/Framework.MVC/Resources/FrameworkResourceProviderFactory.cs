// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrameworkResourceProviderFactory.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Web.Compilation;
using Framework.MVC.Helpers;

namespace Framework.MVC.Resources
{
    /// <summary>
    /// Provider factory that instantiates <see cref="FrameworkResourceProvider"/>.
    /// </summary>
    public class FrameworkResourceProviderFactory : ResourceProviderFactory
    {
        #region ResourceProviderFactory members

        /// <summary>
        /// When overridden in a derived class, creates a global resource provider. 
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Web.Compilation.IResourceProvider"/>.
        /// </returns>
        /// <param name="classKey">The name of the resource class.</param>
        public override IResourceProvider CreateGlobalResourceProvider(String classKey)
        {
            return new FrameworkResourceProvider(classKey);
        }

        /// <summary>
        /// When overridden in a derived class, creates a local resource provider. 
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Web.Compilation.IResourceProvider"/>.
        /// </returns>
        /// <param name="virtualPath">The path to a resource file.</param>
        public override IResourceProvider CreateLocalResourceProvider(String virtualPath)
        {
            return new FrameworkResourceProvider(GetScope(virtualPath));
        }

        #endregion

        #region Helper members

        private String GetScope(String virtualPath)
        {
            return ResourceHelper.GetViewScope(virtualPath);
        }

        #endregion
    }
}