using System;
using Core.Framework.Plugins.Web;

namespace Core.Web.Areas.Navigation.Verbs
{
    public class BreadcrumbsEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static BreadcrumbsEditorVerb _instance;

        private static readonly Object SyncRoot = new Object();

        public static BreadcrumbsEditorVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new BreadcrumbsEditorVerb());
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
            get { return "Breadcrumbs"; }
        }

        public string Area
        {
            get { return "Navigation"; }
        }

        #endregion
    }
}