using System;
using Core.Framework.Plugins.Web;

namespace Core.Web.Areas.Navigation.Verbs
{
    public class SiteMapViewerVerb : IWidgetActionVerb
    {
        #region Singleton

        private static SiteMapViewerVerb instance;

        private static readonly Object SyncRoot = new Object();

        public static SiteMapViewerVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new SiteMapViewerVerb());
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
            get { return "SiteMap"; }
        }

        public String Area
        {
            get { return "Navigation"; }
        }

        #endregion
    }
}