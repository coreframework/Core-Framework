using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Profiles.NHibernate.Contracts;
using Core.Profiles.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.Profiles.NHibernate.Services
{
    public class NHibernateProfileElementService: NHibernateDataService<ProfileElement>, IProfileElementService
    {
        public NHibernateProfileElementService(ISessionManager sessionManager) : base(sessionManager)
        {
          
        }

        public int GetLastOrderNumber(long profileHeaderId)
        {
            var query = from element in CreateQuery()
                        where element.ProfileHeader.Id == profileHeaderId
                        select element;

            return query.Count() + 1;
        }
    }
}
