using Castle.Facilities.NHibernateIntegration;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.Web.NHibernate.Services
{
    public class NHibernatePageWidgetSettingService : NHibernateDataService<PageWidgetSettings>, IPageWidgetSettingService
    {
        #region Constructors

        public NHibernatePageWidgetSettingService(ISessionManager sessionManager) : base(sessionManager)
        {}

        #endregion
    }
}
