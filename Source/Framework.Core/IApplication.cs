// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IApplication.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using Castle.Windsor;

using Framework.Core.Configuration;

using Environment = Framework.Core.Configuration.Environment;

namespace Framework.Core
{
    /// <summary>
    /// Application interface.
    /// </summary>
    public interface IApplication
    {
        /// <summary>
        /// Gets the application environment.
        /// </summary>
        /// <value>The application environment.</value>
        Environment Environment { get;  }

        /// <summary>
        /// Gets application root path.
        /// </summary>
        /// <value>The application path.</value>
        String RootPath { get;  }

        /// <summary>
        /// Gets the database configuration.
        /// </summary>
        /// <value>The database configuration.</value>
        Dictionary<String, DatabaseConfiguration> DatabaseConfiguration { get; }

        /// <summary>
        /// Configures application instance.
        /// </summary>
        /// <param name="container">The container.</param>
        void Configure(IWindsorContainer container);
    }
}