using System;
using Core.Framework.Plugins.Web;

namespace Core.News.Verbs.Widgets
{
    public class NewsWidgetSaveSettingsVerb : IWidgetActionVerb
    {
        #region Singleton

        private static NewsWidgetSaveSettingsVerb _instance;

        private static readonly Object SyncRoot = new Object();

        public static NewsWidgetSaveSettingsVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new NewsWidgetSaveSettingsVerb());
                }
            }
        }

        #endregion

        #region IWidgetActionVerb Members

        public string Action
        {
            get { return "UpdateWidget"; }
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