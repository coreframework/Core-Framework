using System;
using System.ComponentModel.Composition;
using Core.FormLogin;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;
using Core.Framework.Plugins.Widgets;
using Core.LoginWorkflow.Verbs;
using Core.OpenIDLogin;

namespace Core.LoginWorkflow.Widgets
{
    [Export(typeof(ICoreWidget))]
    [Export(typeof(IPermissible))]
    public class LoginHolderWidget : BaseWorkflowWidget
    {
        #region Singleton

        private static readonly Lazy<LoginHolderWidget> instance = new Lazy<LoginHolderWidget>(() => new LoginHolderWidget());

        public static LoginHolderWidget Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private LoginHolderWidget()
        {
            innerPlugins.Add(FormLoginPlugin.Instance);
            innerPlugins.Add(OpenIDLoginPlugin.Instance);
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