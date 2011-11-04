using System;
using System.Collections.Generic;
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

        public IQueryable<NewsArticle> GetSearchQuery(String searchString)
        {
            var baseQuery = CreateQuery();
            return String.IsNullOrEmpty(searchString) ? baseQuery : baseQuery.Where(article => article.Title.Contains(searchString) || article.Content.Contains(searchString));
        }

        public NewsArticle FindPublished(long id)
        {
            IQueryable<NewsArticle> query = CreateQuery();
            return query.Where(article => article.Id == id && article.StatusId == (int)NewsStatus.Published && article.PublishDate <= DateTime.Now).OrderBy(article => article.LastModifiedDate).FirstOrDefault();
        }

        public NewsArticle FindPublished(String url)
        {
            IQueryable<NewsArticle> query = CreateQuery();
            return query.Where(article => article.Url == url && article.StatusId == (int)NewsStatus.Published && article.PublishDate <= DateTime.Now).OrderBy(article => article.LastModifiedDate).FirstOrDefault();
        }

        public IEnumerable<NewsArticle> FindPublished()
        {
            IQueryable<NewsArticle> query = CreateQuery();
            return query.Where(article => article.StatusId == (int)NewsStatus.Published && article.PublishDate <= DateTime.Now).OrderBy(article => article.LastModifiedDate).AsEnumerable();
        }

        public IEnumerable<NewsArticle> GetForListingWidget(NewsListingWidget listingWidget, int currentPage)
        {
            throw new NotImplementedException();
        }

        public int GetCountForListingWidget(NewsListingWidget listingWidget)
        {
            throw new NotImplementedException();
        }
    }
}
