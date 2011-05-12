using Castle.Facilities.NHibernateIntegration;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.Web.NHibernate.Services
{
    public class NHibernatePageLayoutRowService : NHibernateDataService<PageLayoutRow>, IPageLayoutRowService
    {
        #region Constructors

        public NHibernatePageLayoutRowService(ISessionManager sessionManager) : base(sessionManager)
        {}

        #endregion
    }
}
