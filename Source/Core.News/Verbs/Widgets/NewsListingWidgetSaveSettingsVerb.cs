using System;
using Core.Framework.Plugins.Web;

namespace Core.News.Verbs.Widgets
{
    public class NewsListingWidgetSaveSettingsVerb : IWidgetActionVerb
    {
        #region Singleton

        private static NewsListingWidgetSaveSettingsVerb instance;

        private static readonly Object SyncRoot = new Object();

        public static NewsListingWidgetSaveSettingsVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new NewsListingWidgetSaveSettingsVerb());
                }
            }
        }

        #endregion

        #region IWidgetActionVerb Members

        public String Action
        {
            get { return "UpdateWidget"; }
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