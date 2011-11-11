using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.WebContent.NHibernate.Contracts;
using Core.WebContent.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.WebContent.NHibernate.Services
{
    public class NHibernateArticleService: NHibernateDataService<Article>, IArticleService
    {
        public NHibernateArticleService(ISessionManager sessionManager) : base(sessionManager)
        {
          
        }

        public int GetCount(IQueryable<Article> baseQuery)
        {
            return baseQuery.Count();
        }
    }
}
