using System;
using Core.Framework.Plugins.Web;

namespace Core.LoginWorkflow.Verbs
{
    public class LoginHolderWidgetViewerVerb : IWidgetActionVerb
    {
        #region Singleton

        private static readonly Lazy<LoginHolderWidgetViewerVerb> instance = new Lazy<LoginHolderWidgetViewerVerb>(() => new LoginHolderWidgetViewerVerb());

        public static LoginHolderWidgetViewerVerb Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private LoginHolderWidgetViewerVerb()
        {
        }

        #endregion

        #region IWidgetActionVerb Members

        public String Action
        {
            get { return "ViewWidget"; }
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