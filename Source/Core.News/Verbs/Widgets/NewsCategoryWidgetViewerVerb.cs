using System;
using Core.Framework.Plugins.Web;

namespace Core.News.Verbs.Widgets
{
    public class NewsCategoryWidgetViewerVerb: IWidgetActionVerb
    {
        #region Singleton

        private static NewsCategoryWidgetViewerVerb instance;

        private static readonly Object SyncRoot = new Object();

        public static NewsCategoryWidgetViewerVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new NewsCategoryWidgetViewerVerb());
                }
            }
        }

        #endregion

        #region IWidgetActionVerb Members

        public String Action
        {
            get { return "ViewWidget"; }
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