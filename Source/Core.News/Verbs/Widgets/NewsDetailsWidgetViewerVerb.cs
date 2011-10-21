using System;
using Core.Framework.Plugins.Web;

namespace Core.News.Verbs.Widgets
{
    public class NewsDetailsWidgetViewerVerb : IWidgetActionVerb
    {
        #region Singleton

        private static NewsDetailsWidgetViewerVerb instance;

        private static readonly Object SyncRoot = new Object();

        public static NewsDetailsWidgetViewerVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new NewsDetailsWidgetViewerVerb());
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
            get { return "NewsDetailsWidget"; }
        }

        public String Area
        {
            get { return "News"; }
        }

        #endregion
    }
}