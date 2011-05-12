using Castle.Facilities.NHibernateIntegration;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.Web.NHibernate.Services
{
    public class NHibernatePageSettingsService : NHibernateDataService<PageSettings>, IPageSettingService
    {
        #region Constructors

        public NHibernatePageSettingsService(ISessionManager sessionManager) : base(sessionManager)
        {}

        #endregion
    }
}
