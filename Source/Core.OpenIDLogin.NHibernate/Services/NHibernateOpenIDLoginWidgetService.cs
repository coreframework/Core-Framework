using Castle.Facilities.NHibernateIntegration;
using Core.OpenIDLogin.NHibernate.Contracts;
using Core.OpenIDLogin.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.OpenIDLogin.NHibernate.Services
{
    public class NHibernateOpenIDLoginWidgetService : NHibernateDataService<OpenIDLoginWidget>, IOpenIDLoginWidgetService
    {
        #region Constructors

        public NHibernateOpenIDLoginWidgetService(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #endregion

    }
}
