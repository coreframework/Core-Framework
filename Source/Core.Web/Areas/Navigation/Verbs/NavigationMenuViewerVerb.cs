using System;
using Core.Framework.Plugins.Web;

namespace Core.Web.Areas.Navigation.Verbs
{
    public class NavigationMenuViewerVerb : IWidgetActionVerb
    {
        #region Singleton

        private static readonly Lazy<NavigationMenuViewerVerb> instance = new Lazy<NavigationMenuViewerVerb>(() => new NavigationMenuViewerVerb());

        public static NavigationMenuViewerVerb Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private NavigationMenuViewerVerb()
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
            get { return "NavigationMenu"; }
        }

        public String Area
        {
            get { return "Navigation"; }
        }

        #endregion
    }
}