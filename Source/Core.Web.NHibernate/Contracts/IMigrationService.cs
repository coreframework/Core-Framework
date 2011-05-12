using System;
using System.Collections.Generic;
using Core.Web.NHibernate.Models;
using Framework.Core.Services;

namespace Core.Web.NHibernate.Contracts
{
    public interface IMigrationService : IDataService<Migration>
    {
        /// <summary>
        /// Finds the plugin migartions.
        /// </summary>
        /// <param name="pluginIdentifier">The plugin identifier.</param>
        /// <returns></returns>
        IEnumerable<Migration> FindPluginMigartions(String pluginIdentifier);
    }
}
