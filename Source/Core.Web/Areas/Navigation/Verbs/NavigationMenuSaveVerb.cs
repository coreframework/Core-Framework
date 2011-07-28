using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Framework.Plugins.Web;

namespace Core.Web.Areas.Navigation.Verbs
{
    public class NavigationMenuSaveVerb : IWidgetActionVerb
    {
        #region Singleton

        private static NavigationMenuSaveVerb _instance;

        private static readonly Object SyncRoot = new Object();

        public static NavigationMenuSaveVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new NavigationMenuSaveVerb());
                }
            }
        }

        #endregion

        #region IWidgetActionVerb Members

        public string Action
        {
            get { return "UpdateWidget"; }
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