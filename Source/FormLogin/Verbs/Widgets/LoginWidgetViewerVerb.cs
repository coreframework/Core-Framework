using System;
using Core.Framework.Plugins.Web;

namespace Core.FormLogin.Verbs.Widgets
{
    public class LoginWidgetViewerVerb : IWidgetActionVerb
    {
        #region Singleton

        private static readonly Lazy<LoginWidgetViewerVerb> instance = new Lazy<LoginWidgetViewerVerb>(() => new LoginWidgetViewerVerb());

        public static LoginWidgetViewerVerb Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private LoginWidgetViewerVerb()
        {
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
            get { return "FormLogin"; }
        }

        #endregion
    }
}