using System;
using Core.Framework.Plugins.Web;

namespace Core.Forms.Verbs.Widgets
{
    public class FormsBuilderWidgetSaveSettingsVerb : IWidgetActionVerb
    {
        #region Singleton

        private static readonly Lazy<FormsBuilderWidgetSaveSettingsVerb> instance = new Lazy<FormsBuilderWidgetSaveSettingsVerb>(() => new FormsBuilderWidgetSaveSettingsVerb());

        public static FormsBuilderWidgetSaveSettingsVerb Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private FormsBuilderWidgetSaveSettingsVerb()
        {
        }

        #endregion

        #region IWidgetActionVerb Members

        public String Action
        {
            get { return "UpdateWidget"; }
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