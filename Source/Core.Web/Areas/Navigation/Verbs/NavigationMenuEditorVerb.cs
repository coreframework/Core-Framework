using System;
using Core.Framework.Plugins.Web;

namespace Core.Web.Areas.Navigation.Verbs
{
    public class NavigationMenuEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static NavigationMenuEditorVerb instance;

        private static readonly Object SyncRoot = new Object();

        public static NavigationMenuEditorVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new NavigationMenuEditorVerb());
                }
            }
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