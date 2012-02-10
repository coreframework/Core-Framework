using System;
using Core.Framework.Plugins.Web;

namespace Core.FormLogin.Verbs.Widgets
{
    public class LoginWidgetEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static readonly Lazy<LoginWidgetEditorVerb> instance = new Lazy<LoginWidgetEditorVerb>(() => new LoginWidgetEditorVerb());

        public static LoginWidgetEditorVerb Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private LoginWidgetEditorVerb()
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
            get { return "LoginWidget"; }
        }

        public String Area
        {
            get { return "FormLogin"; }
        }

        #endregion
    }
}