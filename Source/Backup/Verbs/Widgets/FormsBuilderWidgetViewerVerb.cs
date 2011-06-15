using System;
using Core.Framework.Plugins.Web;

namespace Core.Forms.Verbs.Widgets
{
    public class FormsBuilderWidgetViewerVerb: IWidgetActionVerb
    {
        #region Singleton

        private static FormsBuilderWidgetViewerVerb _instance;

        private static readonly Object SyncRoot = new Object();

        public static FormsBuilderWidgetViewerVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new FormsBuilderWidgetViewerVerb());
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
            get { return "FormsBuilderWidget"; }
        }

        public string Area
        {
            get { return "Forms"; }
        }

        #endregion
    }
}