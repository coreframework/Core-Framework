using System;
using System.ComponentModel.Composition;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;
using Core.Framework.Plugins.Widgets;
using Core.Web.Areas.Navigation.Helpers;
using Core.Web.Areas.Navigation.Verbs;

namespace Core.Web.Areas.Navigation.Widgets
{
    [Export(typeof(ICoreWidget))]
    [Export(typeof(IPermissible))]
    public class NBreadcrumbsWidget : BaseWidget
    {
        #region Singleton

        private static readonly Lazy<NBreadcrumbsWidget> instance = new Lazy<NBreadcrumbsWidget>(() => new NBreadcrumbsWidget());

        public static NBreadcrumbsWidget Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private NBreadcrumbsWidget()
        {
        }

        #endregion

        public override ICorePlugin Plugin
        {
            get { return NavigationPlugin.Instance; }
        }

        public override IWidgetActionVerb ViewAction
        {
            get { return BreadcrumbsViewerVerb.Instance; }
        }

        public override IWidgetActionVerb EditAction
        {
            get { return BreadcrumbsEditorVerb.Instance; }
        }

        public override IWidgetActionVerb SaveAction
        {
            get { return BreadcrumbsSaveVerb.Instance; }
        }

        public override long? Clone(ICoreWidgetInstance coreWidgetInstance)
        {
            return BreadcrumbsWidgetHelper.CloneBreadcrumbsWidget(coreWidgetInstance);
        }
    }
}