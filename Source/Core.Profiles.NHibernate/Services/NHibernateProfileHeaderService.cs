using System.Collections.Generic;
using System.Linq;
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
            criteria.AddOrder(Order.Asc("OrderNumber"));
            return criteria.SetCacheable(true).List<ProfileHeader>();
        }

        public int GetLastOrderNumber(long profileTypeId)
        {
            var query = from header in CreateQuery()
                        where header.ProfileType.Id == profileTypeId
                        select header;

            return query.Count() + 1;
        }
    }
}
