using System;
using Core.Framework.Plugins.Web;

namespace Products.Verbs.Widgets
{
    public class ProductWidgetSaveSettingsVerb : IWidgetActionVerb
    {
        #region Singleton

        private static ProductWidgetSaveSettingsVerb instance;

        private static readonly Object SyncRoot = new Object();

        public static ProductWidgetSaveSettingsVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new ProductWidgetSaveSettingsVerb());
                }
            }
        }

        #endregion

        #region IWidgetActionVerb Members

        public String Action
        {
            get { return "UpdateWidget"; }
        }

        public String Controller
        {
            get { return "ProductViewerWidget"; }
        }

        public String Area
        {
            get { return "Product"; }
        }

        #endregion
    }
}