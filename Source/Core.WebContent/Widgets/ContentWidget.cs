using System;
using System.ComponentModel.Composition;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;
using Core.Framework.Plugins.Widgets;
using Core.WebContent.Verbs.Widgets;

namespace Core.WebContent.Widgets
{
    [Export(typeof(ICoreWidget))]
    [Export(typeof(IPermissible))]
    public class ContentWidget : BaseWidget
    {
        #region Singleton

        private static readonly Lazy<ContentWidget> instance = new Lazy<ContentWidget>(() => new ContentWidget());

        public static ContentWidget Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private ContentWidget()
        {
        }

        #endregion

        public override ICorePlugin Plugin
        {
            get { return WebContentPlugin.Instance; }
        }

        public override IWidgetActionVerb ViewAction
        {
            get { return WebContentWidgetViewerVerb.Instance; }

        }
        public override IWidgetActionVerb EditAction
        {
            get { return WebContentWidgetEditorVerb.Instance; }
        }

        public override IWidgetActionVerb SaveAction
        {
            get { return WebContentWidgetSaveSettingsVerb.Instance; }
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