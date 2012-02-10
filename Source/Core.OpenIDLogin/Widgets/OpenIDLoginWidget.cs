using System;
using System.ComponentModel.Composition;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;
using Core.Framework.Plugins.Widgets;
using Core.OpenIDLogin.Verbs.Widgets;

namespace Core.OpenIDLogin.Widgets
{
    [Export(typeof(ICoreWidget))]
    [Export(typeof(IPermissible))]
    public class OpenIDLoginWidget : BaseWidget
    {
        #region Singleton

        private static readonly Lazy<OpenIDLoginWidget> instance = new Lazy<OpenIDLoginWidget>(() => new OpenIDLoginWidget());

        public static OpenIDLoginWidget Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private OpenIDLoginWidget()
        {
        }

        #endregion

        public override ICorePlugin Plugin
        {
            get { return OpenIDLoginPlugin.Instance; }
        }

        public override IWidgetActionVerb ViewAction
        {
            get { return OpenIDLoginWidgetViewerVerb.Instance; }

        }
        public override IWidgetActionVerb EditAction
        {
            get { return OpenIDLoginWidgetEditorVerb.Instance; }
        }

        public override IWidgetActionVerb SaveAction
        {
            get { return OpenIDLoginWidgetSaveSettingsVerb.Instance; }
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