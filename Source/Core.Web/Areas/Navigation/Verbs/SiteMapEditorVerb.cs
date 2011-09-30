using System;
using Core.Framework.Plugins.Web;

namespace Core.Web.Areas.Navigation.Verbs
{
    public class SiteMapEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static SiteMapEditorVerb instance;

        private static readonly Object SyncRoot = new Object();

        public static SiteMapEditorVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new SiteMapEditorVerb());
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
            get { return "SiteMap"; }
        }

        public String Area
        {
            get { return "Navigation"; }
        }

        #endregion
    }
}