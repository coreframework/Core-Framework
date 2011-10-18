using System;
using Core.Framework.Plugins.Web;

namespace Core.News.Verbs.Widgets
{
    public class NewsCategoryWidgetSaveSettingsVerb : IWidgetActionVerb
    {
        #region Singleton

        private static NewsCategoryWidgetSaveSettingsVerb instance;

        private static readonly Object SyncRoot = new Object();

        public static NewsCategoryWidgetSaveSettingsVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new NewsCategoryWidgetSaveSettingsVerb());
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
            get { return "NewsCategoryViewerWidget"; }
        }

        public String Area
        {
            get { return "News"; }
        }

        #endregion
    }
}