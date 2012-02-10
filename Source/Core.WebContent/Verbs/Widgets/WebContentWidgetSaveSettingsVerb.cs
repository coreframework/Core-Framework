using System;
using Core.Framework.Plugins.Web;

namespace Core.WebContent.Verbs.Widgets
{
    public class WebContentWidgetSaveSettingsVerb : IWidgetActionVerb
    {
        #region Singleton

        private static readonly Lazy<WebContentWidgetSaveSettingsVerb> instance = new Lazy<WebContentWidgetSaveSettingsVerb>(() => new WebContentWidgetSaveSettingsVerb());

        public static WebContentWidgetSaveSettingsVerb Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private WebContentWidgetSaveSettingsVerb()
        {
        }

        #endregion

        #region IWidgetActionVerb Members

        public String Action
        {
            get { return "UpdateWidget"; }
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