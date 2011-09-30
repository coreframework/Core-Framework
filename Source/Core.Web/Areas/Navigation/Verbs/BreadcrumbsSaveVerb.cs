using System;
using Core.Framework.Plugins.Web;

namespace Core.Web.Areas.Navigation.Verbs
{
    public class BreadcrumbsSaveVerb : IWidgetActionVerb
    {
        #region Singleton

        private static BreadcrumbsSaveVerb instance;

        private static readonly Object SyncRoot = new Object();

        public static BreadcrumbsSaveVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new BreadcrumbsSaveVerb());
                }
            }
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