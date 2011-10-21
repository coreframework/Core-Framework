using System;
using System.ComponentModel.Composition;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;
using Core.Framework.Plugins.Widgets;
using Core.News.Verbs.Widgets;

namespace Core.News.Widgets
{
    [Export(typeof(ICoreWidget))]
    [Export(typeof(IPermissible))]
    public class NewsListingWidget : BaseWidget
    {
        #region Singleton

        private static NewsListingWidget instance;

        private static readonly Object SyncRoot = new Object();

        public static NewsListingWidget Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new NewsListingWidget());
                }
            }
        }

        #endregion

        public override ICorePlugin Plugin
        {
            get { return NewsPlugin.Instance; }
        }

        public override IWidgetActionVerb ViewAction
        {
            get { return NewsListingWidgetViewerVerb.Instance; }

        }
        public override IWidgetActionVerb EditAction
        {
            get { return NewsListingWidgetEditorVerb.Instance; }
        }

        public override IWidgetActionVerb SaveAction
        {
            get { return NewsListingWidgetSaveSettingsVerb.Instance; }
        }
    }
}