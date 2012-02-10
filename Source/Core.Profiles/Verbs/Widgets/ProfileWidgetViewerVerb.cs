using System;
using Core.Framework.Plugins.Web;

namespace Core.Profiles.Verbs.Widgets
{
    public class ProfileWidgetViewerVerb : IWidgetActionVerb
    {
        #region Singleton

        private static readonly Lazy<ProfileWidgetViewerVerb> instance = new Lazy<ProfileWidgetViewerVerb>(() => new ProfileWidgetViewerVerb());

        public static ProfileWidgetViewerVerb Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private ProfileWidgetViewerVerb()
        {
        }

        #endregion

        #region IWidgetActionVerb Members

        public String Action
        {
            get { return "ViewWidget"; }
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