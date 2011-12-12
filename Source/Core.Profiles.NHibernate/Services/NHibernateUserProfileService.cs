using Castle.Facilities.NHibernateIntegration;
using Core.Profiles.NHibernate.Contracts;
using Core.Profiles.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.Profiles.NHibernate.Services
{
    public class NHibernateUserProfileService: NHibernateDataService<UserProfile>, IUserProfileService
    {
        public NHibernateUserProfileService(ISessionManager sessionManager)
            : base(sessionManager)
        {
          
        }
    }
}
