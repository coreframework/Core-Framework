using System;
using Core.Framework.Plugins.Web;

namespace Products.Verbs.Widgets
{
    public class ProductWidgetSaveSettingsVerb : IWidgetActionVerb
    {
        #region Singleton

        private static ProductWidgetSaveSettingsVerb _instance;

        private static readonly Object SyncRoot = new Object();

        public static ProductWidgetSaveSettingsVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new ProductWidgetSaveSettingsVerb());
                }
            }
        }

        #endregion

        #region IWidgetActionVerb Members

        public string Action
        {
            get { return "UpdateWidget"; }
        }

        public string Controller
        {
            get { return "ProductViewerWidget"; }
        }

        public string Area
        {
            get { return "Product"; }
        }

        #endregion
    }
}