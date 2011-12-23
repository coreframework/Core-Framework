using System;
using System.ComponentModel.Composition;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;
using Core.Framework.Plugins.Widgets;
using Core.LoginWorkflow.Verbs;

namespace Core.LoginWorkflow.Widgets
{
    [Export(typeof(ICoreWidget))]
    [Export(typeof(IPermissible))]
    public class LoginHolderWidget : BaseWidget
    {
        #region Singleton

        private static LoginHolderWidget instance;

        private static readonly Object syncRoot = new Object();

        public static LoginHolderWidget Instance
        {
            get
            {
                lock (syncRoot)
                {
                    return instance ?? (instance = new LoginHolderWidget());
                }
            }
        }

        private LoginHolderWidget()
        {

        }

        #endregion

        public override ICorePlugin Plugin
        {
            get { return LoginWorkflowPlugin.Instance; }
        }

        public override IWidgetActionVerb ViewAction
        {
            get { return LoginHolderWidgetViewerVerb.Instance; }

        }
        public override IWidgetActionVerb EditAction
        {
            get { return LoginHolderWidgetEditorVerb.Instance; }
        }

        public override IWidgetActionVerb SaveAction
        {
            get { return LoginHolderWidgetSaveSettingsVerb.Instance; }
        }

        /*  public override void Remove(ICoreWidgetInstance coreWidgetInstance)
          {
              WebContentWidgetHelper.Remove(coreWidgetInstance);
          }

          public override long? Clone(ICoreWidgetInstance coreWidgetInstance)
          {
              return WebContentWidgetHelper.CloneWebContentWidget(coreWidgetInstance);
          }*/
    }
}