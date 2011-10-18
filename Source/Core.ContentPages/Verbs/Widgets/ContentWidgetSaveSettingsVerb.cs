using System;
using Core.Framework.Plugins.Web;

namespace Core.ContentPages.Verbs.Widgets
{
    public class ContentWidgetSaveSettingsVerb : IWidgetActionVerb
    {
        #region Singleton

        private static ContentWidgetSaveSettingsVerb instance;

        private static readonly Object SyncRoot = new Object();

        public static ContentWidgetSaveSettingsVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new ContentWidgetSaveSettingsVerb());
                }
            }
        }

        #endregion

        #region IWidgetActionVerb Members

        public String  Action
        {
            get { return "UpdateWidget"; }
        }

        public String  Controller
        {
            get {return "ContentViewerWidget"; }
        }

        public String  Area
        {
            get { return "ContentPage"; }
        }

        #endregion
    }
}