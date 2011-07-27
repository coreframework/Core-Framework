using System;
using System.ComponentModel.Composition;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;
using Core.Framework.Plugins.Widgets;
using Products.Verbs.Widgets;

namespace Products.Widgets
{
    [Export(typeof(ICoreWidget))]
    [Export(typeof(IPermissible))]
    public class CategoryViewerWidget : BaseWidget
    {
        #region Singleton

        private static CategoryViewerWidget _instance;

        private static readonly Object SyncRoot = new Object();

        public static CategoryViewerWidget Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new CategoryViewerWidget());
                }
            }
        }

        #endregion

        public override ICorePlugin Plugin
        {
            get { return ProductPlugin.Instance; }
        }

        public override IWidgetActionVerb ViewAction
        {
            get { return CategoryWidgetViewerVerb.Instance; }
           
        }
        public override IWidgetActionVerb EditAction
        {
            get { return CategoryWidgetEditorVerb.Instance; }
        }

        public override IWidgetActionVerb SaveAction
        {
            get { return CategoryWidgetSaveSettingsVerb.Instance; }
        }
    }
}
