using System;
using Core.Framework.Plugins.Web;

namespace Core.WebContent.Verbs.Widgets
{
    public class WebContentWidgetViewerVerb : IWidgetActionVerb
    {
        #region Singleton

        private static readonly Lazy<WebContentWidgetViewerVerb> instance = new Lazy<WebContentWidgetViewerVerb>(() => new WebContentWidgetViewerVerb());

        public static WebContentWidgetViewerVerb Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private WebContentWidgetViewerVerb()
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
            get { return "WebContentWidget"; }
        }

        public String Area
        {
            get { return "WebContent"; }
        }

        #endregion
    }
}