using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using Core.Web.NHibernate.Contracts.Widgets;
using Core.Web.NHibernate.Models.Widgets;
using Framework.Facilities.NHibernate;

namespace Core.Web.NHibernate.Services.Widgets
{
    public class NHibernateNavigationMenuWidgetService : NHibernateDataService<NavigationMenuWidget>, INavigationMenuWidgetService
    {
        
        #region Constructors

        public NHibernateNavigationMenuWidgetService(ISessionManager sessionManager): base(sessionManager)
        {
        }

        #endregion
    }
}
