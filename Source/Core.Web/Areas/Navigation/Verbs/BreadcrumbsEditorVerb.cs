using System;
using Core.Framework.Plugins.Web;

namespace Core.Web.Areas.Navigation.Verbs
{
    public class BreadcrumbsEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static readonly Lazy<BreadcrumbsEditorVerb> instance = new Lazy<BreadcrumbsEditorVerb>(() => new BreadcrumbsEditorVerb());

        public static BreadcrumbsEditorVerb Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private BreadcrumbsEditorVerb()
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
            get { return "Breadcrumbs"; }
        }

        public String Area
        {
            get { return "Navigation"; }
        }

        #endregion
    }
}