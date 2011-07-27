using System;
using Core.Framework.Plugins.Web;

namespace Core.News.Verbs.Widgets
{
    public class NewsWidgetViewerVerb : IWidgetActionVerb
    {
        #region Singleton

        private static NewsWidgetViewerVerb _instance;

        private static readonly Object SyncRoot = new Object();

        public static NewsWidgetViewerVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new NewsWidgetViewerVerb());
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
            get { return "NewsViewerWidget"; }
        }

        public string Area
        {
            get { return "News"; }
        }

        #endregion
    }
}