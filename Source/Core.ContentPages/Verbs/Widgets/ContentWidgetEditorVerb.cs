using System;
using Core.Framework.Plugins.Web;

namespace Core.ContentPages.Verbs.Widgets
{
    public class ContentWidgetEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static ContentWidgetEditorVerb instance;

        private static readonly Object SyncRoot = new Object();

        public static ContentWidgetEditorVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new ContentWidgetEditorVerb());
                }
            }
        }

        #endregion

        #region IWidgetActionVerb Members

        public String  Action
        {
            get { return "EditWidget"; }
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