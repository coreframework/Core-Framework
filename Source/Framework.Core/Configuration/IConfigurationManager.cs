// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IConfigurationManager.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Specialized;

namespace Framework.Core.Configuration
{
    /// <summary>
    /// Specifies interface for application configuration manager.
    /// </summary>
    public interface IConfigurationManager
    {
        /// <summary>
        /// Gets the application settings.
        /// </summary>
        /// <value>The app settings.</value>
        NameValueCollection AppSettings { get; }

        /// <summary>
        /// Gets the configuration section.
        /// </summary>
        /// <typeparam name="T">Section handler type.</typeparam>
        /// <param name="sectionName">Name of the section.</param>
        /// <returns>configuration section.</returns>
        T GetSection<T>(String sectionName);
    }
}