using System;
using Core.Framework.Plugins.Web;

namespace Core.Languages.Verbs.Widgets
{
    public class LanguageSelectorWidgetViewerVerb : IWidgetActionVerb
    {
        #region Singleton

        private static LanguageSelectorWidgetViewerVerb _instance;

        private static readonly Object SyncRoot = new Object();

        public static LanguageSelectorWidgetViewerVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new LanguageSelectorWidgetViewerVerb());
                }
            }
        }

        #endregion

        #region IWidgetActionVerb Members

        public string Action
        {
            get { return "ViewWidget"; }
        }

        public string Controller
        {
            get { return "LanguageSelectorWidget"; }
        }

        public string Area
        {
            get { return "Languages"; }
        }

        #endregion
    }
}