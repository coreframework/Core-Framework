using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.News.NHibernate.Contracts;
using Core.News.Nhibernate.Models;
using Framework.Facilities.NHibernate;
using NHibernate;
using NHibernate.Criterion;

namespace Core.News.NHibernate.Services
{
    public class NHibernateNewsCategoryLocaleService : NHibernateDataService<NewsCategoryLocale>, INewsCategoryLocaleService
    {
        public NHibernateNewsCategoryLocaleService(ISessionManager sessionManager) : base(sessionManager)
        {
        }

        public NewsCategoryLocale GetLocale(long newsCategoryId, string culture)
        {
            IQueryable<NewsCategoryLocale> query = CreateQuery();
            return query.Where(locale => locale.Category.Id == newsCategoryId && locale.Culture == culture).FirstOrDefault();
        }

        public ICriteria GetSearchCriteria(string searchString)
        {
            ICriteria criteria = Session.CreateCriteria<NewsCategoryLocale>().CreateAlias("Category", "newsCategory");

            DetachedCriteria filter = DetachedCriteria.For<NewsCategoryLocale>("filteredLocale").CreateAlias("Category", "filteredNewsCategory")
                .SetProjection(Projections.Id()).SetMaxResults(1).AddOrder(Order.Desc("filteredLocale.Priority")).Add(
                    Restrictions.EqProperty("filteredNewsCategory.Id", "newsCategory.Id"));

            criteria.Add(Subqueries.PropertyIn("Id", filter));

            if (!String.IsNullOrEmpty(searchString))
            {
                criteria.Add(Restrictions.Like("Title", searchString, MatchMode.Anywhere));
            }

            return criteria.SetCacheable(true);
        }
    }
}
