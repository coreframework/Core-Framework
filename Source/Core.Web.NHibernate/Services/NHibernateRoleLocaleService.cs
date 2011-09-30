using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Framework.Facilities.NHibernate;
using NHibernate;
using NHibernate.Criterion;

namespace Core.Web.NHibernate.Services
{
    public class NHibernateRoleLocaleService : NHibernateDataService<RoleLocale>, IRoleLocaleService
    {
        public NHibernateRoleLocaleService(ISessionManager sessionManager) : base(sessionManager)
        {

        }

        public RoleLocale GetLocale(long roleId, String culture)
        {
            IQueryable<RoleLocale> query = CreateQuery();
            return query.Where(locale => locale.Role.Id == roleId && locale.Culture == culture).FirstOrDefault();
        }

        public ICriteria GetSearchCriteria(String searchString)
        {
            ICriteria criteria = Session.CreateCriteria<RoleLocale>().CreateAlias("Role", "role");

            DetachedCriteria filter = DetachedCriteria.For<RoleLocale>("filteredLocale").CreateAlias("Role", "filteredRole")
                .SetProjection(Projections.Id()).SetMaxResults(1).AddOrder(Order.Desc("filteredLocale.Priority")).Add(
                    Restrictions.EqProperty("filteredRole.Id", "role.Id"));

            criteria.Add(Subqueries.PropertyIn("Id", filter));

            if (!String.IsNullOrEmpty(searchString))
            {
                criteria.Add(Restrictions.Like("Name", searchString, MatchMode.Anywhere));
            }

            return criteria.SetCacheable(true);
        }
    }
}