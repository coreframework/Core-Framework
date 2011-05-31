using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Core.Web.NHibernate.Models.Permissions;
using Core.Web.NHibernate.Models.Static;
using Framework.Facilities.NHibernate;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using NHibernate.Type;

namespace Core.Web.NHibernate.Services
{
    public class NHibernatePageService : NHibernateDataService<Page>, IPageService
    {
        #region Constructors

        public NHibernatePageService(ISessionManager sessionManager) : base(sessionManager)
        {}

        #endregion

        #region Methods

        public Page FindByUrl(String url)
        {
            var query = from page in CreateQuery()
                        where page.Url == url
                        select page;

            return query.FirstOrDefault();
        }

        public IEnumerable<Page> FindSiblingPages(long? parentPageId)
        {
            var query = from page in CreateQuery()
                        where page.ParentPageId == parentPageId
                        select page;

            return query.ToList();
        }

        public Int32 GetLastOrderNumber(long? parentPageId)
        {
            var query = from page in CreateQuery()
                        where page.ParentPageId == parentPageId
                        select page;

            return query.Count() + 1;
        }

        public Page GetFirstAllowedPage(ICorePrincipal user, Int32 operationCode)
        {
            var criteria = GetAllowedPagesCriteria(user, operationCode);

            criteria.Add(Restrictions.IsNull("pages.ParentPageId"));

            return (Page) criteria.SetCacheable(true).SetMaxResults(1).AddOrder(Order.Asc("pages.OrderNumber")).UniqueResult();
        }

        public IEnumerable<Page> GetAllowedPagesByOperation(ICorePrincipal user, Int32 operationCode)
        {
            var criteria = GetAllowedPagesCriteria(user, operationCode);

            return criteria.SetCacheable(true).List<Page>();
        }

        /// <summary>
        /// Gets the allowed pages criteria.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="operationCode">The operation code.</param>
        /// <returns></returns>
        private ICriteria GetAllowedPagesCriteria(ICorePrincipal user, Int32 operationCode)
        {
            ICriteria criteria = Session.CreateCriteria<Page>("pages").CreateAlias("User", "pageUser", JoinType.LeftOuterJoin);

            if (user != null)
            {
                if (user.IsInRole(SystemRoles.Administrator.ToString()))
                    return criteria;

                var rolesSubQuery = DetachedCriteria.For<Role>()
                               .CreateAlias("Users", "user")
                               .Add(Restrictions.Eq("user.id", user.PrincipalId))
                               .SetProjection(Projections.Id());

                var userUserGroupsSubQuery = DetachedCriteria.For<UserGroup>()
                          .CreateAlias("Users", "userGroupUser", JoinType.LeftOuterJoin)
                          .Add(Restrictions.Eq("userGroupUser.id", user.PrincipalId))
                          .SetProjection(Projections.Id());

                var userGroupsRolesSubQuery = DetachedCriteria.For<Role>()
                             .CreateAlias("UserGroups", "userGroup", JoinType.LeftOuterJoin)
                             .Add(Subqueries.PropertyIn("userGroup.id", userUserGroupsSubQuery))
                             .SetProjection(Projections.Id());

                var permissionsSubQuery = DetachedCriteria.For<Permission>()
                                .Add(Restrictions.EqProperty("EntityId", "pages.Id")).CreateAlias("EntityType", "et").Add(Restrictions.Eq("et.Name", PermissionsHelper.GetEntityType(typeof(Page)))).
                                 Add(Restrictions.Or(Restrictions.Or(
                                          Restrictions.Or(Subqueries.PropertyIn("Role.Id", rolesSubQuery), Subqueries.PropertyIn("Role.Id", userGroupsRolesSubQuery)),
                                          Restrictions.Eq("Role.Id", (Int64)SystemRoles.User)),

                                          Restrictions.And(Restrictions.IsNotNull("pageUser.id"), Restrictions.And(Restrictions.Eq("pageUser.id", user.PrincipalId), Restrictions.Eq("Role.Id", (Int64)SystemRoles.Owner)))
                                          )).Add(

                                          Restrictions.Eq(Projections.SqlProjection(String.Format("Permissions & {0} as result", operationCode), new[] { "result" }, new IType[] { NHibernateUtil.Int32 }), operationCode))
                                .SetProjection(Projections.Id());

                criteria.Add(Subqueries.Exists(permissionsSubQuery));
            }
            else
            {
                var permissionsSubQuery = DetachedCriteria.For<Permission>()
                               .Add(Restrictions.EqProperty("EntityId", "pages.Id")).CreateAlias("EntityType", "et").Add(Restrictions.Eq("et.Name", PermissionsHelper.GetEntityType(typeof(Page)))).
                                Add(Restrictions.Eq("Role.Id", (Int64)SystemRoles.Guest)).Add(
                                Restrictions.Eq(Projections.SqlProjection(String.Format("Permissions & {0} as result", operationCode), new[] { "result" }, new IType[] { NHibernateUtil.Int32 }), operationCode))
                               .SetProjection(Projections.Id());

                criteria.Add(Subqueries.Exists(permissionsSubQuery));
            }

            return criteria;
        }

        #endregion
    }
}
