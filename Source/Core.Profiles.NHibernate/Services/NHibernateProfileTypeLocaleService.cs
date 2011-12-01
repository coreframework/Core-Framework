using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Profiles.NHibernate.Contracts;
using Core.Profiles.NHibernate.Models;
using Framework.Facilities.NHibernate;
using NHibernate;

namespace Core.Profiles.NHibernate.Services
{
    public class NHibernateProfileTypeLocaleService : NHibernateDataService<ProfileTypeLocale>, IProfileTypeLocaleService
    {
        public NHibernateProfileTypeLocaleService(ISessionManager sessionManager)
            : base(sessionManager)
        {

        }

        public ProfileTypeLocale GetLocale(long profileTypeId, String culture)
        {
            IQueryable<ProfileTypeLocale> query = CreateQuery();
            return query.Where(locale => locale.ProfileType.Id == profileTypeId && locale.Culture == culture).FirstOrDefault();
        }

        public ICriteria GetSearchCriteria(string searchString)
        {
            throw new NotImplementedException();
        }
    }
}
