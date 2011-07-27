using System;
using Core.Framework.Plugins.Web;

namespace Products.Verbs.Widgets
{
    public class ProductWidgetViewerVerb: IWidgetActionVerb
    {
        #region Singleton

        private static ProductWidgetViewerVerb _instance;

        private static readonly Object SyncRoot = new Object();

        public static ProductWidgetViewerVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new ProductWidgetViewerVerb());
                }
            }
        }

        #endregion

        #region IWidgetActionVerb Members

        public string Action
        {
            get { return "ViewWidget"; }
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