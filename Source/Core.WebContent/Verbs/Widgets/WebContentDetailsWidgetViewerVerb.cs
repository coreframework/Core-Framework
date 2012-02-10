using System;
using Core.Framework.Plugins.Web;

namespace Core.WebContent.Verbs.Widgets
{
    public class WebContentDetailsWidgetViewerVerb : IWidgetActionVerb
    {
        #region Singleton

        private static readonly Lazy<WebContentDetailsWidgetViewerVerb> instance = new Lazy<WebContentDetailsWidgetViewerVerb>(() => new WebContentDetailsWidgetViewerVerb());

        public static WebContentDetailsWidgetViewerVerb Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private WebContentDetailsWidgetViewerVerb()
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
            get { return "WebContentDetailsWidget"; }
        }

        public String Area
        {
            get { return "WebContent"; }
        }

        #endregion
    }
}