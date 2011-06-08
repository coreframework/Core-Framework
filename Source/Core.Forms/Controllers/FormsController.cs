using System;
using System.ComponentModel.Composition;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core.Forms.NHibernate.Contracts;
using Core.Forms.NHibernate.Models;
using Core.Forms.NHibernate.Permissions.Operations;
using Core.Forms.Permissions.Operations;
using Core.Framework.MEF.Web;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Extensions;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Microsoft.Practices.ServiceLocation;

namespace Core.Forms.Controllers
{
    [Export(typeof(IController)), ExportMetadata("Name", "Forms")]
    [Permissions((int)FormsPluginOperations.ManageForms, typeof(FormsPlugin))]
    public partial class FormsController : CoreController
    {
        #region Fields

        private readonly IFormService _formsService;

        private readonly IPermissionCommonService _permissionService;

        private readonly IPermissionsHelper _permissionsHelper;

        #endregion

        #region CoreController Members

        /// <summary>
        /// Gets the controller plugin identifier.
        /// </summary>
        /// <value>The controller plugin identifier.</value>
        public override string ControllerPluginIdentifier
        {
            get { return FormsPlugin.GetPluginIdentifier(); }
        }

        #endregion

        #region Constructor

        public FormsController()
        {
            _formsService = ServiceLocator.Current.GetInstance<IFormService>();
            _permissionService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();
            _permissionsHelper = ServiceLocator.Current.GetInstance<IPermissionsHelper>();

        }

        #endregion

        #region Helper Methods

        private bool IsFormOwner(Form form)
        {
            return form != null && this.CorePrincipal() != null && form.UserId != null &&
                             form.UserId == this.CorePrincipal().PrincipalId;
        }

        #endregion

        #region Actions

        /// <summary>
        /// Shows the list of all available forms.
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult ShowAll()
        {
            return View("Admin/FormsList", _formsService.GetAllowedFormsByOperation(this.CorePrincipal(), (Int32)FormOperations.View));
        }

        public virtual ActionResult ShowPermissions(long formId)
        {
            var form = _formsService.Find(formId);

            if (form == null || !_permissionService.IsAllowed((Int32)FormOperations.Permissions, this.CorePrincipal(), typeof(Form), form.Id, IsFormOwner(form), PermissionOperationLevel.Object))
            {
                throw new HttpException((int)HttpStatusCode.NotFound, "Not Found");
            }

            return View("Admin/FormPermissions", _permissionsHelper.BindPermissionsModel(form.Id, typeof(Form), false));
        }

        [HttpPost]
        public virtual ActionResult ApplyPermissions(PermissionsModel model)
        {
            var form = _formsService.Find(model.EntityId);

            if (form != null)
            {
                if (_permissionService.IsAllowed((Int32)FormOperations.Permissions, this.CorePrincipal(), typeof(Form), form.Id, IsFormOwner(form), PermissionOperationLevel.Object))
                {
                    _permissionsHelper.ApplyPermissions(model, typeof(Form));
                }
                if (_permissionService.IsAllowed((Int32)FormOperations.Permissions, this.CorePrincipal(), typeof(Form), form.Id, IsFormOwner(form), PermissionOperationLevel.Object))
                    return Content(Url.Action("ShowPermissions", "Forms", new { formId = form.Id }));
            }

            return Content(Url.Action("ShowAll"));
        }

        #endregion
    }
}
