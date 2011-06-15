using System;
using System.ComponentModel.Composition;
using Core.Forms.Verbs.Widgets;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;
using Core.Framework.Plugins.Widgets;

namespace Core.Forms.Widgets
{
    [Export(typeof(ICoreWidget))]
    [Export(typeof(IPermissible))]
    public class FormsBuilderWidget : BaseWidget
    {
        #region Singleton

        private static FormsBuilderWidget _instance;

        private static readonly Object SyncRoot = new Object();

        public static FormsBuilderWidget Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new FormsBuilderWidget());
                }
            }
        }

        #endregion

        public override string Title
        {
            get { return "Forms Builder"; }
        }

        public override ICorePlugin Plugin
        {
            get { return FormsPlugin.Instance; }
        }

        public override string Identifier
        {
            get { return "23333"; }
        }

        public override IWidgetActionVerb ViewAction
        {
            get { return FormsBuilderWidgetViewerVerb.Instance; }

        }
        public override IWidgetActionVerb EditAction
        {
            get { return FormsBuilderWidgetEditorVerb.Instance; }
        }

        public override IWidgetActionVerb SaveAction
        {
            get { return FormsBuilderWidgetSaveSettingsVerb.Instance; }
        }
    }
}