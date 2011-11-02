using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Framework.Facilities.NHibernate;
using NHibernate;
using NHibernate.Criterion;

namespace Core.Web.NHibernate.Services
{
    public class NHibernatePageLocaleService : NHibernateDataService<PageLocale>, IPageLocaleService
    {
        public NHibernatePageLocaleService(ISessionManager sessionManager) : base(sessionManager)
        {}

        public PageLocale GetLocale(long pageId, String culture)
        {
            IQueryable<PageLocale> query = CreateQuery();
            return query.Where(locale => locale.Page.Id == pageId && locale.Culture == culture).FirstOrDefault();
        }

        public IList<PageLocale> GetLocales(long pageId)
        {
            IQueryable<PageLocale> query = CreateQuery();
            return query.Where(locale => locale.Page.Id == pageId).ToList();
        }

        public ICriteria GetSearchCriteria(string searchString, bool isTemplate)
        {
            ICriteria criteria = Session.CreateCriteria<PageLocale>().CreateAlias("Page", "page");

            DetachedCriteria filter = DetachedCriteria.For<PageLocale>("filteredLocale").CreateAlias("Page", "filteredPage")
                .SetProjection(Projections.Id()).SetMaxResults(1).AddOrder(Order.Desc("filteredLocale.Priority")).Add(
                    Restrictions.EqProperty("filteredPage.Id", "page.Id")).Add(
                    Restrictions.Eq("filteredPage.IsTemplate", isTemplate));

            criteria.Add(Subqueries.PropertyIn("Id", filter));            

            if (!String.IsNullOrEmpty(searchString))
            {
                criteria.Add(Restrictions.Like("Title", searchString, MatchMode.Anywhere));
            }

            return criteria.SetCacheable(true);
        }
    }
}