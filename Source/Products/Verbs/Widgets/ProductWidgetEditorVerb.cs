using System;
using Core.Framework.Plugins.Web;

namespace Products.Verbs.Widgets
{
    public class ProductWidgetEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static ProductWidgetEditorVerb _instance;

        private static readonly Object SyncRoot = new Object();

        public static ProductWidgetEditorVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new ProductWidgetEditorVerb());
                }
            }
        }

        #endregion

        #region IWidgetActionVerb Members

        public string Action
        {
            get { return "EditWidget"; }
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