using System.Collections.Generic;
using Castle.Facilities.NHibernateIntegration;
using Core.Profiles.NHibernate.Contracts;
using Core.Profiles.NHibernate.Models;
using Framework.Facilities.NHibernate;
using NHibernate;
using NHibernate.Criterion;

namespace Core.Profiles.NHibernate.Services
{
    public class NHibernateProfileHeaderService: NHibernateDataService<ProfileHeader>, IProfileHeaderService
    {
        public NHibernateProfileHeaderService(ISessionManager sessionManager)
            : base(sessionManager)
        {
          
        }

        public IEnumerable<ProfileHeader> GetProfileHeaders(long profileId)
        {
            ICriteria criteria = Session.CreateCriteria<ProfileHeader>();
            criteria.CreateAlias("ProfileType", "profile").Add(Restrictions.Eq("profile.Id", profileId));
            return criteria.SetCacheable(true).List<ProfileHeader>();
        }
    }
}
