﻿using System;
using System.Collections.Generic;
using System.Linq;
using Core.Web.NHibernate.Models;
using Framework.Core.Services;

namespace Core.Web.NHibernate.Contracts
{
    public interface IPluginService : IDataService<Plugin>
    {
        Plugin FindPluginByIdentifier(String pluginIdentifier);

        IEnumerable<Plugin> FindPluginsByIdentifiers(List<String> pluginIdentifiers);

        IEnumerable<Plugin> FindPluginsByIds(List<long> pluginIds);

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <param name="baseQuery">The base query.</param>
        /// <returns></returns>
        int GetCount(IQueryable<Plugin> baseQuery);

        /// <summary>
        /// Gets the search query.
        /// </summary>
        /// <param name="searchString">The search string.</param>
        /// <returns></returns>
        IQueryable<Plugin> GetSearchQuery(string searchString);
    }
}
