using System;
using Core.Framework.Plugins.Web;

namespace Core.Web.Areas.Navigation.Verbs
{
    public class ListMenuViewerVerb : IWidgetActionVerb
    {
        #region Singleton

        private static readonly Lazy<ListMenuViewerVerb> instance = new Lazy<ListMenuViewerVerb>(() => new ListMenuViewerVerb());

        public static ListMenuViewerVerb Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private ListMenuViewerVerb()
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
            get { return "ListMenu"; }
        }

        public String Area
        {
            get { return "Navigation"; }
        }

        #endregion
    }
}