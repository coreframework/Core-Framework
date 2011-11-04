using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Extensions;
using Core.Framework.Permissions.Models;
using Core.Web.Helpers;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Core.Web.NHibernate.Permissions.Operations;
using Framework.Mvc.Controllers;
using Microsoft.Practices.ServiceLocation;

namespace Core.Web.Controllers
{
    public partial class PageTemplatesController : FrameworkController
    {
        #region Fields

        private readonly IPageService pageService;

        private readonly IPermissionCommonService permissionService;

        private readonly IPermissionsHelper permissionHelper;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PageTemplatesController"/> class.
        /// </summary>
        public PageTemplatesController()
        {
            pageService = ServiceLocator.Current.GetInstance<IPageService>();
            permissionService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();
            permissionHelper = ServiceLocator.Current.GetInstance<IPermissionsHelper>();

        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether [is page owner] [the specified page].
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns>
        /// 	<c>true</c> if [is page owner] [the specified page]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsPageTemplateOwner(Page page)
        {
            return page != null && this.CorePrincipal() != null && page.User != null &&
                             page.User.Id == this.CorePrincipal().PrincipalId;
        }

        #endregion

        #region Actions

        [HttpGet]
        public virtual ActionResult Show(String url)
        {
            var pageTemplate = pageService.FindTemplateByUrl(url);

            if (pageTemplate == null || !permissionService.IsAllowed((Int32)PageOperations.View, this.CorePrincipal(), typeof(PageTemplate), pageTemplate.Id, IsPageTemplateOwner(pageTemplate), PermissionOperationLevel.Object))
            {
                throw new HttpException((int)HttpStatusCode.NotFound, Translate("Messages.NotFound"));
            }

            return View(MVC.Pages.Views.Show, PageHelper.BindPageViewModel(pageTemplate, this.CorePrincipal()));
        }

        #endregion


    }
}
