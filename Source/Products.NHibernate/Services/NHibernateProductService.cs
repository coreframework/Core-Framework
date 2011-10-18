using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Framework.Facilities.NHibernate;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
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

        public int GetCount(IQueryable<Product> searchQuery)
        {
            return searchQuery.Count();
        }

        public IQueryable<Product> GetProductsByCategory(Category category)
        {
            var baseQuery = CreateQuery();
            return baseQuery.Where(product => product.Categories.Contains(category));
        }

        public IQueryable<Product> GetSearchQuery(String search)
        {
            var baseQuery = CreateQuery();
           // if (String.IsNullOrEmpty(searchString))
           // {
                return baseQuery;
          //  }

            return baseQuery.Where(product => product.Title.Contains(search));
        }

        public int GetCount(ICriteria searchCriteria)
        {
            return ((ICriteria)searchCriteria.Clone()).SetProjection(Projections.Count("product.Id")).UniqueResult<int>();
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

        public Product GetProduct(long id, long widgetId)
        {
            var categoriesIdSubQuery = DetachedCriteria.For<ProductWidgetToCategory>("prodWidCat").CreateAlias("Category", "category", JoinType.LeftOuterJoin).CreateAlias("ProductWidget", "widget", JoinType.LeftOuterJoin)
              .Add(Restrictions.Eq("widget.Id", widgetId))
              .SetProjection(Projections.Property("category.Id"));

            var productIdSubQuery = DetachedCriteria.For<ProductToCategory>("prodCat")
              .Add(Subqueries.PropertyIn("prodCat.CategoryId", categoriesIdSubQuery))
              .SetProjection(Projections.Distinct(Projections.Property("prodCat.ProductId")));
            ICriteria basecriteria = Session.CreateCriteria<Product>("product")
               .Add(Subqueries.PropertyIn("product.Id", productIdSubQuery))
               .Add(Restrictions.IdEq(id));
            return (Product)basecriteria.UniqueResult();
        }
    }
}
