using System;
using Core.Framework.Plugins.Web;

namespace Core.News.Verbs.Widgets
{
    public class NewsCategoryWidgetEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static NewsCategoryWidgetEditorVerb _instance;

        private static readonly Object SyncRoot = new Object();

        public static NewsCategoryWidgetEditorVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new NewsCategoryWidgetEditorVerb());
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
            get { return "NewsCategoryViewerWidget"; }
        }

        public string Area
        {
            get { return "News"; }
        }

        #endregion
    }
}