using System;
using System.Collections.Generic;
using Core.Web.NHibernate.Models;
using Framework.Core.Services;
using NHibernate;

namespace Core.Web.NHibernate.Contracts
{
    public interface IPluginService : IDataService<Plugin>
    {
        Plugin FindPluginByIdentifier(String pluginIdentifier);

        IEnumerable<Plugin> FindPluginsByIdentifiers(List<String> pluginIdentifiers);

        IEnumerable<Plugin> FindPluginsByIds(List<long> pluginIds);

        /// <summary>
        /// Gets the search criteria.
        /// </summary>
        /// <param name="searchString">The search string.</param>
        /// <returns></returns>
        ICriteria GetSearchCriteria(String searchString);
    }
}
