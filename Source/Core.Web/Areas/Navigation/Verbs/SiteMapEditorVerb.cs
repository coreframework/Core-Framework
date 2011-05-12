using System;
using Core.Framework.Plugins.Web;

namespace Core.Web.Areas.Navigation.Verbs
{
    public class SiteMapEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static SiteMapEditorVerb _instance;

        private static readonly Object SyncRoot = new Object();

        public static SiteMapEditorVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new SiteMapEditorVerb());
                }
            }
        }

        #endregion

        #region IWidgetActionVerb Members

        public string Action
        {
            get { return "EditWidget"; }
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