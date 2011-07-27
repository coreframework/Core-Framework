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
    public class NewsViewerWidget : BaseWidget
    {
        #region Singleton

        private static NewsViewerWidget _instance;

        private static readonly Object SyncRoot = new Object();

        public static NewsViewerWidget Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new NewsViewerWidget());
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
            get { return NewsWidgetViewerVerb.Instance; }

        }
        public override IWidgetActionVerb EditAction
        {
            get { return NewsWidgetEditorVerb.Instance; }
        }

        public override IWidgetActionVerb SaveAction
        {
            get { return NewsWidgetSaveSettingsVerb.Instance; }
        }
    }
}