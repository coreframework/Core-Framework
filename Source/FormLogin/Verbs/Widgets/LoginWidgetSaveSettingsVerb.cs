using System;
using Core.Framework.Plugins.Web;

namespace Core.FormLogin.Verbs.Widgets
{
    public class LoginWidgetSaveSettingsVerb : IWidgetActionVerb
    {
        #region Singleton

        private static LoginWidgetSaveSettingsVerb instance;

        private static readonly Object syncRoot = new Object();

        public static LoginWidgetSaveSettingsVerb Instance
        {
            get
            {
                lock (syncRoot)
                {
                    return instance ?? (instance = new LoginWidgetSaveSettingsVerb());
                }
            }
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