// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CastleControllerFactory.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Castle.MicroKernel;

namespace Framework.MVC.Controllers
{
    /// <summary>
    /// Instantinate controller throught castle kernel.
    /// </summary>
    public class CastleControllerFactory : DefaultControllerFactory
    {
        #region Fields

        private readonly IKernel kernel;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CastleControllerFactory"/> class.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        public CastleControllerFactory(IKernel kernel)
        {
            this.kernel = kernel;
        }

        #endregion

        #region IControllerFactory members

        /// <summary>
        /// Releases the specified controller.
        /// </summary>
        /// <param name="controller">The controller to release.</param>
        public override void ReleaseController(IController controller)
        {
            var disposable = controller as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }

            kernel.ReleaseComponent(controller);
        }

        /// <summary>
        /// Retrieves the controller instance for the specified request context and controller type.
        /// </summary>
        /// <param name="requestContext">The context of the HTTP request, which includes the HTTP context and route data.</param>
        /// <param name="controllerType">The type of the controller.</param>
        /// <returns>The controller instance.</returns>
        /// <exception cref="T:System.Web.HttpException">
        ///     <paramref name="controllerType"/> is null.</exception>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="controllerType"/> cannot be assigned.</exception>
        /// <exception cref="T:System.InvalidOperationException">An instance of <paramref name="controllerType"/> cannot be created.</exception>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException((int)HttpStatusCode.NotFound, String.Format("The controller for path '{0}' could not be found or it does not implement IController.", requestContext.HttpContext.Request.Path));
            }
            return kernel.Resolve(controllerType) as IController;
        }

        #endregion
    }
}