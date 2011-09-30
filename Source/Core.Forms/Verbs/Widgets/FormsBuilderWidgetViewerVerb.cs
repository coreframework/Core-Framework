using System;
using Core.Framework.Plugins.Web;

namespace Core.Forms.Verbs.Widgets
{
    public class FormsBuilderWidgetViewerVerb : IWidgetActionVerb
    {
        #region Singleton

        private static FormsBuilderWidgetViewerVerb instance;

        private static readonly Object syncRoot = new Object();

        public static FormsBuilderWidgetViewerVerb Instance
        {
            get
            {
                lock (syncRoot)
                {
                    return instance ?? (instance = new FormsBuilderWidgetViewerVerb());
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
            get { return "FormsBuilderWidget"; }
        }

        public String Area
        {
            get { return "Forms"; }
        }

        #endregion
    }
}