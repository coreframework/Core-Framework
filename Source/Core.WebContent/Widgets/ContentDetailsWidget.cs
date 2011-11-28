using System;
using System.ComponentModel.Composition;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;
using Core.Framework.Plugins.Widgets;
using Core.WebContent.Verbs.Widgets;

namespace Core.WebContent.Widgets
{
    [Export(typeof(ICoreWidget))]
    [Export(typeof(IPermissible))]
    public class ContentDetailsWidget : BaseWidget
    {
        #region Singleton

        private static ContentDetailsWidget instance;

        private static readonly Object syncRoot = new Object();

        public static ContentDetailsWidget Instance
        {
            get
            {
                lock (syncRoot)
                {
                    return instance ?? (instance = new ContentDetailsWidget());
                }
            }
        }

        private ContentDetailsWidget()
        {
            
        }

        #endregion

        public override ICorePlugin Plugin
        {
            get { return WebContentPlugin.Instance; }
        }

        public override IWidgetActionVerb ViewAction
        {
            get { return WebContentDetailsWidgetViewerVerb.Instance; }

        }
        public override IWidgetActionVerb EditAction
        {
            get { return WebContentDetailsWidgetEditorVerb.Instance; }
        }

        public override IWidgetActionVerb SaveAction
        {
            get { return WebContentDetailsWidgetSaveSettingsVerb.Instance; }
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