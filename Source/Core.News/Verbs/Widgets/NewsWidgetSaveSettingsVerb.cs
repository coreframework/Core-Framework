using System;
using Core.Framework.Plugins.Web;

namespace Core.News.Verbs.Widgets
{
    public class NewsWidgetSaveSettingsVerb : IWidgetActionVerb
    {
        #region Singleton

        private static NewsWidgetSaveSettingsVerb instance;

        private static readonly Object SyncRoot = new Object();

        public static NewsWidgetSaveSettingsVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new NewsWidgetSaveSettingsVerb());
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
            get { return "NewsViewerWidget"; }
        }

        public String Area
        {
            get { return "News"; }
        }

        #endregion
    }
}