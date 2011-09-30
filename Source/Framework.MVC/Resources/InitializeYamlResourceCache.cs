// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InitializeYamlResourceCache.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Framework.Core;
using Framework.Core.Configuration;

namespace Framework.Mvc.Resources
{
    /// <summary>
    /// Initializes resources cahce and injects it to IoC.
    /// </summary>
    public class InitializeYamlResourceCache : IBootstrapperTask
    {
        private const String ResourcesDirectoryKey = "resourcesDirectory";

        private const String DefaultResourcesDirectory = "Resources";

        #region IBootstrapperTask members

        /// <summary>
        /// Executes task.
        /// </summary>
        /// <param name="application">The application.</param><param name="kernel">The kernel.</param>
        public void Execute(IApplication application, IKernel kernel)
        {
            var configurationManager = kernel.Resolve<IConfigurationManager>();
            var resourcesDirectory = configurationManager.AppSettings[ResourcesDirectoryKey];
            if (String.IsNullOrEmpty(resourcesDirectory))
            {
                resourcesDirectory = DefaultResourcesDirectory;
            }
            var resourceCacheHolder = new YamlResourceCacheHolder(application.Environment, Path.Combine(application.RootPath, resourcesDirectory));
            kernel.Register(Component.For<IResourceCachesHolder>().Instance(resourceCacheHolder).LifeStyle.Singleton);
        }

        #endregion
    }
}