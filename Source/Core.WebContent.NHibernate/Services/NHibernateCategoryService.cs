using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.WebContent.NHibernate.Contracts;
using Core.WebContent.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.WebContent.NHibernate.Services
{
    public class NHibernateCategoryService: NHibernateDataService<WebContentCategory>, ICategoryService
    {
        public NHibernateCategoryService(ISessionManager sessionManager) : base(sessionManager)
        {
          
        }

        public int GetCount(IQueryable<WebContentCategory> baseQuery)
        {
            return baseQuery.Count();
        }
    }
}
