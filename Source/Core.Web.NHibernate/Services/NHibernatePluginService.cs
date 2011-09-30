using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Framework.Facilities.NHibernate;
using NHibernate;
using NHibernate.Criterion;

namespace Core.Web.NHibernate.Services
{
    public class NHibernatePluginService : NHibernateDataService<Plugin>, IPluginService
    {
        #region Constructors

        public NHibernatePluginService(ISessionManager sessionManager) : base(sessionManager)
        {}

        #endregion

        #region Methods

        public Plugin FindPluginByIdentifier(String pluginIdentifier)
        {
            var query = from plugin in CreateQuery()
                        where plugin.Identifier == pluginIdentifier
                        select plugin;
            return query.FirstOrDefault();
        }

        public IEnumerable<Plugin> FindPluginsByIdentifiers(List<String> pluginIdentifiers)
        {
            return CreateQuery().Select(t => t).Where(t => pluginIdentifiers.Contains(t.Identifier)).ToList();
        }

        public IEnumerable<Plugin> FindPluginsByIds(List<long> pluginIds)
        {
            return CreateQuery().Select(t => t).Where(t => pluginIds.Contains(t.Id)).ToList();
        }

        public int GetCount(IQueryable<Plugin> baseQuery)
        {
            return baseQuery.Count();
        }

        public ICriteria GetSearchCriteria(String searchString)
        {
            var criteria = Session.CreateCriteria<Plugin>();

            if (!String.IsNullOrEmpty(searchString))
            {
                var localesSubQuery = DetachedCriteria.For<PluginLocale>("locale")
               .Add(Restrictions.Like("Title", searchString, MatchMode.Anywhere)).CreateAlias("Plugin", "plugin")
               .SetProjection(Projections.Property("plugin.Id"));

                var filter = DetachedCriteria.For<PluginLocale>("filteredLocale").CreateAlias("Plugin", "filteredPlugin")
                    .SetProjection(Projections.Id()).SetMaxResults(1).AddOrder(Order.Desc("filteredLocale.Priority")).Add(Restrictions.EqProperty("filteredPlugin.Id", "plugin.Id"));

                localesSubQuery.Add(Subqueries.PropertyIn("locale.Id", filter));

                criteria.Add(Subqueries.PropertyIn("Id", localesSubQuery));
            }

            return criteria.SetCacheable(true);
        }

        #endregion
    }
}
