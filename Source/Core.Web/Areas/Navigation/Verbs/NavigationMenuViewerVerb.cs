using System;
using Core.Framework.Plugins.Web;

namespace Core.Web.Areas.Navigation.Verbs
{
    public class NavigationMenuViewerVerb : IWidgetActionVerb
    {
        #region Singleton

        private static NavigationMenuViewerVerb instance;

        private static readonly Object SyncRoot = new Object();

        public static NavigationMenuViewerVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new NavigationMenuViewerVerb());
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
            get { return "NavigationMenu"; }
        }

        public String Area
        {
            get { return "Navigation"; }
        }

        #endregion
    }
}