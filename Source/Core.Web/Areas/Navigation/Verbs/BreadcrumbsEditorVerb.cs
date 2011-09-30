using System;
using Core.Framework.Plugins.Web;

namespace Core.Web.Areas.Navigation.Verbs
{
    public class BreadcrumbsEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static BreadcrumbsEditorVerb instance;

        private static readonly Object SyncRoot = new Object();

        public static BreadcrumbsEditorVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new BreadcrumbsEditorVerb());
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
            get { return "Breadcrumbs"; }
        }

        public String Area
        {
            get { return "Navigation"; }
        }

        #endregion
    }
}