using System;
using Core.Framework.Plugins.Web;

namespace Core.Profiles.Verbs.Widgets
{
    public class ProfileWidgetEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static ProfileWidgetEditorVerb instance;

        private static readonly Object syncRoot = new Object();

        public static ProfileWidgetEditorVerb Instance
        {
            get
            {
                lock (syncRoot)
                {
                    return instance ?? (instance = new ProfileWidgetEditorVerb());
                }
            }
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