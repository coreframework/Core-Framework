using System;
using System.ComponentModel.Composition;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;
using Core.Framework.Plugins.Widgets;
using Core.Web.Areas.Navigation.Verbs;

namespace Core.Web.Areas.Navigation.Widgets
{
    [Export(typeof(ICoreWidget))]
    [Export(typeof(IPermissible))]
    public class NListMenuWidget: BaseWidget
    {
        #region Singleton

        private static NListMenuWidget _instance;

        private static readonly Object SyncRoot = new Object();

        public static NListMenuWidget Instance
        {
            get
            {
                lock (SyncRoot)
                {
                    return _instance ?? (_instance = new NListMenuWidget());
                }
            }
        }

        #endregion

        public override string Title
        {
            get { return "List Menu"; }
        }

        public override ICorePlugin Plugin
        {
            get { return NavigationPlugin.Instance; }
        }

        public override string Identifier
        {
            get { return "5224B396-44E1-11E0-B8AF-801ADFD92i85"; }
        }

        public override IWidgetActionVerb ViewAction
        {
            get { return ListMenuViewerVerb.Instance; }
        }

        public override IWidgetActionVerb EditAction
        {
            get { return ListMenuEditorVerb.Instance; }
        }

        public override IWidgetActionVerb SaveAction
        {
            get { return ListMenuSaveVerb.Instance; }
        }
    }
}