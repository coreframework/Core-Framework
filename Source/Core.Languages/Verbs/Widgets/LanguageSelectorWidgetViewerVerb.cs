using System;
using Core.Framework.Plugins.Web;

namespace Core.Languages.Verbs.Widgets
{
    public class LanguageSelectorWidgetViewerVerb : IWidgetActionVerb
    {
        #region Singleton

        private static readonly Lazy<LanguageSelectorWidgetViewerVerb> instance = new Lazy<LanguageSelectorWidgetViewerVerb>(() => new LanguageSelectorWidgetViewerVerb());

        public static LanguageSelectorWidgetViewerVerb Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private LanguageSelectorWidgetViewerVerb()
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
            get { return "LanguageSelectorWidget"; }
        }

        public String Area
        {
            get { return "Languages"; }
        }

        #endregion
    }
}