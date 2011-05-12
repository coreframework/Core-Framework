using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Core.Web.NHibernate.Models.Permissions;
using Core.Web.NHibernate.Models.Static;
using Framework.Facilities.NHibernate;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using NHibernate.Type;

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

        #endregion
    }
}
