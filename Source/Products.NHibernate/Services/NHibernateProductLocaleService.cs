using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Framework.Facilities.NHibernate;
using NHibernate;
using NHibernate.Criterion;
using Products.NHibernate.Contracts;
using Products.NHibernate.Models;

namespace Products.NHibernate.Services
{
    public class NHibernateProductLocaleService : NHibernateDataService<ProductLocale>, IProductLocaleService
    {
        public NHibernateProductLocaleService(ISessionManager sessionManager) : base(sessionManager)
        {
        }

        public ProductLocale GetLocale(long productId, string culture)
        {
            IQueryable<ProductLocale> query = CreateQuery();
            return query.Where(locale => locale.Product.Id == productId && locale.Culture == culture).FirstOrDefault();
        }

        public ICriteria GetSearchCriteria(string searchString)
        {
            ICriteria criteria = Session.CreateCriteria<ProductLocale>().CreateAlias("Product", "product");

            DetachedCriteria filter = DetachedCriteria.For<ProductLocale>("filteredLocale").CreateAlias("Product", "filteredProduct")
                .SetProjection(Projections.Id()).SetMaxResults(1).AddOrder(Order.Desc("filteredLocale.Priority")).Add(
                    Restrictions.EqProperty("filteredProduct.Id", "product.Id"));

            criteria.Add(Subqueries.PropertyIn("Id", filter));

            if (!String.IsNullOrEmpty(searchString))
            {
                criteria.Add(Restrictions.Like("Title", searchString, MatchMode.Anywhere));
            }

            return criteria.SetCacheable(true);
        }
    }
}
