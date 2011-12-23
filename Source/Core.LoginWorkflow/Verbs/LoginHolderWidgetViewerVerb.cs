using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Framework.Plugins.Web;

namespace Core.LoginWorkflow.Verbs
{
    public class LoginHolderWidgetViewerVerb : IWidgetActionVerb
    {
        #region Singleton

        private static LoginHolderWidgetViewerVerb instance;

        private static readonly Object syncRoot = new Object();

        public static LoginHolderWidgetViewerVerb Instance
        {
            get
            {
                lock (syncRoot)
                {
                    return instance ?? (instance = new LoginHolderWidgetViewerVerb());
                }
            }
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