using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.ContentPages.NHibernate.Contracts;
using Core.ContentPages.NHibernate.Models;
using Framework.Facilities.NHibernate;
using NHibernate;
using NHibernate.Criterion;

namespace Core.ContentPages.NHibernate.Services
{
    public class NHibernateContentPageLocaleService : NHibernateDataService<ContentPageLocale>, IContentPageLocaleService
    {
        public NHibernateContentPageLocaleService(ISessionManager sessionManager)
            : base(sessionManager)
        {

        }

        public ContentPageLocale GetLocale(long contentPageId, String culture)
        {
            IQueryable<ContentPageLocale> query = CreateQuery();
            return query.Where(locale => locale.ContentPage.Id == contentPageId && locale.Culture == culture).FirstOrDefault();
        }

        public ICriteria GetSearchCriteria(String searchString)
        {
            ICriteria criteria = Session.CreateCriteria<ContentPageLocale>().CreateAlias("ContentPage", "contentPage");

            DetachedCriteria filter = DetachedCriteria.For<ContentPageLocale>("filteredLocale").CreateAlias("ContentPage", "filteredContentPage")
                .SetProjection(Projections.Id()).SetMaxResults(1).AddOrder(Order.Desc("filteredLocale.Priority")).Add(
                    Restrictions.EqProperty("filteredContentPage.Id", "contentPage.Id"));

            criteria.Add(Subqueries.PropertyIn("Id", filter));

            if (!String.IsNullOrEmpty(searchString))
            {
                criteria.Add(Restrictions.Like("Title", searchString, MatchMode.Anywhere));
            }

            return criteria.SetCacheable(true);
        }
    }
}
