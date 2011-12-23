using System;
using Core.Framework.Plugins.Web;

namespace Core.OpenIDLogin.Verbs.Widgets
{
    public class OpenIDLoginWidgetSaveSettingsVerb : IWidgetActionVerb
    {
        #region Singleton

        private static OpenIDLoginWidgetSaveSettingsVerb instance;

        private static readonly Object syncRoot = new Object();

        public static OpenIDLoginWidgetSaveSettingsVerb Instance
        {
            get
            {
                lock (syncRoot)
                {
                    return instance ?? (instance = new OpenIDLoginWidgetSaveSettingsVerb());
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
            get { return "OpenIDLoginWidget"; }
        }

        public String Area
        {
            get { return "OpenIDLogin"; }
        }

        #endregion
    }
}