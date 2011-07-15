using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Framework.Facilities.NHibernate;

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

        /// <summary>
        /// Gets the search query.
        /// </summary>
        /// <param name="searchString">The search string.</param>
        /// <returns></returns>
        public IQueryable<Plugin> GetSearchQuery(string searchString)
        {
            var baseQuery = CreateQuery();
//            if (String.IsNullOrEmpty(searchString))
//            {
                return baseQuery;
//            }
//            return baseQuery.Where(user => user.Title.Contains(searchString));
        }

        #endregion

    }
}
