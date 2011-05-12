// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IApplicationConfigurator.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Castle.MicroKernel;

namespace Framework.Core.Configuration
{
    /// <summary>
    /// Specifies interface for database configurators.
    /// </summary>
    public interface IApplicationConfigurator
    {
        /// <summary>
        /// Fills specified database configuration.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <param name="kernel">The kernel.</param>
        void Configure(IApplication application, IKernel kernel);

        ///<summary>
        /// Filled database configuration.
        ///</summary>
        DatabaseConfiguration DatabaseConfiguration { get; }
    }
}