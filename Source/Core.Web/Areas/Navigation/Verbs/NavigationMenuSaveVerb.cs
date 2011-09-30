using System;
using Core.Framework.Plugins.Web;

namespace Core.Web.Areas.Navigation.Verbs
{
    public class NavigationMenuSaveVerb : IWidgetActionVerb
    {
        #region Singleton

        private static NavigationMenuSaveVerb instance;

        private static readonly Object SyncRoot = new Object();

        public static NavigationMenuSaveVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new NavigationMenuSaveVerb());
                }
            }
        }

        #endregion

        #region IWidgetActionVerb Members

        public String Action
        {
            get { return "UpdateWidget"; }
        }

        public String Controller
        {
            get { return "NavigationMenu"; }
        }

        public String Area
        {
            get { return "Navigation"; }
        }

        #endregion
    }
}