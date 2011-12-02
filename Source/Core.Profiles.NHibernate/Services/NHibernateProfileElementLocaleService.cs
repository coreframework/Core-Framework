using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Profiles.NHibernate.Contracts;
using Core.Profiles.NHibernate.Models;
using Framework.Facilities.NHibernate;
using NHibernate;
using NHibernate.Criterion;

namespace Core.Profiles.NHibernate.Services
{
    public class NHibernateProfileElementLocaleService : NHibernateDataService<ProfileElementLocale>, IProfileElementLocaleService
    {
        public NHibernateProfileElementLocaleService(ISessionManager sessionManager)
            : base(sessionManager)
        {

        }

        public ProfileElementLocale GetLocale(long profileTypeId, String culture)
        {
            IQueryable<ProfileElementLocale> query = CreateQuery();
            return query.Where(locale => locale.ProfileElement.Id == profileTypeId && locale.Culture == culture).FirstOrDefault();
        }

        public ICriteria GetSearchCriteria(string searchString)
        {
            ICriteria criteria = Session.CreateCriteria<ProfileElementLocale>();

            if (!String.IsNullOrEmpty(searchString))
            {
                criteria.Add(Restrictions.Like("Title", searchString, MatchMode.Anywhere));
            }

            return criteria.SetCacheable(true);
        }
    }
}
