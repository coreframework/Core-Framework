using System;
using Core.Framework.Plugins.Web;

namespace Core.News.Verbs.Widgets
{
    public class NewsWidgetEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static NewsWidgetEditorVerb instance;

        private static readonly Object SyncRoot = new Object();

        public static NewsWidgetEditorVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new NewsWidgetEditorVerb());
                }
            }
        }

        #endregion

        #region IWidgetActionVerb Members

        public String Action
        {
            get { return "EditWidget"; }
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