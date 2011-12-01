using Castle.Facilities.NHibernateIntegration;
using Core.Profiles.NHibernate.Contracts;
using Core.Profiles.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.Profiles.NHibernate.Services
{
    class NHibernateRegistrationWidgetService : NHibernateDataService<RegistrationWidget>, IRegistrationWidgetService
    {
        public NHibernateRegistrationWidgetService(ISessionManager sessionManager) : base(sessionManager)
        {
          
        }
    }
}