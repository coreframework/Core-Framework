using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Framework.Plugins.Web;

namespace Core.Web.Areas.Navigation.Verbs
{
    public class NavigationMenuViewerVerb : IWidgetActionVerb
    {
        #region Singleton

        private static NavigationMenuViewerVerb _instance;

        private static readonly Object SyncRoot = new Object();

        public static NavigationMenuViewerVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new NavigationMenuViewerVerb());
                }
            }
        }

        #endregion

        #region IWidgetActionVerb Members

        public string Action
        {
            get { return "ViewWidget"; }
        }

        public string Controller
        {
            get { return "NavigationMenu"; }
        }

        public string Area
        {
            get { return "Navigation"; }
        }

        #endregion
    }
}