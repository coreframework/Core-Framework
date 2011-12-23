using System;
using Core.Framework.Plugins.Web;

namespace Core.FormLogin.Verbs.Widgets
{
    public class LoginWidgetEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static LoginWidgetEditorVerb instance;

        private static readonly Object syncRoot = new Object();

        public static LoginWidgetEditorVerb Instance
        {
            get
            {
                lock (syncRoot)
                {
                    return instance ?? (instance = new LoginWidgetEditorVerb());
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
            get { return "LoginWidget"; }
        }

        public String Area
        {
            get { return "FormLogin"; }
        }

        #endregion
    }
}