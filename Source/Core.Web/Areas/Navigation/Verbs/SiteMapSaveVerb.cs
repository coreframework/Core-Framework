using System;
using Core.Framework.Plugins.Web;

namespace Core.Web.Areas.Navigation.Verbs
{
    public class SiteMapSaveVerb : IWidgetActionVerb
    {
        #region Singleton

        private static SiteMapSaveVerb instance;

        private static readonly Object SyncRoot = new Object();

        public static SiteMapSaveVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new SiteMapSaveVerb());
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
            get { return "SiteMap"; }
        }

        public String Area
        {
            get { return "Navigation"; }
        }

        #endregion
    }
}