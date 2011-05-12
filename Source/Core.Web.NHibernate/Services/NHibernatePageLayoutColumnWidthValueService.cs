using Castle.Facilities.NHibernateIntegration;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.Web.NHibernate.Services
{
    public class NHibernatePageLayoutColumnWidthValueService : NHibernateDataService<PageLayoutColumnWidthValue>, IPageLayoutColumnWidthValueService
    {
        #region Constructors

        public NHibernatePageLayoutColumnWidthValueService(ISessionManager sessionManager) : base(sessionManager)
        {}

        #endregion
    }
}
