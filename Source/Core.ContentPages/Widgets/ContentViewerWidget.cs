using System;
using System.ComponentModel.Composition;
using Core.ContentPages.Verbs.Widgets;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;
using Core.Framework.Plugins.Widgets;

namespace Core.ContentPages.Widgets
{
    [Export(typeof(ICoreWidget))]
    [Export(typeof(IPermissible))]
    public class ContentViewerWidget : BaseWidget
    {
        #region Singleton

        private static ContentViewerWidget _instance;

        private static readonly Object SyncRoot = new Object();

        public static ContentViewerWidget Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new ContentViewerWidget());
                }
            }
        }

        #endregion

        public override string Title
        {
            get { return "Web Content"; }
        }

        public override ICorePlugin Plugin
        {
            get { return ContentPagePlugin.Instance; }
        }

        public override string Identifier
        {
            get { return "123456"; }
        }

        public override IWidgetActionVerb ViewAction
        {
            get { return ContentWidgetViewerVerb.Instance; }
           
        }
        public override IWidgetActionVerb EditAction
        {
            get { return ContentWidgetEditorVerb.Instance; }
        }

        public override IWidgetActionVerb SaveAction
        {
            get { return ContentWidgetSaveSettingsVerb.Instance; }
        }
    }
}
