using System;
using Core.Framework.Plugins.Web;

namespace Core.News.Verbs.Widgets
{
    public class NewsDetailsWidgetEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static NewsDetailsWidgetEditorVerb instance;

        private static readonly Object SyncRoot = new Object();

        public static NewsDetailsWidgetEditorVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new NewsDetailsWidgetEditorVerb());
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
            get { return "NewsDetailsWidget"; }
        }

        public String Area
        {
            get { return "News"; }
        }

        #endregion
    }
}