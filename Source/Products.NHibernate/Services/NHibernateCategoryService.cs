using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Framework.Core.Localization;
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


        public IQueryable<Category> GetSearchQuery(string search)
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

        public IEnumerable<Category> GetCategories(string searchString)
        {
            var baseQuery = CreateQuery();
          //  var allCategories = baseQuery.ToList();
            if (String.IsNullOrEmpty(searchString))
            {
                return baseQuery;
            }
            return baseQuery.Where(category => category.CurrentCategoryLocales.Any(item=>item.Title.Contains(searchString)));
        }

        public int GetCount(string searchQuery)
        {
            return 0;
        }
    }
}
