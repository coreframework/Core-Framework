using System;
using Core.Framework.Plugins.Web;

namespace Core.Web.Areas.Navigation.Verbs
{
    public class ListMenuSaveVerb : IWidgetActionVerb
    {
        #region Singleton

        private static ListMenuSaveVerb instance;

        private static readonly Object SyncRoot = new Object();

        public static ListMenuSaveVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new ListMenuSaveVerb());
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
            get { return "ListMenu"; }
        }

        public String Area
        {
            get { return "Navigation"; }
        }

        #endregion
    }
}