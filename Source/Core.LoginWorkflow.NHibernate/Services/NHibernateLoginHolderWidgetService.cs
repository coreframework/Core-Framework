using Castle.Facilities.NHibernateIntegration;
using Core.LoginWorkflow.NHibernate.Contracts;
using Core.LoginWorkflow.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.LoginWorkflow.NHibernate.Services
{
    public class NHibernateLoginHolderWidgetService : NHibernateDataService<LoginHolderWidget>, ILoginHolderWidgetService
    {
        #region Constructors

        public NHibernateLoginHolderWidgetService(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #endregion

    }
}
