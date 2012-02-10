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
    public class NNavigationMenuWidget: BaseWidget
    {
        #region Singleton

        private static readonly Lazy<NNavigationMenuWidget> instance = new Lazy<NNavigationMenuWidget>(() => new NNavigationMenuWidget());

        public static NNavigationMenuWidget Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private NNavigationMenuWidget()
        {
        }

        #endregion

        public override ICorePlugin Plugin
        {
            get { return NavigationPlugin.Instance; }
        }

        public override IWidgetActionVerb ViewAction
        {
            get { return NavigationMenuViewerVerb.Instance; }
        }

        public override IWidgetActionVerb EditAction
        {
            get { return NavigationMenuEditorVerb.Instance; }
        }

        public override IWidgetActionVerb SaveAction
        {
            get { return NavigationMenuSaveVerb.Instance; }
        }

        public override long? Clone(ICoreWidgetInstance coreWidgetInstance)
        {
            return NavigationMenuWidgetHelper.CloneNavigationMenuWidget(coreWidgetInstance);
        }
    }
}