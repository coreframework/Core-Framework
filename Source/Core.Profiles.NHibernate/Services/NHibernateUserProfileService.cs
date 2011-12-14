using Castle.Facilities.NHibernateIntegration;
using Core.Framework.Permissions.Models;
using Core.Profiles.NHibernate.Contracts;
using Core.Profiles.NHibernate.Models;
using Framework.Facilities.NHibernate;
using NHibernate.Criterion;

namespace Core.Profiles.NHibernate.Services
{
    public class NHibernateUserProfileService: NHibernateDataService<UserProfile>, IUserProfileService
    {
        public NHibernateUserProfileService(ISessionManager sessionManager)
            : base(sessionManager)
        {
          
        }

        /// <summary>
        /// Gets the user profile.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public UserProfile GetUserProfile(ICorePrincipal user)
        {
            var criteria = Session.CreateCriteria<UserProfile>().CreateAlias("User", "user");
            criteria.Add(Restrictions.Eq("user.Id", user.PrincipalId));
            return (UserProfile) criteria.SetCacheable(true).SetMaxResults(1).UniqueResult();
        }
    }
}
