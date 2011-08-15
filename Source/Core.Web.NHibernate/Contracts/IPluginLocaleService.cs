using System;
using Core.Web.NHibernate.Models;
using Framework.Core.Services;
using NHibernate;

namespace Core.Web.NHibernate.Contracts
{
    public interface IPluginLocaleService : IDataService<PluginLocale>
    {
        PluginLocale GetLocale(long pluginId, String culture);

        ICriteria GetSearchCriteria(string searchString);
    }
}