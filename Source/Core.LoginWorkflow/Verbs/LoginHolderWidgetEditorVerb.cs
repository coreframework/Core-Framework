using System;
using Core.Framework.Plugins.Web;

namespace Core.LoginWorkflow.Verbs
{
    public class LoginHolderWidgetEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static readonly Lazy<LoginHolderWidgetEditorVerb> instance = new Lazy<LoginHolderWidgetEditorVerb>(() => new LoginHolderWidgetEditorVerb());

        public static LoginHolderWidgetEditorVerb Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private LoginHolderWidgetEditorVerb()
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
            get { return "LoginHolderWidget"; }
        }

        public String Area
        {
            get { return "LoginWorkflow"; }
        }

        #endregion
    }
}