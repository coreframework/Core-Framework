using System;
using System.ComponentModel.Composition;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;
using Core.Framework.Plugins.Widgets;
using Core.Languages.Permissions.Operations;
using Core.Languages.Verbs.Widgets;

namespace Core.Languages.Widgets
{
    [Export(typeof(ICoreWidget))]
    [Export(typeof(IPermissible))]
    public class LanguageSelectorWidget : BaseWidget
    {
        #region Singleton

        private static LanguageSelectorWidget instance;

        private static readonly Object SyncRoot = new Object();

        public static LanguageSelectorWidget Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return instance ?? (instance = new LanguageSelectorWidget());
                }
            }
        }

        private LanguageSelectorWidget()
        {
            Operations = OperationsHelper.GetOperations<LanguageSelectorWidgetOperations>();
        }

        #endregion

        public override ICorePlugin Plugin
        {
            get { return LanguagesPlugin.Instance; }
        }

        public override IWidgetActionVerb ViewAction
        {
            get { return LanguageSelectorWidgetViewerVerb.Instance; }

        }
        public override IWidgetActionVerb EditAction
        {
            get { return null; }
        }

        public override IWidgetActionVerb SaveAction
        {
            get { return null; }
        }
    }
}