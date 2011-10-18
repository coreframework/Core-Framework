using System;
using Core.Framework.Plugins.Web;

namespace Core.News.Verbs.Widgets
{
    public class NewsCategoryWidgetEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static NewsCategoryWidgetEditorVerb instance;

        private static readonly Object SyncRoot = new Object();

        public static NewsCategoryWidgetEditorVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new NewsCategoryWidgetEditorVerb());
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
            get { return "NewsCategoryViewerWidget"; }
        }

        public String Area
        {
            get { return "News"; }
        }

        #endregion
    }
}