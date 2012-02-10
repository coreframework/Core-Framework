using System;
using Core.Framework.Plugins.Web;

namespace Core.Web.Areas.Navigation.Verbs
{
    public class SiteMapViewerVerb : IWidgetActionVerb
    {
        #region Singleton

        private static readonly Lazy<SiteMapViewerVerb> instance = new Lazy<SiteMapViewerVerb>(() => new SiteMapViewerVerb());

        public static SiteMapViewerVerb Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private SiteMapViewerVerb()
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
            get { return "SiteMap"; }
        }

        public String Area
        {
            get { return "Navigation"; }
        }

        #endregion
    }
}