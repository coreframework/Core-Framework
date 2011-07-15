using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.Web.NHibernate.Services
{
    public class NHibernatePluginLocaleService : NHibernateDataService<PluginLocale>, IPluginLocaleService
    {
        public NHibernatePluginLocaleService(ISessionManager sessionManager) : base(sessionManager)
        {

        }

        public PluginLocale GetLocale(long pluginId, String culture)
        {
            IQueryable<PluginLocale> query = CreateQuery();
            return query.Where(locale => locale.Plugin.Id == pluginId && locale.Culture == culture).FirstOrDefault();
        }
    }
}