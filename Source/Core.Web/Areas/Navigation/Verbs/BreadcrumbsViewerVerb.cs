using System;
using Core.Framework.Plugins.Web;

namespace Core.Web.Areas.Navigation.Verbs
{
    public class BreadcrumbsViewerVerb : IWidgetActionVerb
    {
        #region Singleton

        private static BreadcrumbsViewerVerb _instance;

        private static readonly Object SyncRoot = new Object();

        public static BreadcrumbsViewerVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new BreadcrumbsViewerVerb());
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
            get { return "Breadcrumbs"; }
        }

        public string Area
        {
            get { return "Navigation"; }
        }

        #endregion
    }
}