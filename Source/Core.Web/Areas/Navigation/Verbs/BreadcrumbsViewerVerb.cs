using System;
using Core.Framework.Plugins.Web;

namespace Core.Web.Areas.Navigation.Verbs
{
    public class BreadcrumbsViewerVerb : IWidgetActionVerb
    {
        #region Singleton

        private static BreadcrumbsViewerVerb instance;

        private static readonly Object SyncRoot = new Object();

        public static BreadcrumbsViewerVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new BreadcrumbsViewerVerb());
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
            get { return "Breadcrumbs"; }
        }

        public String Area
        {
            get { return "Navigation"; }
        }

        #endregion
    }
}