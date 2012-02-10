using System;
using Core.Framework.Plugins.Web;

namespace Core.FormLogin.Verbs.Widgets
{
    public class LoginWidgetSaveSettingsVerb : IWidgetActionVerb
    {
        #region Singleton

        private static readonly Lazy<LoginWidgetSaveSettingsVerb> instance = new Lazy<LoginWidgetSaveSettingsVerb>(() => new LoginWidgetSaveSettingsVerb());

        public static LoginWidgetSaveSettingsVerb Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private LoginWidgetSaveSettingsVerb()
        {
        }

        #endregion

        #region IWidgetActionVerb Members

        public String Action
        {
            get { return "UpdateWidget"; }
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