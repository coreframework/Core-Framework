using System;
using Core.Web.NHibernate.Models;
using Framework.Core.Services;

namespace Core.Web.NHibernate.Contracts
{
    public interface IPluginLocaleService : IDataService<PluginLocale>
    {
        PluginLocale GetLocale(long pluginId, String culture);
    }
}