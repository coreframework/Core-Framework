using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.News.Nhibernate.Contracts;
using Core.News.Nhibernate.Models;
using Framework.Facilities.NHibernate;
using NHibernate;
using NHibernate.Criterion;

namespace Core.News.Nhibernate.Services
{
    public class NHibernateNewsArticleLocaleService : NHibernateDataService<NewsArticleLocale>, INewsArticleLocaleService
    {
        public NHibernateNewsArticleLocaleService(ISessionManager sessionManager)
            : base(sessionManager)
        {

        }

        public NewsArticleLocale GetLocale(long newsArticleId, String culture)
        {
            IQueryable<NewsArticleLocale> query = CreateQuery();
            return query.Where(locale => locale.NewsArticle.Id == newsArticleId && locale.Culture == culture).FirstOrDefault();
        }

        public ICriteria GetSearchCriteria(string searchString)
        {
            ICriteria criteria = Session.CreateCriteria<NewsArticleLocale>().CreateAlias("NewsArticle", "newsArticle");

            DetachedCriteria filter = DetachedCriteria.For<NewsArticleLocale>("filteredLocale").CreateAlias("NewsArticle", "filteredNewsArticle")
                .SetProjection(Projections.Id()).SetMaxResults(1).AddOrder(Order.Desc("filteredLocale.Priority")).Add(
                    Restrictions.EqProperty("filteredNewsArticle.Id", "newsArticle.Id"));

            criteria.Add(Subqueries.PropertyIn("Id", filter));

            if (!String.IsNullOrEmpty(searchString))
            {
                criteria.Add(Restrictions.Or(Restrictions.Like("Title", searchString, MatchMode.Anywhere), Restrictions.Like("Summary", searchString, MatchMode.Anywhere)));
            }

            return criteria.SetCacheable(true);
        }
    }
}
