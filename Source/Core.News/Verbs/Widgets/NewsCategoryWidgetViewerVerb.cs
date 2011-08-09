using System;
using Core.Framework.Plugins.Web;

namespace Core.News.Verbs.Widgets
{
    public class NewsCategoryWidgetViewerVerb: IWidgetActionVerb
    {
        #region Singleton

        private static NewsCategoryWidgetViewerVerb _instance;

        private static readonly Object SyncRoot = new Object();

        public static NewsCategoryWidgetViewerVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new NewsCategoryWidgetViewerVerb());
                }
            }
        }

        #endregion

        #region IWidgetActionVerb Members

        public string Action
        {
            get { return "ViewWidget"; }
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