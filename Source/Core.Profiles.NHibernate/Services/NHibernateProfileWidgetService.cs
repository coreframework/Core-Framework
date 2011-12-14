using Castle.Facilities.NHibernateIntegration;
using Core.Profiles.NHibernate.Contracts;
using Core.Profiles.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.Profiles.NHibernate.Services
{
    class NHibernateProfileWidgetService : NHibernateDataService<ProfileWidget>, IProfileWidgetService
    {
        public NHibernateProfileWidgetService(ISessionManager sessionManager) : base(sessionManager)
        {
          
        }
    }
}