using System;
using Core.Framework.Plugins.Web;

namespace Core.LoginWorkflow.Verbs
{
    public class LoginHolderWidgetSaveSettingsVerb : IWidgetActionVerb
    {
        #region Singleton

        private static LoginHolderWidgetSaveSettingsVerb instance;

        private static readonly Object syncRoot = new Object();

        public static LoginHolderWidgetSaveSettingsVerb Instance
        {
            get
            {
                lock (syncRoot)
                {
                    return instance ?? (instance = new LoginHolderWidgetSaveSettingsVerb());
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
            get { return "LoginHolderWidget"; }
        }

        public String Area
        {
            get { return "LoginWorkflow"; }
        }

        #endregion
    }
}