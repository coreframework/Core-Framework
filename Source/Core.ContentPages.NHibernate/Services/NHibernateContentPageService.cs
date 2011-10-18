using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.ContentPages.NHibernate.Contracts;
using Core.ContentPages.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.ContentPages.NHibernate.Services
{
    public class NHibernateContentPageService: NHibernateDataService<ContentPage>, IContentPageService
    {
        public NHibernateContentPageService(ISessionManager sessionManager) : base(sessionManager)
        {
          
        }

        public int GetCount(IQueryable<ContentPage> baseQuery)
        {
            return baseQuery.Count();
        }

        public IQueryable<ContentPage> GetSearchQuery(String searchString)
        {
            var baseQuery = CreateQuery();
//            if (String.IsNullOrEmpty(searchString))
//            {
                return baseQuery;
//            }
//            return baseQuery.Where(user => user.Title.Contains(searchString));
        }
    }
}
