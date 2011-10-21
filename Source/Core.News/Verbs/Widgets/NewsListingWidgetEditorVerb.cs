using System;
using Core.Framework.Plugins.Web;

namespace Core.News.Verbs.Widgets
{
    public class NewsListingWidgetEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static NewsListingWidgetEditorVerb instance;

        private static readonly Object SyncRoot = new Object();

        public static NewsListingWidgetEditorVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new NewsListingWidgetEditorVerb());
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
            get { return "NewsListingWidget"; }
        }

        public String Area
        {
            get { return "News"; }
        }

        #endregion
    }
}