using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.News.NHibernate.Contracts;
using Framework.Facilities.NHibernate;
using Core.News.Nhibernate.Models;

namespace Core.News.NHibernate.Services
{
    public class NHibernateNewsCategoryService : NHibernateDataService<NewsCategory>, INewsCategoryService
    {
        public NHibernateNewsCategoryService(ISessionManager sessionManager)
            : base(sessionManager)
        {
          
        }


        public IQueryable<NewsCategory> GetSearchQuery(String search)
        {
            var baseQuery = CreateQuery();
           // if (String.IsNullOrEmpty(search))
          //  {
                return baseQuery;
          //  }
           // return baseQuery.Where(category => category.CurrentCategoryLocales.Any(item => item.Title.Contains(search)));
        }

        public int GetCount(IQueryable<NewsCategory> searchQuery)
        {
            return searchQuery.Count();
        }

        public IEnumerable<NewsCategory> GetCategories(String search)
        {
            var baseQuery = CreateQuery();
          //  var allCategories = baseQuery.ToList();
            if (String.IsNullOrEmpty(search))
            {
                return baseQuery;
            }
            return baseQuery.Where(category => category.CurrentCategoryLocales.Any(item => item.Title.Contains(search)));
        }

        public int GetCount(String searchQuery)
        {
            return 0;
        }
    }
}
