using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Framework.Plugins.Web;

namespace Core.WebContent.Verbs.Widgets
{
    public class WebContentDetailsWidgetSaveSettingsVerb : IWidgetActionVerb
    {
        #region Singleton

        private static WebContentDetailsWidgetSaveSettingsVerb instance;

        private static readonly Object syncRoot = new Object();

        public static WebContentDetailsWidgetSaveSettingsVerb Instance
        {
            get
            {
                lock (syncRoot)
                {
                    return instance ?? (instance = new WebContentDetailsWidgetSaveSettingsVerb());
                }
            }
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