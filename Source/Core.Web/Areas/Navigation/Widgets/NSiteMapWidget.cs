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
    public class NSiteMapWidget: BaseWidget
    {
        #region Singleton

        private static NSiteMapWidget _instance;

        private static readonly Object SyncRoot = new Object();

        public static NSiteMapWidget Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new NSiteMapWidget());
                }
            }
        }

        #endregion

        public override ICorePlugin Plugin
        {
            get {return NavigationPlugin.Instance; }
        }

        public override IWidgetActionVerb ViewAction
        {
            get { return SiteMapViewerVerb.Instance; }

        }

        public override IWidgetActionVerb EditAction
        {
            get { return SiteMapEditorVerb.Instance; }
        }

        public override IWidgetActionVerb SaveAction
        {
            get { return SiteMapSaveVerb.Instance; }
        }

        public override long? Clone(ICoreWidgetInstance coreWidgetInstance)
        {
            return SiteMapWidgetHelper.CloneSiteMapWidget(coreWidgetInstance);
        }
    }
}