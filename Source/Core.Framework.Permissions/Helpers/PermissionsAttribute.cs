using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Models;
using Microsoft.Practices.ServiceLocation;

namespace Core.Framework.Permissions.Helpers
{
    /// <summary>
    /// Specifies permissions required for action executing.
    /// </summary>
    public class PermissionsAttribute : ActionFilterAttribute
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionsAttribute"/> class.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="entityType">Type of the entity.</param>
        public PermissionsAttribute(Int32 operation, Type entityType)
        {
            Operation = operation;
            EntityType = entityType;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the operation.
        /// </summary>
        /// <value>The operation.</value>
        public Int32 Operation { get; set; }

        /// <summary>
        /// Gets or sets the type of the entity.
        /// </summary>
        /// <value>The type of the entity.</value>
        public Type EntityType { get; set; }

        #endregion

        #region Filters

        /// <summary>
        /// Called by the MVC framework before the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var permissionService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();

            bool isAllowed =  permissionService.IsAllowed(Operation, filterContext.HttpContext.User as ICorePrincipal, EntityType,
                                               null);

            if (!isAllowed)
            {
                throw new HttpException((int)HttpStatusCode.Forbidden, String.Format("Operation \"{0}\" is denied for current user.", Operation));
            }
        }

        #endregion
    }
}