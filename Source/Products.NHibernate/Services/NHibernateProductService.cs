﻿using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Framework.Facilities.NHibernate;
using NHibernate;
using NHibernate.Criterion;
using Products.NHibernate.Contracts;
using Products.NHibernate.Models;

namespace Products.NHibernate.Services
{
    public class NHibernateProductService : NHibernateDataService<Product>, IProductService
    {
        public NHibernateProductService(ISessionManager sessionManager)
            : base(sessionManager)
        {
          
        }

        public int GetCount(IQueryable<Product> baseQuery)
        {
            return baseQuery.Count();
        }

        public IQueryable<Product> GetProductsByCategory(Category category)
        {
            var baseQuery = CreateQuery();
            return baseQuery.Where(product => product.Categories.Contains(category));
        }

        public IQueryable<Product> GetSearchQuery(string searchString)
        {
            var baseQuery = CreateQuery();
           // if (String.IsNullOrEmpty(searchString))
           // {
                return baseQuery;
          //  }
            return baseQuery.Where(product => product.Title.Contains(searchString));
        }

        public int GetCount(ICriteria baseQuery)
        {
            return ((ICriteria)baseQuery.Clone()).SetProjection(Projections.Count("product.Id")).UniqueResult<int>();
        }

        public ICriteria GetProductCriteria(long[] categoriesIds)
        {
            var productIdSubQuery = DetachedCriteria.For<ProductToCategory>("prodCat")
                .Add(Restrictions.In("prodCat.CategoryId", categoriesIds))
                .SetProjection(Projections.Distinct(Projections.Property("prodCat.ProductId")));
            ICriteria basecriteria = Session.CreateCriteria<Product>("product")
                .Add(Subqueries.PropertyIn("product.Id", productIdSubQuery));
         
            return basecriteria;
        }
    }
}