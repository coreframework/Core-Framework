using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Profiles.NHibernate.Contracts;
using Core.Profiles.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.Profiles.NHibernate.Services
{
    public class NHibernateProfileHeaderLocaleService : NHibernateDataService<ProfileHeaderLocale>, IProfileHeaderLocaleService
    {
        public NHibernateProfileHeaderLocaleService(ISessionManager sessionManager)
            : base(sessionManager)
        {

        }

        public ProfileHeaderLocale GetLocale(long profileTypeId, String culture)
        {
            IQueryable<ProfileHeaderLocale> query = CreateQuery();
            return query.Where(locale => locale.ProfileHeader.Id == profileTypeId && locale.Culture == culture).FirstOrDefault();
        }
    }
}
