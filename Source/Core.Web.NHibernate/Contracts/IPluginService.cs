using System;
using System.Collections.Generic;
using Core.Web.NHibernate.Models;
using Framework.Core.Services;

namespace Core.Web.NHibernate.Contracts
{
    public interface IPluginService : IDataService<Plugin>
    {
        Plugin FindPluginByIdentifier(String pluginIdentifier);

        IEnumerable<Plugin> FindPluginsByIdentifiers(List<String> pluginIdentifiers);

        IEnumerable<Plugin> FindPluginsByIds(List<long> pluginIds);
    }
}
