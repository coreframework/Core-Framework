using System;
using Core.Framework.Plugins.Web;

namespace Core.ContentPages.Verbs.Widgets
{
    public class ContentWidgetViewerVerb: IWidgetActionVerb
    {
        #region Singleton

        private static ContentWidgetViewerVerb instance;

        private static readonly Object syncRoot = new Object();

        public static ContentWidgetViewerVerb Instance
        {
            get
            {
                lock (syncRoot)
                {
                    return instance ?? (instance = new ContentWidgetViewerVerb());
                }
            }
        }

        #endregion

        #region IWidgetActionVerb Members

        public String  Action
        {
            get { return "ViewWidget"; }
        }

        public String  Controller
        {
            get { return "ContentViewerWidget"; }
        }

        public String  Area
        {
            get { return "ContentPage"; }
        }

        #endregion
    }
}