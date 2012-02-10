using System;
using Core.Framework.Plugins.Web;

namespace Core.WebContent.Verbs.Widgets
{
    public class WebContentDetailsWidgetSaveSettingsVerb : IWidgetActionVerb
    {
        #region Singleton

        private static readonly Lazy<WebContentDetailsWidgetSaveSettingsVerb> instance = new Lazy<WebContentDetailsWidgetSaveSettingsVerb>(() => new WebContentDetailsWidgetSaveSettingsVerb());

        public static WebContentDetailsWidgetSaveSettingsVerb Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private WebContentDetailsWidgetSaveSettingsVerb()
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
            get { return "WebContentDetailsWidget"; }
        }

        public String Area
        {
            get { return "WebContent"; }
        }

        #endregion
    }
}