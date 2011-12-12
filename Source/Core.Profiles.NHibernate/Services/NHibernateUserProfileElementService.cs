using Castle.Facilities.NHibernateIntegration;
using Core.Profiles.NHibernate.Contracts;
using Core.Profiles.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.Profiles.NHibernate.Services
{
    public class NHibernateUserProfileElementService: NHibernateDataService<UserProfileElement>, IUserProfileElementService
    {
        public NHibernateUserProfileElementService(ISessionManager sessionManager)
            : base(sessionManager)
        {
          
        }
    }
}
