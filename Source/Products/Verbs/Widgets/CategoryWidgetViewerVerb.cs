using System;
using Core.Framework.Plugins.Web;

namespace Products.Verbs.Widgets
{
    public class CategoryWidgetViewerVerb: IWidgetActionVerb
    {
        #region Singleton

        private static CategoryWidgetViewerVerb instance;

        private static readonly Object syncRoot = new Object();

        public static CategoryWidgetViewerVerb Instance
        {
            get
            {
                lock (syncRoot)
                {
                    return instance ?? (instance = new CategoryWidgetViewerVerb());
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
            get { return "CategoryViewerWidget"; }
        }

        public String Area
        {
            get { return "Product"; }
        }

        #endregion
    }
}