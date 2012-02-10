using System;
using Core.Framework.Plugins.Web;

namespace Core.Forms.Verbs.Widgets
{
    public class FormsBuilderWidgetEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static readonly Lazy<FormsBuilderWidgetEditorVerb> instance = new Lazy<FormsBuilderWidgetEditorVerb>(() => new FormsBuilderWidgetEditorVerb());

        public static FormsBuilderWidgetEditorVerb Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private FormsBuilderWidgetEditorVerb()
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
            get {return "FormsBuilderWidget"; }
        }

        public String Area
        {
            get { return "Forms"; }
        }

        #endregion
    }
}