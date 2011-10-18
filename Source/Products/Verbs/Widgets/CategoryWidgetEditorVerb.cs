using System;
using Core.Framework.Plugins.Web;

namespace Products.Verbs.Widgets
{
    public class CategoryWidgetEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static CategoryWidgetEditorVerb instance;

        private static readonly Object SyncRoot = new Object();

        public static CategoryWidgetEditorVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new CategoryWidgetEditorVerb());
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
            get { return "CategoryViewerWidget"; }
        }

        public String Area
        {
            get { return "Product"; }
        }

        #endregion
    }
}