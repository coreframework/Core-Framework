using System;
using Core.Framework.Plugins.Web;

namespace Core.Profiles.Verbs.Widgets
{
    public class LoginWidgetViewerVerb : IWidgetActionVerb
    {
        #region Singleton

        private static LoginWidgetViewerVerb instance;

        private static readonly Object syncRoot = new Object();

        public static LoginWidgetViewerVerb Instance
        {
            get
            {
                lock (syncRoot)
                {
                    return instance ?? (instance = new LoginWidgetViewerVerb());
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
            get { return "LoginWidget"; }
        }

        public String Area
        {
            get { return "Profiles"; }
        }

        #endregion
    }
}