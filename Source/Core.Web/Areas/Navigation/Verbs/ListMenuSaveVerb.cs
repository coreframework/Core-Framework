using System;
using Core.Framework.Plugins.Web;

namespace Core.Web.Areas.Navigation.Verbs
{
    public class ListMenuSaveVerb : IWidgetActionVerb
    {
        #region Singleton

        private static readonly Lazy<ListMenuSaveVerb> instance = new Lazy<ListMenuSaveVerb>(() => new ListMenuSaveVerb());

        public static ListMenuSaveVerb Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private ListMenuSaveVerb()
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
            get { return "ListMenu"; }
        }

        public String Area
        {
            get { return "Navigation"; }
        }

        #endregion
    }
}