// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigurationManagerWrapper.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Specialized;
using System.Configuration;

namespace Framework.Core.Configuration
{
    /// <summary>
    /// Default implementation for <see cref="IConfigurationManager"/>. Uses default .NET configuration system.
    /// </summary>
    public class ConfigurationManagerWrapper : IConfigurationManager
    {
        #region IConfigurationManager members

        /// <summary>
        /// Gets the application settings.
        /// </summary>
        /// <value>The app settings.</value>
        public NameValueCollection AppSettings
        {
            get
            {
                return ConfigurationManager.AppSettings;
            }
        }

        /// <summary>
        /// Gets the configuration section.
        /// </summary>
        /// <typeparam name="T">Section handler type.</typeparam>
        /// <param name="sectionName">Name of the section.</param>
        /// <returns>configuration section.</returns>
        public T GetSection<T>(String sectionName)
        {
            return (T) ConfigurationManager.GetSection(sectionName);
        }

        #endregion
    }
}