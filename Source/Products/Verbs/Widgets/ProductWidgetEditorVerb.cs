using System;
using Core.Framework.Plugins.Web;

namespace Products.Verbs.Widgets
{
    public class ProductWidgetEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static ProductWidgetEditorVerb instance;

        private static readonly Object SyncRoot = new Object();

        public static ProductWidgetEditorVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new ProductWidgetEditorVerb());
                }
            }
        }

        #endregion

        #region IWidgetActionVerb Members

        public String Action
        {
            get { return "EditWidget"; }
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