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
    public class ContentDetailsWidget : BaseWidget
    {
        #region Singleton

        private static ContentDetailsWidget instance;

        private static readonly Object SyncRoot = new Object();

        public static ContentDetailsWidget Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new ContentDetailsWidget());
                }
            }
        }

        #endregion

        public override ICorePlugin Plugin
        {
            get { return ContentPagePlugin.Instance; }
        }

        public override IWidgetActionVerb ViewAction
        {
            get { return ContentDetailsWidgetViewerVerb.Instance; }

        }
        public override IWidgetActionVerb EditAction
        {
            get { return null; }
        }

        public override IWidgetActionVerb SaveAction
        {
            get { return null; }
        }

        public override bool IsDetailsWidget
        {
            get
            {
                return true;
            }
        }
    }
}