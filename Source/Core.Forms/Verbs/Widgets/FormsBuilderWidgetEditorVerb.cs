using System;
using Core.Framework.Plugins.Web;

namespace Core.Forms.Verbs.Widgets
{
    public class FormsBuilderWidgetEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static FormsBuilderWidgetEditorVerb instance;

        private static readonly Object syncRoot = new Object();

        public static FormsBuilderWidgetEditorVerb Instance
        {
            get
            {
                lock (syncRoot)
                {
                    return instance ?? (instance = new FormsBuilderWidgetEditorVerb());
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
            get {return "FormsBuilderWidget"; }
        }

        public String Area
        {
            get { return "Forms"; }
        }

        #endregion
    }
}