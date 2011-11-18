using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.WebContent.NHibernate.Contracts;
using Core.WebContent.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.WebContent.NHibernate.Services
{
    public class NHibernateArticleFileService : NHibernateDataService<ArticleFile>, IArticleFileService
    {
        public NHibernateArticleFileService(ISessionManager sessionManager)
            : base(sessionManager)
        {

        }

        public int GetCount(IQueryable<ArticleFile> baseQuery)
        {
            return baseQuery.Count();
        }

        public IQueryable<ArticleFile> GetSearchQuery(string searchString)
        {
            var baseQuery = CreateQuery();
            if (String.IsNullOrEmpty(searchString))
            {
                return baseQuery;
            }
            return baseQuery.Where(file => file.Title.Contains(searchString));
        }
    }
}
