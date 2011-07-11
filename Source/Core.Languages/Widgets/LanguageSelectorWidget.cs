using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;
using Core.Framework.Plugins.Widgets;

namespace Core.Languages.Widgets
{
    [Export(typeof(ICoreWidget))]
    [Export(typeof(IPermissible))]
    public class LanguageSelectorWidget : BaseWidget
    {
        #region Singleton

        private static LanguageSelectorWidget _instance;

        private static readonly Object SyncRoot = new Object();

        public static LanguageSelectorWidget Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new LanguageSelectorWidget());
                }
            }
        }

        #endregion

        public override string Title
        {
            get { return "Language selector"; }
        }

        public override ICorePlugin Plugin
        {
            get { return LanguagesPlugin.Instance; }
        }

        public override string Identifier
        {
            get { return "123456sadf"; }
        }

        public override IWidgetActionVerb ViewAction
        {
            get { return ContentWidgetViewerVerb.Instance; }

        }
        public override IWidgetActionVerb EditAction
        {
            get { return ContentWidgetEditorVerb.Instance; }
        }

        public override IWidgetActionVerb SaveAction
        {
            get { return ContentWidgetSaveSettingsVerb.Instance; }
        }
    }
}