using System;
using Core.Framework.Plugins.Web;

namespace Core.Forms.Verbs.Widgets
{
    public class FormsBuilderWidgetViewerVerb : IWidgetActionVerb
    {
        #region Singleton

        private static readonly Lazy<FormsBuilderWidgetViewerVerb> instance = new Lazy<FormsBuilderWidgetViewerVerb>(() => new FormsBuilderWidgetViewerVerb());

        public static FormsBuilderWidgetViewerVerb Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private FormsBuilderWidgetViewerVerb()
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
            get { return "FormsBuilderWidget"; }
        }

        public String Area
        {
            get { return "Forms"; }
        }

        #endregion
    }
}