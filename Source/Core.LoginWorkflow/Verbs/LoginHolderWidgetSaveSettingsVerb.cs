using System;
using Core.Framework.Plugins.Web;

namespace Core.LoginWorkflow.Verbs
{
    public class LoginHolderWidgetSaveSettingsVerb : IWidgetActionVerb
    {
        #region Singleton

        private static readonly Lazy<LoginHolderWidgetSaveSettingsVerb> instance = new Lazy<LoginHolderWidgetSaveSettingsVerb>(() => new LoginHolderWidgetSaveSettingsVerb());

        public static LoginHolderWidgetSaveSettingsVerb Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private LoginHolderWidgetSaveSettingsVerb()
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
            get { return "LoginHolderWidget"; }
        }

        public String Area
        {
            get { return "LoginWorkflow"; }
        }

        #endregion
    }
}