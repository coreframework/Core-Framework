using System;
using Core.Framework.Plugins.Web;

namespace Core.Web.Areas.Navigation.Verbs
{
    public class ListMenuEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static readonly Lazy<ListMenuEditorVerb> instance = new Lazy<ListMenuEditorVerb>(() => new ListMenuEditorVerb());

        public static ListMenuEditorVerb Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private ListMenuEditorVerb()
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
            get { return "ListMenu"; }
        }

        public String Area
        {
            get { return "Navigation"; }
        }

        #endregion
    }
}