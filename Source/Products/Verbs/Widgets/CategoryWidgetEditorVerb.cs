using System;
using Core.Framework.Plugins.Web;

namespace Products.Verbs.Widgets
{
    public class CategoryWidgetEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static CategoryWidgetEditorVerb _instance;

        private static readonly Object SyncRoot = new Object();

        public static CategoryWidgetEditorVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new CategoryWidgetEditorVerb());
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
            get { return "CategoryViewerWidget"; }
        }

        public string Area
        {
            get { return "Product"; }
        }

        #endregion
    }
}