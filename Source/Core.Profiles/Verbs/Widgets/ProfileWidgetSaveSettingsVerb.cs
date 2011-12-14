using System;
using Core.Framework.Plugins.Web;

namespace Core.Profiles.Verbs.Widgets
{
    public class ProfileWidgetSaveSettingsVerb : IWidgetActionVerb
    {
        #region Singleton

        private static ProfileWidgetSaveSettingsVerb instance;

        private static readonly Object syncRoot = new Object();

        public static ProfileWidgetSaveSettingsVerb Instance
        {
            get
            {
                lock (syncRoot)
                {
                    return instance ?? (instance = new ProfileWidgetSaveSettingsVerb());
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
            get { return "ProfileWidget"; }
        }

        public String Area
        {
            get { return "Profiles"; }
        }

        #endregion
    }
}