using System;
using Core.Framework.Plugins.Web;

namespace Core.Web.Areas.Navigation.Verbs
{
    public class SiteMapSaveVerb : IWidgetActionVerb
    {
        #region Singleton

        private static SiteMapSaveVerb _instance;

        private static readonly Object SyncRoot = new Object();

        public static SiteMapSaveVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new SiteMapSaveVerb());
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
            get { return "SiteMap"; }
        }

        public string Area
        {
            get { return "Navigation"; }
        }

        #endregion
    }
}