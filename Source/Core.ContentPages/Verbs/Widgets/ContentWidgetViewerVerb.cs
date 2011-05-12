using System;
using Core.Framework.Plugins.Web;

namespace Core.ContentPages.Verbs.Widgets
{
    public class ContentWidgetViewerVerb: IWidgetActionVerb
    {
        #region Singleton

        private static ContentWidgetViewerVerb _instance;

        private static readonly Object SyncRoot = new Object();

        public static ContentWidgetViewerVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new ContentWidgetViewerVerb());
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
            get { return "ContentViewerWidget"; }
        }

        public string Area
        {
            get { return "ContentPage"; }
        }

        #endregion
    }
}