using System;
using Core.Framework.Plugins.Web;

namespace Core.Web.Verbs
{
    public class PlaceHolderWidgetViewVerb : IWidgetActionVerb
    {
        #region Singleton

        private static PlaceHolderWidgetViewVerb instance;

        private static readonly Object SyncRoot = new Object();

        public static PlaceHolderWidgetViewVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new PlaceHolderWidgetViewVerb());
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
            get { return "PlaceHolderWidget"; }
        }

        public String Area
        {
            get { return String.Empty; }
        }

        #endregion
    }
}