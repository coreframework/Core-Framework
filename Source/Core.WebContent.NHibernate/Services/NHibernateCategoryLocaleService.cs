using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Models;
using Core.WebContent.NHibernate.Contracts;
using Core.WebContent.NHibernate.Models;
using Framework.Facilities.NHibernate;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using NHibernate.Criterion;

namespace Core.WebContent.NHibernate.Services
{
    public class NHibernateCategoryLocaleService : NHibernateDataService<WebContentCategoryLocale>, ICategoryLocaleService
    {
        public NHibernateCategoryLocaleService(ISessionManager sessionManager)
            : base(sessionManager)
        {

        }

        public WebContentCategoryLocale GetLocale(long categoryId, String culture)
        {
            IQueryable<WebContentCategoryLocale> query = CreateQuery();
            return query.Where(locale => locale.Category.Id == categoryId && locale.Culture == culture).FirstOrDefault();
        }

        public ICriteria GetSearchCriteria(String searchString, ICorePrincipal user, int operationCode)
        {
            ICriteria criteria = Session.CreateCriteria<WebContentCategoryLocale>().CreateAlias("Category", "category");

            DetachedCriteria filter = DetachedCriteria.For<WebContentCategoryLocale>("filteredLocale").CreateAlias("Category", "filteredCategory")
                .SetProjection(Projections.Id()).SetMaxResults(1).AddOrder(Order.Desc("filteredLocale.Priority")).Add(
                    Restrictions.EqProperty("filteredCategory.Id", "category.Id"));

            criteria.Add(Subqueries.PropertyIn("Id", filter));

            //apply permissions criteria

            var categorysCriteria = DetachedCriteria.For<WebContentCategory>("categorys");
            var permissionCommonService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();
            var permissionCriteria = permissionCommonService.GetPermissionsCriteria(user, operationCode, typeof(WebContentCategory),
                                                                                    "categorys.Id", "categorys.UserId");
            if (permissionCriteria != null)
            {
                categorysCriteria.Add(permissionCriteria).SetProjection(Projections.Id());
                criteria.Add(Subqueries.PropertyIn("category.Id", categorysCriteria));
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                criteria.Add(Restrictions.Like("Title", searchString, MatchMode.Anywhere));
            }

            return criteria.SetCacheable(true);
        }
    }
}
