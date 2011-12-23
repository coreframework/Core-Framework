using System;
using Core.Framework.Plugins.Web;

namespace Core.LoginWorkflow.Verbs
{
    public class LoginHolderWidgetEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static LoginHolderWidgetEditorVerb instance;

        private static readonly Object syncRoot = new Object();

        public static LoginHolderWidgetEditorVerb Instance
        {
            get
            {
                lock (syncRoot)
                {
                    return instance ?? (instance = new LoginHolderWidgetEditorVerb());
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
            get { return "LoginHolderWidget"; }
        }

        public String Area
        {
            get { return "LoginWorkflow"; }
        }

        #endregion
    }
}