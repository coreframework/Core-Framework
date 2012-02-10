using System;
using Core.Framework.Plugins.Web;

namespace Core.Web.Areas.Navigation.Verbs
{
    public class NavigationMenuSaveVerb : IWidgetActionVerb
    {
        #region Singleton

        private static readonly Lazy<NavigationMenuSaveVerb> instance = new Lazy<NavigationMenuSaveVerb>(() => new NavigationMenuSaveVerb());

        public static NavigationMenuSaveVerb Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private NavigationMenuSaveVerb()
        {
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