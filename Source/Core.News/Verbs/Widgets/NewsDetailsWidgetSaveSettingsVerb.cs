using System;
using Core.Framework.Plugins.Web;

namespace Core.News.Verbs.Widgets
{
    public class NewsDetailsWidgetSaveSettingsVerb : IWidgetActionVerb
    {
        #region Singleton

        private static NewsDetailsWidgetSaveSettingsVerb instance;

        private static readonly Object SyncRoot = new Object();

        public static NewsDetailsWidgetSaveSettingsVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new NewsDetailsWidgetSaveSettingsVerb());
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
            get { return "NewsDetailsWidget"; }
        }

        public String Area
        {
            get { return "News"; }
        }

        #endregion
    }
}