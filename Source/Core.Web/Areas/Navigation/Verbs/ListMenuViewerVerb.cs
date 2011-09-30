using System;
using Core.Framework.Plugins.Web;

namespace Core.Web.Areas.Navigation.Verbs
{
    public class ListMenuViewerVerb : IWidgetActionVerb
    {
        #region Singleton

        private static ListMenuViewerVerb instance;

        private static readonly Object SyncRoot = new Object();

        public static ListMenuViewerVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new ListMenuViewerVerb());
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
            get { return "ListMenu"; }
        }

        public String Area
        {
            get { return "Navigation"; }
        }

        #endregion
    }
}