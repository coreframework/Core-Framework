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
    public class NHibernateCategoryLocaleService : NHibernateDataService<CategoryLocale>, ICategoryLocaleService
    {
        public NHibernateCategoryLocaleService(ISessionManager sessionManager) : base(sessionManager)
        {
        }

        public CategoryLocale GetLocale(long categoryId, string culture)
        {
            IQueryable<CategoryLocale> query = CreateQuery();
            return query.Where(locale => locale.Category.Id == categoryId && locale.Culture == culture).FirstOrDefault();
        }

        public ICriteria GetSearchCriteria(string searchString)
        {
            ICriteria criteria = Session.CreateCriteria<CategoryLocale>().CreateAlias("Category", "category");

            DetachedCriteria filter = DetachedCriteria.For<CategoryLocale>("filteredLocale").CreateAlias("Category", "filteredCategory")
                .SetProjection(Projections.Id()).SetMaxResults(1).AddOrder(Order.Desc("filteredLocale.Priority")).Add(
                    Restrictions.EqProperty("filteredCategory.Id", "category.Id"));

            criteria.Add(Subqueries.PropertyIn("Id", filter));

            if (!String.IsNullOrEmpty(searchString))
            {
                criteria.Add(Restrictions.Like("Title", searchString, MatchMode.Anywhere));
            }

            return criteria.SetCacheable(true);
        }
    }
}
