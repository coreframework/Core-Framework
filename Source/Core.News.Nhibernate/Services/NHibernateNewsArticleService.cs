using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.News.Nhibernate.Contracts;
using Core.News.Nhibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.News.Nhibernate.Services
{
    public class NHibernateNewsArticleService : NHibernateDataService<NewsArticle>, INewsArticleService
    {
        public NHibernateNewsArticleService(ISessionManager sessionManager)
            : base(sessionManager)
        {
          
        }

        public int GetCount(IQueryable<NewsArticle> baseQuery)
        {
            return baseQuery.Count();
        }

        public IQueryable<NewsArticle> GetSearchQuery(string searchString)
        {
            var baseQuery = CreateQuery();
            return String.IsNullOrEmpty(searchString) ? baseQuery : baseQuery.Where(article => article.Title.Contains(searchString) || article.Content.Contains(searchString));
        }
    }
}
