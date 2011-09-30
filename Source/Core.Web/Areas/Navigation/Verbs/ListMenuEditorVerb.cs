using System;
using Core.Framework.Plugins.Web;

namespace Core.Web.Areas.Navigation.Verbs
{
    public class ListMenuEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static ListMenuEditorVerb instance;

        private static readonly Object SyncRoot = new Object();

        public static ListMenuEditorVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new ListMenuEditorVerb());
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
            get { return "ListMenu"; }
        }

        public String Area
        {
            get { return "Navigation"; }
        }

        #endregion
    }
}