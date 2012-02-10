using System;
using Core.Framework.Plugins.Web;

namespace Core.Web.Areas.Navigation.Verbs
{
    public class BreadcrumbsSaveVerb : IWidgetActionVerb
    {
        #region Singleton

        private static readonly Lazy<BreadcrumbsSaveVerb> instance = new Lazy<BreadcrumbsSaveVerb>(() => new BreadcrumbsSaveVerb());

        public static BreadcrumbsSaveVerb Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private BreadcrumbsSaveVerb()
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
            get { return "Breadcrumbs"; }
        }

        public String Area
        {
            get { return "Navigation"; }
        }

        #endregion
    }
}