using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Framework.Plugins.Web;

namespace Core.ContentPages.Verbs.Widgets
{
    public class ContentDetailsWidgetViewerVerb : IWidgetActionVerb
    {
        #region Singleton

        private static ContentDetailsWidgetViewerVerb instance;

        private static readonly Object syncRoot = new Object();

        public static ContentDetailsWidgetViewerVerb Instance
        {
            get
            {
                lock (syncRoot)
                {
                    return instance ?? (instance = new ContentDetailsWidgetViewerVerb());
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
            get { return "ContentDetailsWidget"; }
        }

        public String Area
        {
            get { return "ContentPage"; }
        }

        #endregion
    }
}