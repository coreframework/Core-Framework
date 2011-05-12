using System;
using Core.Framework.Plugins.Web;

namespace Core.ContentPages.Verbs.Widgets
{
    public class ContentWidgetSaveSettingsVerb : IWidgetActionVerb
    {
        #region Singleton

        private static ContentWidgetSaveSettingsVerb _instance;

        private static readonly Object SyncRoot = new Object();

        public static ContentWidgetSaveSettingsVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new ContentWidgetSaveSettingsVerb());
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
            get {return "ContentViewerWidget"; }
        }

        public string Area
        {
            get { return "ContentPage"; }
        }

        #endregion
    }
}