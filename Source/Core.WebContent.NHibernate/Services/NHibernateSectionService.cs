using System;
using System.Collections.Generic;
using Castle.Facilities.NHibernateIntegration;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Models;
using Core.WebContent.NHibernate.Contracts;
using Core.WebContent.NHibernate.Models;
using Framework.Facilities.NHibernate;
using Microsoft.Practices.ServiceLocation;
using NHibernate;

namespace Core.WebContent.NHibernate.Services
{
    public class NHibernateSectionService: NHibernateDataService<Section>, ISectionService
    {
        public NHibernateSectionService(ISessionManager sessionManager) : base(sessionManager)
        {
          
        }

        /// <summary>
        /// Gets the allowed sections by operation code.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="operation">The operation code.</param>
        /// <returns></returns>
        public IEnumerable<Section> GetAllowedSectionsByOperation(ICorePrincipal user, int operation)
        {
            var criteria = GetAllowedSectionsCriteria(user, operation);
            return criteria.SetCacheable(true).List<Section>();
        }

        /// <summary>
        /// Gets the allowed sections criteria.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="operationCode">The operation code.</param>
        /// <returns></returns>
        private ICriteria GetAllowedSectionsCriteria(ICorePrincipal user, Int32 operationCode)
        {
            ICriteria criteria = Session.CreateCriteria<Section>("sections");

            var permissionCommonService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();
            var permissionCriteria = permissionCommonService.GetPermissionsCriteria(user, operationCode, typeof(Section),
                                                                                    "sections.Id", "sections.UserId");
            if (permissionCriteria != null)
                criteria.Add(permissionCriteria);
            return criteria;
        }
    }
}
