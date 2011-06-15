using System;
using Core.Framework.Plugins.Web;

namespace Core.Forms.Verbs.Widgets
{
    public class FormsBuilderWidgetEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static FormsBuilderWidgetEditorVerb _instance;

        private static readonly Object SyncRoot = new Object();

        public static FormsBuilderWidgetEditorVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new FormsBuilderWidgetEditorVerb());
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
            get {return "FormsBuilderWidget"; }
        }

        public string Area
        {
            get { return "Forms"; }
        }

        #endregion
    }
}