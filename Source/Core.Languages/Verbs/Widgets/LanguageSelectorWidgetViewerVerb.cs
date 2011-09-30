using System;
using Core.Framework.Plugins.Web;

namespace Core.Languages.Verbs.Widgets
{
    public class LanguageSelectorWidgetViewerVerb : IWidgetActionVerb
    {
        #region Singleton

        private static LanguageSelectorWidgetViewerVerb instance;

        private static readonly Object SyncRoot = new Object();

        public static LanguageSelectorWidgetViewerVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new LanguageSelectorWidgetViewerVerb());
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
            get { return "LanguageSelectorWidget"; }
        }

        public String Area
        {
            get { return "Languages"; }
        }

        #endregion
    }
}