using System;
using Core.Framework.Plugins.Web;

namespace Core.Web.Areas.Navigation.Verbs
{
    public class SiteMapEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static readonly Lazy<SiteMapEditorVerb> instance = new Lazy<SiteMapEditorVerb>(() => new SiteMapEditorVerb());

        public static SiteMapEditorVerb Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private SiteMapEditorVerb()
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
            get { return "SiteMap"; }
        }

        public String Area
        {
            get { return "Navigation"; }
        }

        #endregion
    }
}