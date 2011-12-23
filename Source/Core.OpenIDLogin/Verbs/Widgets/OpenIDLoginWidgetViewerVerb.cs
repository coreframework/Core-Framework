using System;
using Core.Framework.Plugins.Web;

namespace Core.OpenIDLogin.Verbs.Widgets
{
    public class OpenIDLoginWidgetViewerVerb : IWidgetActionVerb
    {
        #region Singleton

        private static OpenIDLoginWidgetViewerVerb instance;

        private static readonly Object syncRoot = new Object();

        public static OpenIDLoginWidgetViewerVerb Instance
        {
            get
            {
                lock (syncRoot)
                {
                    return instance ?? (instance = new OpenIDLoginWidgetViewerVerb());
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
            get { return "OpenIDLoginWidget"; }
        }

        public String Area
        {
            get { return "OpenIDLogin"; }
        }

        #endregion
    }
}