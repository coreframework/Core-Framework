using System;
using System.ComponentModel.Composition;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;
using Core.Framework.Plugins.Widgets;
using Products.Helpers;
using Products.Verbs.Widgets;

namespace Products.Widgets
{
    [Export(typeof(ICoreWidget))]
    [Export(typeof(IPermissible))]
    public class ProductViewerWidget : BaseWidget
    {
        #region Singleton

        private static ProductViewerWidget _instance;

        private static readonly Object SyncRoot = new Object();

        public static ProductViewerWidget Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new ProductViewerWidget());
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
            get { return ProductWidgetViewerVerb.Instance; }
           
        }
        public override IWidgetActionVerb EditAction
        {
            get { return ProductWidgetEditorVerb.Instance; }
        }

        public override IWidgetActionVerb SaveAction
        {
            get { return ProductWidgetSaveSettingsVerb.Instance; }
        }

        public override long? Clone(ICoreWidgetInstance coreWidgetInstance)
        {
            return ProductViewerWidgetHelper.CloneProductViewerWidget(coreWidgetInstance);
        }
    }
}
