using System;
using Core.Framework.Plugins.Web;

namespace Products.Verbs.Widgets
{
    public class CategoryWidgetViewerVerb: IWidgetActionVerb
    {
        #region Singleton

        private static CategoryWidgetViewerVerb _instance;

        private static readonly Object SyncRoot = new Object();

        public static CategoryWidgetViewerVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new CategoryWidgetViewerVerb());
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
            get { return "CategoryViewerWidget"; }
        }

        public string Area
        {
            get { return "Product"; }
        }

        #endregion
    }
}