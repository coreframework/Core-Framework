using System;
using Core.Framework.Plugins.Web;

namespace Core.Web.Areas.Navigation.Verbs
{
    public class ListMenuSaveVerb : IWidgetActionVerb
    {
        #region Singleton

        private static ListMenuSaveVerb _instance;

        private static readonly Object SyncRoot = new Object();

        public static ListMenuSaveVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new ListMenuSaveVerb());
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
            get { return "ListMenu"; }
        }

        public string Area
        {
            get { return "Navigation"; }
        }

        #endregion
    }
}