using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core.Web.NHibernate.Contracts.Permissions;
using Microsoft.Practices.ServiceLocation;

namespace Core.Web.NHibernate.Helpers.Permissions
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
        /// <param name="entityIdParam">The entity id param.</param>
        public PermissionsAttribute(Int32 operation, Type entityType, String entityIdParam)
        {
            Operation = operation;
            EntityType = entityType;
            EntityIdParam = entityIdParam;
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

        /// <summary>
        /// Gets or sets the entity id param.
        /// </summary>
        /// <value>The entity id param.</value>
        public String EntityIdParam { get; set; }

        #endregion

        #region Filters

        /// <summary>
        /// Called by the MVC framework before the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userService = ServiceLocator.Current.GetInstance<ISystemUserService>();
            var permissionService = ServiceLocator.Current.GetInstance<IPermissionService>();

            bool allowed = false;

           /* if (filterContext.HttpContext.User != null && filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var user = userService.GetUser(filterContext.HttpContext.User.Identity.Name);

                if (user != null)
                {
                    if (EntityType != null)
                    {
                        long id = Convert.ToInt64(filterContext.ActionParameters[EntityIdParam]);
                        allowed = permissionService.IsAllowed(Operation).On(EntityType, id).For(user).Check();
                    }
                    else
                    {
                        allowed = permissionService.IsAllowed(Operation).For(user).Check();
                    }
                }
            }*/

            if (!allowed)
            {
                throw new HttpException((int)HttpStatusCode.Forbidden, String.Format("Operation \"{0}\" is denied for current user.", Operation));
            }
        }

        #endregion
    }
}