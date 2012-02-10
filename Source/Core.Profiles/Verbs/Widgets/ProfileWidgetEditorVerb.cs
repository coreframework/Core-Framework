using System;
using Core.Framework.Plugins.Web;

namespace Core.Profiles.Verbs.Widgets
{
    public class ProfileWidgetEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static readonly Lazy<ProfileWidgetEditorVerb> instance = new Lazy<ProfileWidgetEditorVerb>(() => new ProfileWidgetEditorVerb());

        public static ProfileWidgetEditorVerb Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private ProfileWidgetEditorVerb()
        {
        }

        #endregion

        #region IWidgetActionVerb Members

        public String Action
        {
            get { return "EditWidget"; }
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