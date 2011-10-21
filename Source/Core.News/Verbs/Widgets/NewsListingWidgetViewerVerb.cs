using System;
using Core.Framework.Plugins.Web;

namespace Core.News.Verbs.Widgets
{
    public class NewsListingWidgetViewerVerb : IWidgetActionVerb
    {
        #region Singleton

        private static NewsListingWidgetViewerVerb instance;

        private static readonly Object SyncRoot = new Object();

        public static NewsListingWidgetViewerVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new NewsListingWidgetViewerVerb());
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
            get { return "NewsListingWidget"; }
        }

        public String Area
        {
            get { return "News"; }
        }

        #endregion
    }
}