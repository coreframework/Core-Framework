using System;
using Core.Framework.Plugins.Web;

namespace Core.News.Verbs.Widgets
{
    public class NewsWidgetViewerVerb : IWidgetActionVerb
    {
        #region Singleton

        private static NewsWidgetViewerVerb instance;

        private static readonly Object SyncRoot = new Object();

        public static NewsWidgetViewerVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new NewsWidgetViewerVerb());
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
            get { return "NewsViewerWidget"; }
        }

        public String Area
        {
            get { return "News"; }
        }

        #endregion
    }
}