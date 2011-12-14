using System;
using Core.Framework.Plugins.Web;

namespace Core.Profiles.Verbs.Widgets
{
    public class ProfileWidgetViewerVerb : IWidgetActionVerb
    {
        #region Singleton

        private static ProfileWidgetViewerVerb instance;

        private static readonly Object syncRoot = new Object();

        public static ProfileWidgetViewerVerb Instance
        {
            get
            {
                lock (syncRoot)
                {
                    return instance ?? (instance = new ProfileWidgetViewerVerb());
                }
            }
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