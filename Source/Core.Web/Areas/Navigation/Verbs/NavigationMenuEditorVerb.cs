using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Framework.Plugins.Web;

namespace Core.Web.Areas.Navigation.Verbs
{
    public class NavigationMenuEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static NavigationMenuEditorVerb _instance;

        private static readonly Object SyncRoot = new Object();

        public static NavigationMenuEditorVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new NavigationMenuEditorVerb());
                }
            }
        }

        #endregion

        #region IWidgetActionVerb Members

        public string Action
        {
            get { return "EditWidget"; }
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