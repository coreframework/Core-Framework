using System;
using Core.Framework.Plugins.Web;

namespace Core.Web.Areas.Navigation.Verbs
{
    public class SiteMapSaveVerb : IWidgetActionVerb
    {
        #region Singleton

        private static readonly Lazy<SiteMapSaveVerb> instance = new Lazy<SiteMapSaveVerb>(() => new SiteMapSaveVerb());

        public static SiteMapSaveVerb Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private SiteMapSaveVerb()
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
            get { return "SiteMap"; }
        }

        public String Area
        {
            get { return "Navigation"; }
        }

        #endregion
    }
}