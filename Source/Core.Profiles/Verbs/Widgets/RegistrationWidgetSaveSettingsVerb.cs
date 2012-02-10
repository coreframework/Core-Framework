using System;
using Core.Framework.Plugins.Web;

namespace Core.Profiles.Verbs.Widgets
{
    public class RegistrationWidgetSaveSettingsVerb : IWidgetActionVerb
    {
        #region Singleton

        private static readonly Lazy<RegistrationWidgetSaveSettingsVerb> instance = new Lazy<RegistrationWidgetSaveSettingsVerb>(() => new RegistrationWidgetSaveSettingsVerb());

        public static RegistrationWidgetSaveSettingsVerb Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private RegistrationWidgetSaveSettingsVerb()
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
            get { return "RegistrationWidget"; }
        }

        public String Area
        {
            get { return "Profiles"; }
        }

        #endregion
    }
}