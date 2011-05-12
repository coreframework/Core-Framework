using Castle.Facilities.NHibernateIntegration;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.Web.NHibernate.Services
{
    public class NHibernatePageLayoutService : NHibernateDataService<PageLayout>, IPageLayoutService
    {
        #region Constructors

        public NHibernatePageLayoutService(ISessionManager sessionManager) : base(sessionManager)
        {}

        #endregion
    }
}
