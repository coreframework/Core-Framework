using System;
using Core.Framework.Plugins.Web;

namespace Core.News.Verbs.Widgets
{
    public class NewsWidgetEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static NewsWidgetEditorVerb _instance;

        private static readonly Object SyncRoot = new Object();

        public static NewsWidgetEditorVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new NewsWidgetEditorVerb());
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
            get { return "NewsViewerWidget"; }
        }

        public string Area
        {
            get { return "News"; }
        }

        #endregion
    }
}