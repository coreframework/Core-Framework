using System;
using Core.Framework.Plugins.Web;

namespace Core.Profiles.Verbs.Widgets
{
    public class RegistrationWidgetEditorVerb : IWidgetActionVerb
    {
        #region Singleton

        private static readonly Lazy<RegistrationWidgetEditorVerb> instance = new Lazy<RegistrationWidgetEditorVerb>(() => new RegistrationWidgetEditorVerb());

        public static RegistrationWidgetEditorVerb Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private RegistrationWidgetEditorVerb()
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
            get { return "RegistrationWidget"; }
        }

        public String Area
        {
            get { return "Profiles"; }
        }

        #endregion
    }
}