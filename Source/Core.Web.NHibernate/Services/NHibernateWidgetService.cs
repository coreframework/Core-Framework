using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Framework.Permissions.Models;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.Web.NHibernate.Services
{
    public class NHibernateWidgetService : NHibernateDataService<Widget>, IWidgetService
    {
        #region Constructors

        public NHibernateWidgetService(ISessionManager sessionManager) : base(sessionManager)
        {}

        #endregion

        #region Methods

        public IEnumerable<Widget> GetInstalledWidgets()
        {
            var query = from widget in CreateQuery()
                        where widget.Plugin.Status == PluginStatus.Installed
                        select widget;

            return query.ToList();
        }

        public IEnumerable<Widget> GetAvailableWidgets(ICorePrincipal user)
        {
            var query = from widget in CreateQuery()
                        where widget.Plugin.Status == PluginStatus.Installed && widget.Status == WidgetStatus.Enabled
                        select widget;

            return query.ToList();
        }

        public Widget FindWidgetByIdentifier(String widgetIdentifier)
        {
            var query = from widget in CreateQuery()
                        where widget.Identifier == widgetIdentifier
                        select widget;
            return query.FirstOrDefault();
        }

        public bool IsWidgetEnable(Widget widget)
        {
            return widget != null && widget.Plugin != null && widget.Plugin.Status.Equals(PluginStatus.Installed) && widget.Status.Equals(WidgetStatus.Enabled);
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <param name="baseQuery">The base query.</param>
        /// <returns></returns>
        public int GetCount(IQueryable<Widget> baseQuery)
        {
            return baseQuery.Count();
        }

        /// <summary>
        /// Gets the search query.
        /// </summary>
        /// <param name="searchString">The search string.</param>
        /// <returns></returns>
        public IQueryable<Widget> GetSearchQuery(string searchString)
        {
            var baseQuery = CreateQuery();
            if (String.IsNullOrEmpty(searchString))
            {
                return baseQuery;
            }
           
            return baseQuery.Where(widget => widget.CurrentWidgetLocales.Where(item=>item.Title.Contains(searchString)).Count()>0);
        }

        #endregion
    }
}
