using System;
using Core.Framework.Plugins.Web;

namespace Core.ContentPages.Verbs.Widgets
{
    public class ContentWidgetEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static ContentWidgetEditorVerb _instance;

        private static readonly Object SyncRoot = new Object();

        public static ContentWidgetEditorVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new ContentWidgetEditorVerb());
                }
            }
        }

        #endregion

        #region IWidgetActionVerb Members

        public string Action
        {
            get { return "EditWidget"; }
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