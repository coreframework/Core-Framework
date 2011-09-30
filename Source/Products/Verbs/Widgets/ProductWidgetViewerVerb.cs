using System;
using Core.Framework.Plugins.Web;

namespace Products.Verbs.Widgets
{
    public class ProductWidgetViewerVerb: IWidgetActionVerb
    {
        #region Singleton

        private static ProductWidgetViewerVerb instance;

        private static readonly Object SyncRoot = new Object();

        public static ProductWidgetViewerVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new ProductWidgetViewerVerb());
                }
            }
        }

        #endregion

        #region IWidgetActionVerb Members

        public String Action
        {
            get { return "ViewWidget"; }
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