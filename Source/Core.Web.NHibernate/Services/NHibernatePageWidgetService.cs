using Castle.Facilities.NHibernateIntegration;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.Web.NHibernate.Services
{
    public class NHibernatePageWidgetService : NHibernateDataService<PageWidget>, IPageWidgetService
    {
        #region Constructors

        public NHibernatePageWidgetService(ISessionManager sessionManager) : base(sessionManager)
        {}

        #endregion
    }
}
