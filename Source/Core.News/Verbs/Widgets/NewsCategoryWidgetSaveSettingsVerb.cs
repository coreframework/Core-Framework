using System;
using Core.Framework.Plugins.Web;

namespace Core.News.Verbs.Widgets
{
    public class NewsCategoryWidgetSaveSettingsVerb : IWidgetActionVerb
    {
        #region Singleton

        private static NewsCategoryWidgetSaveSettingsVerb _instance;

        private static readonly Object SyncRoot = new Object();

        public static NewsCategoryWidgetSaveSettingsVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new NewsCategoryWidgetSaveSettingsVerb());
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
            get { return "NewsCategoryViewerWidget"; }
        }

        public string Area
        {
            get { return "News"; }
        }

        #endregion
    }
}