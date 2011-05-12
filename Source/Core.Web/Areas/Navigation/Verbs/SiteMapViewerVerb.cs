using System;
using Core.Framework.Plugins.Web;

namespace Core.Web.Areas.Navigation.Verbs
{
    public class SiteMapViewerVerb : IWidgetActionVerb
    {
        #region Singleton

        private static SiteMapViewerVerb _instance;

        private static readonly Object SyncRoot = new Object();

        public static SiteMapViewerVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new SiteMapViewerVerb());
                }
            }
        }

        #endregion

        #region IWidgetActionVerb Members

        public string Action
        {
            get { return "ViewWidget"; }
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