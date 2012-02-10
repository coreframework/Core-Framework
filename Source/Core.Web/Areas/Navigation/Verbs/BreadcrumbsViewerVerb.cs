using System;
using Core.Framework.Plugins.Web;

namespace Core.Web.Areas.Navigation.Verbs
{
    public class BreadcrumbsViewerVerb : IWidgetActionVerb
    {
        #region Singleton

        private static readonly Lazy<BreadcrumbsViewerVerb> instance = new Lazy<BreadcrumbsViewerVerb>(() => new BreadcrumbsViewerVerb());

        public static BreadcrumbsViewerVerb Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private BreadcrumbsViewerVerb()
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
            get { return "Breadcrumbs"; }
        }

        public String Area
        {
            get { return "Navigation"; }
        }

        #endregion
    }
}