using System;
using Core.Framework.Plugins.Web;

namespace Core.Web.Areas.Navigation.Verbs
{
    public class ListMenuEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static ListMenuEditorVerb _instance;

        private static readonly Object SyncRoot = new Object();

        public static ListMenuEditorVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new ListMenuEditorVerb());
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
            get { return "ListMenu"; }
        }

        public string Area
        {
            get { return "Navigation"; }
        }

        #endregion
    }
}