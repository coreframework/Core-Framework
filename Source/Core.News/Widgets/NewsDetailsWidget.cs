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
    public class NewsDetailsWidget : BaseWidget
    {
        #region Singleton

        private static NewsDetailsWidget instance;

        private static readonly Object SyncRoot = new Object();

        public static NewsDetailsWidget Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new NewsDetailsWidget());
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
            get { return NewsDetailsWidgetViewerVerb.Instance; }
        }
        public override IWidgetActionVerb EditAction
        {
            get { return NewsDetailsWidgetEditorVerb.Instance; }
        }

        public override IWidgetActionVerb SaveAction
        {
            get { return NewsDetailsWidgetSaveSettingsVerb.Instance; }
        }

        public override bool IsDetailsWidget
        {
            get { return true; }
        }
    }
}