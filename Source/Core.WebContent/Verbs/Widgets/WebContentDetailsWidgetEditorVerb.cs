using System;
using Core.Framework.Plugins.Web;

namespace Core.WebContent.Verbs.Widgets
{
    public class WebContentDetailsWidgetEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static readonly Lazy<WebContentDetailsWidgetEditorVerb> instance = new Lazy<WebContentDetailsWidgetEditorVerb>(() => new WebContentDetailsWidgetEditorVerb());

        public static WebContentDetailsWidgetEditorVerb Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private WebContentDetailsWidgetEditorVerb()
        {
        }

        #endregion

        #region IWidgetActionVerb Members

        public String Action
        {
            get { return "EditWidget"; }
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