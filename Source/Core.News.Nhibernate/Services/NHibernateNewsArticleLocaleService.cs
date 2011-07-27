using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.News.Nhibernate.Contracts;
using Core.News.Nhibernate.Models;
using Framework.Facilities.NHibernate;

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
    }
}
