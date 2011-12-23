using System;
using Core.Framework.Plugins.Web;

namespace Core.OpenIDLogin.Verbs.Widgets
{
    public class OpenIDLoginWidgetEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static OpenIDLoginWidgetEditorVerb instance;

        private static readonly Object syncRoot = new Object();

        public static OpenIDLoginWidgetEditorVerb Instance
        {
            get
            {
                lock (syncRoot)
                {
                    return instance ?? (instance = new OpenIDLoginWidgetEditorVerb());
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
            get { return "OpenIDLoginWidget"; }
        }

        public String Area
        {
            get { return "OpenIDLogin"; }
        }

        #endregion
    }
}