using System;
using Core.Framework.Plugins.Web;

namespace Core.Profiles.Verbs.Widgets
{
    public class ProfileWidgetSaveSettingsVerb : IWidgetActionVerb
    {
        #region Singleton

        private static readonly Lazy<ProfileWidgetSaveSettingsVerb> instance = new Lazy<ProfileWidgetSaveSettingsVerb>(() => new ProfileWidgetSaveSettingsVerb());

        public static ProfileWidgetSaveSettingsVerb Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private ProfileWidgetSaveSettingsVerb()
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
            get { return "ProfileWidget"; }
        }

        public String Area
        {
            get { return "Profiles"; }
        }

        #endregion
    }
}