using System;
using Core.Framework.Plugins.Web;

namespace Core.WebContent.Verbs.Widgets
{
    public class WebContentWidgetEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static readonly Lazy<WebContentWidgetEditorVerb> instance = new Lazy<WebContentWidgetEditorVerb>(() => new WebContentWidgetEditorVerb());

        public static WebContentWidgetEditorVerb Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private WebContentWidgetEditorVerb()
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
            get {return "WebContentWidget"; }
        }

        public String Area
        {
            get { return "WebContent"; }
        }

        #endregion
    }
}