using System;
using Core.Framework.Plugins.Web;

namespace Core.Forms.Verbs.Widgets
{
    public class FormsBuilderWidgetSaveSettingsVerb : IWidgetActionVerb
    {
        #region Singleton

        private static FormsBuilderWidgetSaveSettingsVerb _instance;

        private static readonly Object SyncRoot = new Object();

        public static FormsBuilderWidgetSaveSettingsVerb Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new FormsBuilderWidgetSaveSettingsVerb());
                }
            }
        }

        #endregion

        #region IWidgetActionVerb Members

        public string Action
        {
            get { return "UpdateWidget"; }
        }

        public string Controller
        {
            get { return "FormsBuilderWidget"; }
        }

        public string Area
        {
            get { return "Forms"; }
        }

        #endregion
    }
}