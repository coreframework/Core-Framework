using Castle.Facilities.NHibernateIntegration;
using Core.FormLogin.NHibernate.Contracts;
using Core.FormLogin.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.FormLogin.NHibernate.Services
{
    public class NHibernateFormLoginWidgetService : NHibernateDataService<FormLoginWidget>, IFormLoginWidgetService
    {
        #region Constructors

        public NHibernateFormLoginWidgetService(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #endregion

    }
}
