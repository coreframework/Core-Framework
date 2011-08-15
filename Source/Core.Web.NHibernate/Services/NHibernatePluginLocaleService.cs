using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Framework.Facilities.NHibernate;
using NHibernate;
using NHibernate.Criterion;

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

        public ICriteria GetSearchCriteria(string searchString)
        {
            ICriteria criteria = Session.CreateCriteria<PluginLocale>().CreateAlias("Plugin", "plugin");

            DetachedCriteria filter = DetachedCriteria.For<PluginLocale>("filteredLocale").CreateAlias("Plugin", "filteredPlugin")
                .SetProjection(Projections.Id()).SetMaxResults(1).AddOrder(Order.Desc("filteredLocale.Priority")).Add(
                    Restrictions.EqProperty("filteredPlugin.Id", "plugin.Id"));

            criteria.Add(Subqueries.PropertyIn("Id", filter));

            if (!String.IsNullOrEmpty(searchString))
            {
                criteria.Add(Restrictions.Or(Restrictions.Like("Title", searchString, MatchMode.Anywhere), Restrictions.Like("Description", searchString, MatchMode.Anywhere)));
            }

            return criteria.SetCacheable(true);
        }
    }
}