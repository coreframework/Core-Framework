using System;
using Core.Framework.Plugins.Web;

namespace Core.WebContent.Verbs.Widgets
{
    public class WebContentDetailsWidgetViewerVerb : IWidgetActionVerb
    {
        #region Singleton

        private static WebContentDetailsWidgetViewerVerb instance;

        private static readonly Object syncRoot = new Object();

        public static WebContentDetailsWidgetViewerVerb Instance
        {
            get
            {
                lock (syncRoot)
                {
                    return instance ?? (instance = new WebContentDetailsWidgetViewerVerb());
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
            get { return "WebContentDetailsWidget"; }
        }

        public String Area
        {
            get { return "WebContent"; }
        }

        #endregion
    }
}