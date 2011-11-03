using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Framework.Facilities.NHibernate;
using Products.NHibernate.Contracts;
using Products.NHibernate.Models;

namespace Products.NHibernate.Services
{
    public class NHibernateCategoryService : NHibernateDataService<Category>, ICategoryService
    {
        public NHibernateCategoryService(ISessionManager sessionManager)
            : base(sessionManager)
        {
          
        }

        public IQueryable<Category> GetSearchQuery(String search)
        {
            var baseQuery = CreateQuery();
           // if (String.IsNullOrEmpty(search))
          //  {
                return baseQuery;
          //  }
           // return baseQuery.Where(category => category.CurrentCategoryLocales.Any(item => item.Title.Contains(search)));
        }

        public int GetCount(IQueryable<Category> searchQuery)
        {
            return searchQuery.Count();
        }

        public IEnumerable<Category> GetCategories(String search)
        {
            var baseQuery = CreateQuery();
            if (String.IsNullOrEmpty(search))
            {
                return baseQuery;
            }
            return baseQuery.Where(category => category.CurrentLocales.Any(item => item.Title.Contains(search)));
        }

        public int GetCount(String searchQuery)
        {
            return 0;
        }
    }
}
