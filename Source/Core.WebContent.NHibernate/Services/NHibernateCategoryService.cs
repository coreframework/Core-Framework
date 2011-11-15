using System;
using System.Collections.Generic;
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
    public class NHibernateCategoryService: NHibernateDataService<WebContentCategory>, ICategoryService
    {
        public NHibernateCategoryService(ISessionManager sessionManager) : base(sessionManager)
        {
          
        }

        public IEnumerable<WebContentCategory> GetAllowedCategoriesByOperation(ICorePrincipal user, int operation)
        {
            var criteria = GetAllowedCategoriesCriteria(user, operation);
            return criteria.SetCacheable(true).List<WebContentCategory>();
        }

        public IEnumerable<WebContentCategory> GetAllowedSectionCategoriesByOperation(ICorePrincipal user, int operation, long sectionId)
        {
            var criteria = GetAllowedCategoriesCriteria(user, operation);
            criteria.CreateAlias("Section", "section").Add(Restrictions.Eq("section.Id", sectionId));
            return criteria.SetCacheable(true).List<WebContentCategory>();
        }

        /// <summary>
        /// Gets the allowed categories criteria.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="operationCode">The operation code.</param>
        /// <returns></returns>
        private ICriteria GetAllowedCategoriesCriteria(ICorePrincipal user, Int32 operationCode)
        {
            ICriteria criteria = Session.CreateCriteria<WebContentCategory>("sections");

            var permissionCommonService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();
            var permissionCriteria = permissionCommonService.GetPermissionsCriteria(user, operationCode, typeof(WebContentCategory),
                                                                                    "categories.Id", "categories.UserId");
            if (permissionCriteria != null)
                criteria.Add(permissionCriteria);
            return criteria;
        }
    }
}
