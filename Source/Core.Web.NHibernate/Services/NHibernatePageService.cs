using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Models;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Core.Web.NHibernate.Permissions.Operations;
using Framework.Facilities.NHibernate;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;

namespace Core.Web.NHibernate.Services
{
    public class NHibernatePageService : NHibernateDataService<Page>, IPageService
    {
        #region Constructors

        public NHibernatePageService(ISessionManager sessionManager) : base(sessionManager)
        {}

        #endregion

        #region Methods

        public override IQueryable<Page> CreateQuery()
        {
            var baseQuery = base.CreateQuery();
            return baseQuery.Where(page => !page.IsTemplate);
        }

        public Page FindByUrl(String url)
        {
            var query = from page in CreateQuery()
                        where page.Url == url
                        select page;

            return query.FirstOrDefault();
        }

        public Page FindTemplateByUrl(String url)
        {
            var query = from page in base.CreateQuery()
                        where page.Url == url && page.IsTemplate
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
            var criteria = GetAllowedPagesCriteria(user, operationCode, false);

            criteria.Add(Restrictions.IsNull("pages.ParentPageId"));

            return (Page) criteria.SetCacheable(true).SetMaxResults(1).AddOrder(Order.Asc("pages.OrderNumber")).UniqueResult();
        }

        public IEnumerable<Page> GetAllowedPagesByOperation(ICorePrincipal user, Int32 operationCode)
        {
            var criteria = GetAllowedPagesCriteria(user, operationCode, false);

            return criteria.SetCacheable(true).List<Page>();
        }

        public IEnumerable<Page> GetAllowedPageTemplatesByOperation(ICorePrincipal user, Int32 operationCode)
        {
            var criteria = GetAllowedPagesCriteria(user, operationCode, true);

            return criteria.SetCacheable(true).List<Page>();
        }

        public IEnumerable<Page> GetAllowedPagesForMainMenu(ICorePrincipal user)
        {
            var criteria = GetAllowedPagesCriteria(user, (int)PageOperations.View, false);
            //criteria.Add(Restrictions.Eq("pages.HideInMainMenu", false));

            return criteria.SetCacheable(true).List<Page>();
        }

        /// <summary>
        /// Gets the allowed pages criteria.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="operationCode">The operation code.</param>
        /// <returns></returns>
        private ICriteria GetAllowedPagesCriteria(ICorePrincipal user, Int32 operationCode, bool isTemplate)
        {
            ICriteria criteria = Session.CreateCriteria<Page>("pages").CreateAlias("User", "pageUser", JoinType.LeftOuterJoin);
            var permissionCommonService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();
            var permissionCriteria = permissionCommonService.GetPermissionsCriteria(user, operationCode, typeof (Page),
                                                                                    "pages.Id", "pageUser.Id");
            criteria.Add(Restrictions.Eq("pages.IsTemplate", isTemplate));
            if (permissionCriteria!=null)
                criteria.Add(permissionCriteria);

            return criteria;
        }

        /// <summary>
        /// Gets the pages from template.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <returns></returns>
        public IEnumerable<Page> GetPagesFromTemplate(Page template)
        {
            return CreateQuery().Where(page => !page.IsTemplate && page.Template == template).ToList();
        }

        #endregion
    }
}
