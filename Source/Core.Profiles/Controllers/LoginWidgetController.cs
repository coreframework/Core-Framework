using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Framework.MEF.Web;
using Core.Framework.Plugins.Web;
using Core.Profiles.Models;
using Core.Profiles.Widgets;

namespace Core.Profiles.Controllers
{
    [Export(typeof(IController)), ExportMetadata("Name", "LoginWidget")]
    public partial class LoginWidgetController : CoreWidgetController
    {
        #region Properties

        public override String ControllerWidgetIdentifier
        {
            get
            {
                return LoginWidget.Instance.Identifier;
            }
        }

        #endregion

        [ChildActionOnly]
        public virtual ActionResult ViewWidget(ICoreWidgetInstance instance)
        {
            return PartialView(new LoginWidgetViewModel());
        }

    }
}
