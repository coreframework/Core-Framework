using System;
using Core.Framework.Plugins.Web;

namespace Core.OpenIDLogin.Verbs.Widgets
{
    public class OpenIDLoginWidgetEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static readonly Lazy<OpenIDLoginWidgetEditorVerb> instance = new Lazy<OpenIDLoginWidgetEditorVerb>(() => new OpenIDLoginWidgetEditorVerb());

        public static OpenIDLoginWidgetEditorVerb Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private OpenIDLoginWidgetEditorVerb()
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
            get { return "OpenIDLoginWidget"; }
        }

        public String Area
        {
            get { return "OpenIDLogin"; }
        }

        #endregion
    }
}