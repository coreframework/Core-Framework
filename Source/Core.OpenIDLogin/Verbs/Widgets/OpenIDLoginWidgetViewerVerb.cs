using System;
using Core.Framework.Plugins.Web;

namespace Core.OpenIDLogin.Verbs.Widgets
{
    public class OpenIDLoginWidgetViewerVerb : IWidgetActionVerb
    {
        #region Singleton

        private static readonly Lazy<OpenIDLoginWidgetViewerVerb> instance = new Lazy<OpenIDLoginWidgetViewerVerb>(() => new OpenIDLoginWidgetViewerVerb());

        public static OpenIDLoginWidgetViewerVerb Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private OpenIDLoginWidgetViewerVerb()
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
            get { return "OpenIDLoginWidget"; }
        }

        public String Area
        {
            get { return "OpenIDLogin"; }
        }

        #endregion
    }
}