using System;
using Core.Framework.Plugins.Web;

namespace Core.OpenIDLogin.Verbs.Widgets
{
    public class OpenIDLoginWidgetSaveSettingsVerb : IWidgetActionVerb
    {
        #region Singleton

        private static readonly Lazy<OpenIDLoginWidgetSaveSettingsVerb> instance = new Lazy<OpenIDLoginWidgetSaveSettingsVerb>(() => new OpenIDLoginWidgetSaveSettingsVerb());

        public static OpenIDLoginWidgetSaveSettingsVerb Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private OpenIDLoginWidgetSaveSettingsVerb()
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
            get { return "OpenIDLoginWidget"; }
        }

        public String Area
        {
            get { return "OpenIDLogin"; }
        }

        #endregion
    }
}