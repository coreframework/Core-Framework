using System;
using Core.Framework.Plugins.Web;

namespace Core.WebContent.Verbs.Widgets
{
    public class WebContentDetailsWidgetEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static WebContentDetailsWidgetEditorVerb instance;

        private static readonly Object syncRoot = new Object();

        public static WebContentDetailsWidgetEditorVerb Instance
        {
            get
            {
                lock (syncRoot)
                {
                    return instance ?? (instance = new WebContentDetailsWidgetEditorVerb());
                }
            }
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