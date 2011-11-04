using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using Core.ContentPages.Widgets;
using Core.Framework.MEF.Web;
using Core.Framework.Plugins.Web;

namespace Core.ContentPages.Controllers
{
    [Export(typeof(IController)), ExportMetadata("Name", "ContentDetailsWidget")]
    public partial class ContentDetailsWidgetController : CoreWidgetController
    {
        #region Properties

        public override String ControllerWidgetIdentifier
        {
            get { return ContentDetailsWidget.Instance.Identifier; }
        }

        #endregion

        #region Actions

        /// <summary>
        /// Views the widget.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        [ChildActionOnly]
        public virtual ActionResult ViewWidget(ICoreWidgetInstance instance)
        {
            if (instance != null)
            {
            }
            return Content("Content would be here");
        }

        #endregion

    }
}
