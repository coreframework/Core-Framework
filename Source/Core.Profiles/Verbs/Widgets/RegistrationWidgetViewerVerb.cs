using System;
using Core.Framework.Plugins.Web;

namespace Core.Profiles.Verbs.Widgets
{
    public class RegistrationWidgetViewerVerb : IWidgetActionVerb
    {
        #region Singleton

        private static readonly Lazy<RegistrationWidgetViewerVerb> instance = new Lazy<RegistrationWidgetViewerVerb>(() => new RegistrationWidgetViewerVerb());

        public static RegistrationWidgetViewerVerb Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private RegistrationWidgetViewerVerb()
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
            get { return "RegistrationWidget"; }
        }

        public String Area
        {
            get { return "Profiles"; }
        }

        #endregion
    }
}