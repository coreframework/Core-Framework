using System;
using Core.Framework.Plugins.Web;

namespace Core.Web.Verbs
{
    public class PlaceHolderWidgetViewVerb : IWidgetActionVerb
    {
        #region Singleton

        private static readonly Lazy<PlaceHolderWidgetViewVerb> instance = new Lazy<PlaceHolderWidgetViewVerb>(() => new PlaceHolderWidgetViewVerb());

        public static PlaceHolderWidgetViewVerb Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private PlaceHolderWidgetViewVerb()
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
            get { return "PlaceHolderWidget"; }
        }

        public String Area
        {
            get { return String.Empty; }
        }

        #endregion
    }
}