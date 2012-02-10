using System;
using Core.Framework.Plugins.Web;

namespace Core.Web.Areas.Navigation.Verbs
{
    public class NavigationMenuEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static readonly Lazy<NavigationMenuEditorVerb> instance = new Lazy<NavigationMenuEditorVerb>(() => new NavigationMenuEditorVerb());

        public static NavigationMenuEditorVerb Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private NavigationMenuEditorVerb()
        {
        }

        #endregion

        #region IWidgetActionVerb Members

        public String Action
        {
            get { return "EditWidget"; }
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