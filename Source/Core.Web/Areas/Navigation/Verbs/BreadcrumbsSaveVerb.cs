using System;
using Core.Framework.Plugins.Web;

namespace Core.Web.Areas.Navigation.Verbs
{
    public class BreadcrumbsSaveVerb : IWidgetActionVerb
    {
        #region Singleton

        private static BreadcrumbsSaveVerb _instance;

        private static readonly Object SyncRoot = new Object();

        public static BreadcrumbsSaveVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new BreadcrumbsSaveVerb());
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
            get { return "Breadcrumbs"; }
        }

        public string Area
        {
            get { return "Navigation"; }
        }

        #endregion
    }
}