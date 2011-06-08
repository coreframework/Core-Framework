using System;
using System.Collections.Generic;
using Castle.Facilities.NHibernateIntegration;
using Core.Forms.NHibernate.Contracts;
using Core.Forms.NHibernate.Models;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Models;
using Framework.Facilities.NHibernate;
using Microsoft.Practices.ServiceLocation;
using NHibernate;

namespace Core.Forms.NHibernate.Services
{
    public class NHibernateFormService: NHibernateDataService<Form>, IFormService
    {
        public NHibernateFormService(ISessionManager sessionManager) : base(sessionManager)
        {
        }

        #region Helper Methods

        /// <summary>
        /// Gets the allowed forms by operation code.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="operation">The operation code.</param>
        /// <returns></returns>
        public IEnumerable<Form> GetAllowedFormsByOperation(ICorePrincipal user, int operation)
        {
            var criteria = GetAllowedFormsCriteria(user, operation);
            return criteria.SetCacheable(true).List<Form>();
        }

        /// <summary>
        /// Gets the allowed forms criteria.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="operationCode">The operation code.</param>
        /// <returns></returns>
        private ICriteria GetAllowedFormsCriteria(ICorePrincipal user, Int32 operationCode)
        {
            ICriteria criteria = Session.CreateCriteria<Form>("forms");
            
            var permissionCommonService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();

            return permissionCommonService.AttachPermissionsCriteria(criteria, user, operationCode, typeof (Form),
                                                                     "forms.Id", "forms.UserId");
        }

        #endregion
    }
}
